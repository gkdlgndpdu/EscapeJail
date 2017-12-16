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
        maxArmor = -1;

        UpdateArmorUi();
    }

    public void SetArmor(int value)
    {
        //같은레벨 아이템 들어오면
        if (maxArmor >= value)
        {     
            MessageBar.Instance.ShowInfoBar("Item Overlap +100 gold",Color.white);
            GamePlayerManager.Instance.player.GetCoin(GameConstants.ItemOverlapGold);
            return;
        }
        maxArmor = value;
        remainArmor = maxArmor;    
        
        UpdateArmorUi();
    }

    public void FillArmor()
    {
        remainArmor = maxArmor;
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
