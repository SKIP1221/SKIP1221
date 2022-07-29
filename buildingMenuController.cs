using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingMenuController : MonoBehaviour
{
    private buildSystem buildSystem;
    private HudManager hudManager;

    void Start()
    {
        buildSystem = GetComponent<buildSystem>();
        hudManager = GetComponent<HudManager>();
    }

    public void onChangeBuilding(int index)
    {
        buildSystem.build(index);
        hudManager.secondButton();
        hudManager.sButton = HudManager.secondButtonFuncs.cancel;
        hudManager.fButton = HudManager.firstButtonFuncs.putBuilding;
    }
}
