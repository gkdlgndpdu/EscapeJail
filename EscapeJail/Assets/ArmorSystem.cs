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
    private int armorLevel;

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
        armorLevel = level;
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
        if (inventoryUi == null || hpBar == null) return;

        float ratio = (float)remainArmor / (float)maxArmor;


            inventoryUi.SetArmorImage(armorLevel);
            inventoryUi.SetArmorUi(ratio);

      
            hpBar.SetArmorBar(ratio);
    }
	
}
