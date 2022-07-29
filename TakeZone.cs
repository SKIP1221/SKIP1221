using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeZone : MonoBehaviour
{
    public List<GameObject> zoneObjects = new List<GameObject>();
    public GameObject raycastPlace;
    public GameObject AimObject;
    public float zoneRadius, zoneLenght;
    bool isBuilding;

    private void Update()
    {
        float dist = zoneLenght+1;
        GameObject newAim = null;
        bool newIsBuilding=false;
        RaycastHit[] hits = Physics.SphereCastAll(raycastPlace.transform.position, zoneRadius, transform.forward, zoneLenght,1111, QueryTriggerInteraction.UseGlobal);
        foreach (var hit in hits)
        {
            if (((!hit.collider.isTrigger && Vector3.Distance(transform.position + Vector3.up, hit.point)<dist) ||
                (hit.collider.isTrigger && hit.transform.tag=="building" && Vector3.Distance(raycastPlace.transform.position, hit.point) < dist)) && hit.transform.tag!="Player")
            {
                newAim = hit.transform.gameObject;
                newIsBuilding = hit.collider.isTrigger;
            }
        }
        if (AimObject!=newAim)
        {
            if (isBuilding)
                AimObject.GetComponent<buildingController>().ShowText(false);

            AimObject = newAim;
            isBuilding = newIsBuilding;

            if (isBuilding)
                AimObject.GetComponent<buildingController>().ShowText(true);
        }


    }
}
