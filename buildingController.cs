using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingController : MonoBehaviour
{
    public int Tag;
    public float health=100;
    public float nowHealth = 10;
    public Vector3 offset;
    public int maxResource;
    public Material color;

    private int nowResource;
    private int index;

    private GameObject buildingText;
    public GameObject textPos;
    private Camera cam;

    private bool isView;
    public bool isBuild=false;
    public bool isCraft=false;

    private void Start()
    {
        if (transform.parent==null)
        {
            isBuild = true;
            nowResource = PlayerPrefs.GetInt("building" + Tag + ".nowResource");
            nowHealth= PlayerPrefs.GetFloat("building" + Tag + ".health");
        }

        if (isBuild && PlayerPrefs.GetInt("building" + Tag + ".nowResource") == maxResource)
        {
            isCraft = true;
            ToOpaqueMode(GetComponent<Renderer>().material);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshCollider>().enabled = true;
        }
        else
            this.gameObject.layer = 6;
    }

    private void Update()
    {
        if (!isBuild && !transform.parent)
        {
            isBuild = true;
            Save();
        }
        if (isView)
            buildingText.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(textPos.transform.position);
        if (isCraft)
        {
            ToOpaqueMode(GetComponent<Renderer>().material);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshCollider>().enabled = true;
            nowHealth = health;
            PlayerPrefs.SetFloat("building" + Tag + ".health", nowHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        nowHealth -= damage;
        PlayerPrefs.SetFloat("building" + Tag + ".health", nowHealth);
        if (nowHealth <= 0)
        {
            buildingText.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }


    public void ShowText(bool res)
    {
        if (isBuild && !isCraft)
        {
            isView = res;
            buildingText.SetActive(res);
            if (res)
            {
                buildingText.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(textPos.transform.position);
                buildingText.GetComponent<Text>().text = nowResource + "/" + maxResource;

            }
        }
    }

    void Save()
    {
        PlayerPrefs.SetInt("buildingsCount", Tag + 1);
        PlayerPrefs.SetInt("building" + Tag + ".index", index);
        PlayerPrefs.SetFloat("building" + Tag + ".health", 10);
        PlayerPrefs.SetFloat("building" + Tag + ".position.x", transform.position.x);
        PlayerPrefs.SetFloat("building" + Tag + ".position.y", transform.position.y);
        PlayerPrefs.SetFloat("building" + Tag + ".position.z", transform.position.z);
        PlayerPrefs.SetFloat("building" + Tag + ".quaternion.x", transform.rotation.x);
        PlayerPrefs.SetFloat("building" + Tag + ".quaternion.y", transform.rotation.y);
        PlayerPrefs.SetFloat("building" + Tag + ".quaternion.z", transform.rotation.z);
        PlayerPrefs.SetFloat("building" + Tag + ".quaternion.w", transform.rotation.w);
        PlayerPrefs.SetInt("building" + Tag + ".nowResource", 0);
    }

    public void UpdateArg(GameObject buildingText,Camera cam,int Tag, int index=0)
    {
        this.buildingText = buildingText;
        this.cam = cam;
        this.Tag = Tag;
        this.index = index;
    }


    void ToOpaqueMode(Material material)
    {
        material.shader = Shader.Find("Legacy Shaders/Diffuse");
        material.color = color.color;
    }
}
