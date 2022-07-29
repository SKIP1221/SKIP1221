using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject character;
    public Canvas canvas;
    public GameObject healthBar;
    public GameObject dropText;
    public Camera cam;

    public GameObject[] Objects;
    public string[] ObjectsNameEN;
    public string[] ObjectsNameRU;

    [HideInInspector]
    public string language = "RU";

    private void Start()
    {
        PlayerPrefs.SetString("language", "RU");
        language = PlayerPrefs.GetString("language");
    }
}
