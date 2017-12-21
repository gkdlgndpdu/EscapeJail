using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class WeaponUI : MonoBehaviour
{
    private Text text;
    private StringBuilder stringBuilder;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        stringBuilder = new StringBuilder();
    }

    public void SetWeaponUI(int minAmmo,int maxAmmo,string weapontype)
    {
        if (text == null || stringBuilder == null) return;

        if (maxAmmo == 1)
        {
            stringBuilder.Length = 0;
            stringBuilder.AppendFormat("{0} \n Infinite", weapontype);
            text.text = stringBuilder.ToString();
        }
        else
        {
            stringBuilder.Length = 0;
            stringBuilder.AppendFormat("{0} \n {1} / {2}", weapontype, minAmmo, maxAmmo);
            text.text = stringBuilder.ToString();
        }
       // ∞
    
    }

    public void SetWeaponUiDefault()
    {
        if (text == null || stringBuilder == null) return;
        stringBuilder.Length = 0;
        stringBuilder.AppendFormat("Hand \n {0} / {1}",  1, 1);
        text.text = stringBuilder.ToString();

    }
}
