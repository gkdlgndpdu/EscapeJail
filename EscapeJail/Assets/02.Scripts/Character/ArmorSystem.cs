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

    public void SetArmorFull()
    {
        if (maxArmor == 0) return;
        remainArmor = maxArmor;
        UpdateArmorUi();
        if (Language.Instance.NowLanguage == LanguageType.English)
            MessageBar.Instance.ShowInfoBar("Armor repaired", Color.white);
        else
            MessageBar.Instance.ShowInfoBar("조끼가 회복됐습니다.", Color.white);

    }

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
            if (Language.Instance.NowLanguage == LanguageType.English)
                MessageBar.Instance.ShowInfoBar("Item Overlap +100 gold", Color.white);
            else
                MessageBar.Instance.ShowInfoBar("중복(하위)아이템 획득 +100 gold", Color.white);

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
