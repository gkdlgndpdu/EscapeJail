using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArmorSystem
{
    private int remainArmor = 0;
    private int maxArmor = 0;

    private InventoryUi inventoryUi;
    private PlayerUI playerUi;
    Action<int> updateFunc;

    public ArmorSystem(InventoryUi InventroyUi, PlayerUI playerUi, Action<int> updateFunc)
    {
        this.inventoryUi = InventroyUi;
        this.playerUi = playerUi;
        this.updateFunc = updateFunc;
        
        remainArmor = 0;
        maxArmor = 0;

        UpdateArmorUi();
    }

    public void SetArmor(int level, int value)
    {

        if (remainArmor > value) return;
#if UNITY_EDITOR
        Debug.Log("Gain Armor " + value.ToString());
#endif

        remainArmor = value;
        maxArmor = remainArmor;
        
        UpdateArmorUi();
    }

    public void UseArmor(int damage)
    {
        remainArmor -= damage;
        UpdateArmorUi();
        SoundManager.Instance.PlaySoundEffect("vestshieldhit");
    }

    public bool hasArmor()
    {
        return remainArmor > 0;
    }

    private void UpdateArmorUi()
    {
        if (updateFunc == null) return;
        updateFunc(remainArmor);

       
    }

}
