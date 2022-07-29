using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenItem : MonoBehaviour
{
    public float health=100;

    private float nowHealth;

    public List<Item> Items = new List<Item>();

    public float barViewTime = 3;
    private float nowtime = 3;
    private bool barIsCreate=false;

    private GameObject healthBar;
    private GameObject hud;
    private Camera cam;
    public SceneManager sceneManager;

    private void Start()
    {
        LoadHealth();

        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        healthBar = sceneManager.healthBar;
        hud = sceneManager.GetComponent<HudManager>().Hud;
        cam = sceneManager.cam;

    }

    void LoadHealth()
    {
        if (PlayerPrefs.GetInt(transform.ToString()+"isCreated")==0)
        {
            PlayerPrefs.SetInt(transform.ToString() + "isCreated", 1);
            PlayerPrefs.SetFloat(transform.ToString(), health);
            nowHealth = health;
            print("create");
        }
        else
        {
            nowHealth = PlayerPrefs.GetFloat(transform.ToString());
            if (nowHealth <= 0)
                Destroy(this.gameObject);
        }
    }


    private void Update()
    {
        if (barIsCreate)
        {
            if (nowtime > 0)
            {
                Vector3 pos = cam.WorldToScreenPoint(transform.position + new Vector3(0, 3, 0));
                nowtime -= Time.deltaTime;
                healthBar.GetComponent<RectTransform>().position = pos;
            }
            else if (healthBar.activeSelf)
                healthBar.SetActive(false);
        }
    }


    public void Broke(float val)
    {
        Vector3 pos = cam.WorldToScreenPoint(transform.position + new Vector3(0, 3, 0));
        if (!barIsCreate)
        {
            healthBar = Instantiate(healthBar, pos, Quaternion.identity, hud.transform);
            healthBar.GetComponent<Slider>().maxValue = health;
            barIsCreate = true;
        }
        healthBar.GetComponent<RectTransform>().position = pos;
        healthBar.SetActive(true);
        nowtime = barViewTime;
        nowHealth -= val;
        PlayerPrefs.SetFloat(transform.ToString(), nowHealth);
        healthBar.GetComponent<Slider>().value = nowHealth;
        if (nowHealth <= 0)
        {
            foreach (Item item in Items)
            {
                int random = Random.Range(1, 101);
                GameObject newObject = sceneManager.Objects[(int)item.Object];
                if (random <= item.chance)
                {
                    random = Random.Range(item.min, item.max + 1);
                    for (int i = 0; i < random; i++)
                    {
                        GameObject drop = Instantiate(newObject, transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-1, 1)), Quaternion.Euler(0, Random.Range(0, 180), 0)) as GameObject;
                        Drop _drop = drop.GetComponent<Drop>();
                        _drop.nameText = sceneManager.language == "RU" ? sceneManager.ObjectsNameRU[(int)item.Object] : sceneManager.ObjectsNameEN[(int)item.Object];
                        _drop.dropText = sceneManager.dropText;
                        _drop.camera = sceneManager.cam;
                    }
                }
            }
            Destroy(healthBar);
            this.gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class Item
{
    public AllObjects.Objects Object;
    [Range(1, 100)]
    public int chance = 50;
    public int min = 1, max = 1;
}