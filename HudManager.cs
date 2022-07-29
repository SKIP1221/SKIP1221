using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [HideInInspector]
    public enum firstButtonFuncs
    {
        attack,
        useObject,
        putBuilding
    }
    [HideInInspector]
    public enum secondButtonFuncs
    {
        buildMenu,
        cancel
    }
    public firstButtonFuncs fButton = firstButtonFuncs.attack;
    public secondButtonFuncs sButton = secondButtonFuncs.buildMenu;


    private buildSystem buildSystem;
    private PlayerController playerController;
    private SceneManager sceneManager;

    public GameObject BuildMenu;
    public GameObject Hud;
    public GameObject Inventory;

    private void Start()
    {
        sceneManager = GetComponent<SceneManager>();
        playerController = sceneManager.character.GetComponent<PlayerController>();
        buildSystem = GetComponent<buildSystem>();
    }


    public void firstButton()
    {
        switch (fButton)
        {
            case firstButtonFuncs.attack:
                if (playerController.canRun)
                {
                    StartCoroutine(playerController.Kick());
                }
                break;
            case firstButtonFuncs.useObject:
                break;
            case firstButtonFuncs.putBuilding:
                buildSystem.putBuilding();
                fButton = firstButtonFuncs.attack;
                sButton = secondButtonFuncs.buildMenu;
                playerController.Speed = 5;
                break;
        }
    }


    public void secondButton()
    {
        switch (sButton)
        {
            case secondButtonFuncs.buildMenu:
                BuildMenu.SetActive(!BuildMenu.activeSelf);
                Inventory.SetActive(!BuildMenu.activeSelf);
                Hud.SetActive(!BuildMenu.activeSelf);
                playerController.canRun = !BuildMenu.activeSelf;
                playerController.isRun = !BuildMenu.activeSelf;
                playerController.Speed = 2;
                break;
            case secondButtonFuncs.cancel:
                buildSystem.destroy();
                playerController.Speed = 5;
                sButton = secondButtonFuncs.buildMenu;
                fButton = firstButtonFuncs.attack;
                break;
        }
    }

}
