using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{   

   // public HpBar hpBar;
    public InventoryUi inventoryUi;
    public WeaponUI weaponUi;
    public OptionUi optionUi;
    public HpUi hpUi;
    public GoodsUi goodsUi;

    private void Awake()
    {
        //player에서 GameObject.find로 찾기때문에 건들면 안됨
         this.name = "PlayerUi";
    }
    //public void SetHpBar(float min,float max)
    //{
    //    if (hpBar != null)
    //        hpBar.SetHpBar(min , max);
    //}

    public void SetHp(int value)
    {
        if (hpUi == null) return;
        hpUi.SetHp(value);

    }
    public void SetArmor(int value)
    {
        if (hpUi == null) return;
        hpUi.SetArmor(value);
    }

    public void OptionUiOnOff()
    {
        if (optionUi == null) return;
        optionUi.gameObject.SetActive(!optionUi.gameObject.activeSelf);
    }

}
