using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public HpBar hpBar;
    
    public void Awake()
    {
        hpBar = GetComponentInChildren<HpBar>();
    }

    public void SetHpBar(float min,float max)
    {
        if (hpBar != null)
            hpBar.SetHpBar(min , max);
    }

}
