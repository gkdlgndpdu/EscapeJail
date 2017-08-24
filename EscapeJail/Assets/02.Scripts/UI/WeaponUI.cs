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

    public void SetWeaponUI(float minAmmo,float maxAmmo,string weaponName)
    {
        if (text == null || stringBuilder == null) return;

        stringBuilder.Length = 0;
        stringBuilder.AppendFormat("{0} \n {1}/{2}", weaponName, minAmmo, maxAmmo);
        text.text = stringBuilder.ToString();
    }
}
