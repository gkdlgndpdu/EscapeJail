﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{   

    public HpBar hpBar;
    public InventoryUi inventoryUi;
    public WeaponUI weaponUi;
    public OptionUi optionUi;

    private void Awake()
    {
        //player에서 GameObject.find로 찾기때문에 건들면 안됨
         this.name = "PlayerUi";
    }
    public void SetHpBar(float min,float max)
    {
        if (hpBar != null)
            hpBar.SetHpBar(min , max);
    }

    public void OptionUiOnOff()
    {
        if (optionUi == null) return;
        optionUi.gameObject.SetActive(!optionUi.gameObject.activeSelf);
    }

}
