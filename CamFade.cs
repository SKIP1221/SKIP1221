using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamFade : MonoBehaviour
{

    public GameObject Trigger;
    Vector3 cast;
    public List<GameObject> lastMaterials = new List<GameObject>();
    public List<GameObject> nowMaterials = new List<GameObject>();




    void ToTransparentMode(Material material)
    {
        material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
    }


    void ToOpaqueMode(Material material)
    {
        material.shader = Shader.Find("Legacy Shaders/Diffuse");
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cast = (Trigger.transform.position + Vector3.up) - transform.position;
        var hits = Physics.SphereCastAll(transform.position, 1, cast, Vector3.Distance(Trigger.transform.position + Vector3.up, transform.position) - 1);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.layer!=6)
            {
                var mats = hit.collider.gameObject.GetComponent<Renderer>().materials;
                foreach (var item in mats)
                {
                    if (item.color.a != 0.3f)
                        ToTransparentMode(item);
                }
                nowMaterials.Add(hit.collider.gameObject);
            }
        }
        foreach (var item in lastMaterials)
        {
            if (nowMaterials.IndexOf(item)==-1)
            {
                var mat = item.GetComponent<Renderer>().materials;
                foreach (var x in mat)
                {
                    ToOpaqueMode(x);
                }
            }
        }
        lastMaterials = nowMaterials;
        nowMaterials = new List<GameObject>();
    }
}
