using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSystem 
{
    private int remainArmor=0;
    private int maxArmor=0;

    private InventoryUi inventoryUi;
    private PlayerUI playerUi;
    private HpBar hpBar;

    public ArmorSystem(InventoryUi InventroyUi,PlayerUI playerUi,HpBar hpBar)
    {
        this.inventoryUi = InventroyUi;
        this.playerUi = playerUi;
        this.hpBar = hpBar;

        remainArmor = 0;
        maxArmor = 0;

        UpdateArmorUi();
    }
    
    public void SetArmor(int level)
    {
        remainArmor = level * 3;
        maxArmor = remainArmor;

        UpdateArmorUi();
    }

    public void UseArmor(int damage)
    {
        remainArmor -= damage;
        UpdateArmorUi();
    }

    public bool hasArmor()
    {
        return remainArmor > 0;
    }

    private void UpdateArmorUi()
    {
        float ratio = (float)remainArmor / (float)maxArmor;

        if (inventoryUi != null)
            inventoryUi.SetArmorUi(ratio);

        if (hpBar != null)
            hpBar.SetArmorBar(ratio);
    }
	
}
