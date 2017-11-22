using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUi : MonoBehaviour
{
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private Text armorText;

    public void SetHp(int hp)
    {
        if (hpText == null) return;
        hpText.text = hp.ToString();

    }
    public void SetArmor(int armor)
    {
        if (armorText == null) return;
        armorText.text = armor.ToString();
    }

}
