using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;

    public void UpdateBar(float min,float max)
    {
        if (hpBar != null)
            hpBar.fillAmount = min / max;

    }
	
}
