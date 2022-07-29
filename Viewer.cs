using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    List<GameObject> drop = new List<GameObject>();
    public GameObject viewDrop=null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="drop")
        {
            drop.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "drop")
        {
            drop.Remove(other.gameObject);
            if (other.gameObject==viewDrop)
            {
                viewDrop.GetComponent<Outline>().enabled = false;
                viewDrop.GetComponent<Drop>().ShowText(false);
                viewDrop = null;
            }
        }
    }

    private void Update()
    {
        if (drop.Count>0)
        {
            float dist = 99;
            GameObject newviewDrop = null;
            foreach (GameObject item in drop)
            {
                if (Vector3.Distance(item.transform.position, transform.position) < dist)
                {
                    if (item.activeSelf)
                    {
                        dist = Vector3.Distance(item.transform.position, transform.position);
                        newviewDrop = item;
                    }
                    else
                    {
                        drop.Remove(item);
                    }
                }
            }
            if (viewDrop != newviewDrop)
            {
                if (viewDrop != null)
                {
                    viewDrop.GetComponent<Outline>().enabled = false;
                    viewDrop.GetComponent<Drop>().ShowText(false);
                }
                viewDrop = newviewDrop;

                if (viewDrop != null)
                {
                    viewDrop.GetComponent<Outline>().enabled = true;
                    viewDrop.GetComponent<Drop>().ShowText(true);
                }
            }
        }
    }
}
