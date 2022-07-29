using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick variableJoystick;
    public float SpeedRotate;
    public float Speed;
    [HideInInspector]
    public float rotation;
    private float NowRotate;
    [HideInInspector]
    public bool isRun = false;
    [HideInInspector]
    public bool canRun = true;
    private CharacterController _characterController;
    private Animator _animationController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        _animationController = GetComponent<Animator>();
    }


    public void Update()
    {
        if (canRun)
        {
            Rotate();
        }
        if (isRun)
        {
            Vector3 Rel_Dir = transform.TransformDirection(Vector3.forward);
            _characterController.SimpleMove(Rel_Dir * Speed);
        }
        setAnimations();
    }


    void setAnimations()
    {
        _animationController.SetBool("isRun", isRun);
    }


    public IEnumerator Kick()
    {
        canRun = false;
        isRun = false;
        _animationController.SetBool("isBoxing", true);
        yield return new WaitForSeconds(0.5f);
        GameObject aim = GetComponent<TakeZone>().AimObject;
        if (aim && aim.GetComponent<BrokenItem>())
        {
            GetComponent<TakeZone>().AimObject.GetComponent<BrokenItem>().Broke(10);
        }
        if (aim && aim.GetComponent<buildingController>())
        {
            GetComponent<TakeZone>().AimObject.GetComponent<buildingController>().TakeDamage(10);
        }
        _animationController.SetBool("isBoxing", false);
        yield return new WaitForSeconds(0.6f);
        canRun = true;
    }

    void Rotate()
    {
        NowRotate = transform.eulerAngles.y;
        float horizontal = variableJoystick.Horizontal;
        float vertical = variableJoystick.Vertical;
        Vector3 direction = new Vector3(horizontal, vertical);
        Vector3 aa = new Vector3(0, 0, 0);
        Vector3 diff = aa - direction;
        diff.Normalize();
        rotation = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;


        if (rotation != 0)
        {
            isRun = true;
            rotation += 180;
            if (NowRotate - rotation < SpeedRotate * Time.deltaTime && NowRotate - rotation > -SpeedRotate * Time.deltaTime)
            {
                transform.rotation = Quaternion.Euler(0f, rotation, 0);
            }
            else if (NowRotate >= 180)
            {
                if (NowRotate - 180 <= rotation && NowRotate > rotation)
                    transform.rotation = Quaternion.Euler(0f, NowRotate - SpeedRotate * Time.deltaTime, 0);
                else
                    transform.rotation = Quaternion.Euler(0f, NowRotate + SpeedRotate * Time.deltaTime, 0);
            }
            else if (NowRotate < 180)
            {
                if (NowRotate + 180 > rotation && NowRotate < rotation)
                    transform.rotation = Quaternion.Euler(0f, NowRotate + SpeedRotate * Time.deltaTime, 0);
                else
                    transform.rotation = Quaternion.Euler(0f, NowRotate - SpeedRotate * Time.deltaTime, 0);
            }
        }
        else
        {
            isRun = false;
        }
    }
}


