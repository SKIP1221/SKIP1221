using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drop : MonoBehaviour
{
    public string nameText;
    public AllObjects.Objects name=AllObjects.Objects.stone;
    public Vector3 offset;
    [HideInInspector]
    public GameObject dropText;
    private RectTransform textRect;
    [HideInInspector]
    public Camera camera;
    private bool showText;
    private void Start()
    {
       Outline _outline = this.gameObject.AddComponent<Outline>();
        _outline.OutlineColor = Color.white;
        _outline.OutlineWidth = 4;
        _outline.enabled = false;
        textRect = dropText.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (showText)
        {
            Vector3 pos = camera.WorldToScreenPoint(transform.position + offset);
            textRect.position = pos;
        }
    }

    public void ShowText(bool res)
    {
        Vector3 pos = camera.WorldToScreenPoint(transform.position + offset);
        textRect.position = pos;
        showText = res;
        dropText.SetActive(res);
        if (res)
            dropText.GetComponent<Text>().text = nameText;
    }
}
