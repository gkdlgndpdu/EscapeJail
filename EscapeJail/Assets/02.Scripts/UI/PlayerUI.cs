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
    public ResultUi resultUi;
    public ClawMachinePopup clawMachinePopup;
    public RevivePopup revivePopup;

    [SerializeField]
    private GameObject autoStick;
    [SerializeField]
    private GameObject manualStick;
    [SerializeField]
    private SkillButtonProgress skillButtonProgress;

  

    public void SetSkillButtonProgress(float min,float max)
    {
        if (skillButtonProgress != null)
            skillButtonProgress.SetProgress(min, max);
    }
    public void SetSkillButtonText(string text)
    {
        if (skillButtonProgress != null)
            skillButtonProgress.SetText(text);
    }

    private void Awake()
    {
        //player에서 GameObject.find로 찾기때문에 건들면 안됨
         this.name = "PlayerUi";
    }
  

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

    public void ChangeFireStyle(FireStyle style)
    {
        switch (style)
        {
            case FireStyle.Auto:
                {
                    autoStick.SetActive(true);
                    manualStick.SetActive(false);
                }
                break;
            case FireStyle.Manual:
                {
                    autoStick.SetActive(false);
                    manualStick.SetActive(true);
                }
                break;
        }

    }

    public void ResultUiOnOff(bool OnOff)
    {
        if (resultUi != null)
            resultUi.gameObject.SetActive(OnOff);
    }

}