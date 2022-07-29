using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildSystem : MonoBehaviour
{
    public GameObject[] buildings = {};

    [HideInInspector]
    public GameObject nowBuilding;
    private int buildingIndex;

    public GameObject buildingPlace;

    private int buildingsCount = 0;

    public GameObject buildingText;

    private void Start()
    {
        buildingsCount = PlayerPrefs.GetInt("buildingsCount");
        for (int i = 0; i < buildingsCount; i++)
        {
            if (PlayerPrefs.GetFloat("building" + i + ".health")>0)
            {
                Vector3 pos = new Vector3(PlayerPrefs.GetFloat("building" + i + ".position.x"), PlayerPrefs.GetFloat("building" + i + ".position.y"), PlayerPrefs.GetFloat("building" + i + ".position.z"));
                Quaternion rotation = new Quaternion(PlayerPrefs.GetFloat("building" + i + ".quaternion.x"), PlayerPrefs.GetFloat("building" + i + ".quaternion.y"), PlayerPrefs.GetFloat("building" + i + ".quaternion.z"), PlayerPrefs.GetFloat("building" + i + ".quaternion.w"));
                int index = PlayerPrefs.GetInt("building" + i + ".index");
                GameObject building = Instantiate(buildings[index], pos, rotation) as GameObject;
                buildingController _building = building.GetComponent<buildingController>();
                _building.UpdateArg(buildingText, GetComponent<SceneManager>().cam, i);
            }
        }
    }

    public void build(int index)
    {
        nowBuilding = Instantiate(buildings[index],Vector3.zero, buildingPlace.transform.parent.transform.rotation, buildingPlace.transform);
        nowBuilding.transform.localPosition = buildings[index].GetComponent<buildingController>().offset;
        buildingIndex = index;
        buildingController _building = nowBuilding.GetComponent<buildingController>();
        _building.UpdateArg(buildingText, GetComponent<SceneManager>().cam, buildingsCount, index);
    }

    private void Update()
    {
        if (nowBuilding)
        {
            RaycastHit[] ray;
            ray = Physics.RaycastAll(buildingPlace.transform.position, Vector3.down, 3);
            foreach (var hit in ray)
            {
                if (hit.transform.tag == "Ground")
                {
                    buildingPlace.transform.position = hit.point;
                    break;
                }
            }
        }
    }

    public void destroy()
    {
        Destroy(nowBuilding);
        nowBuilding = null;
    }

    public void putBuilding()
    {
        nowBuilding.transform.parent = null;
        buildingsCount++;
        nowBuilding = null;
    }
}
