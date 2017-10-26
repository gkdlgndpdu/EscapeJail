using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    private Image hudImage;


    private void Awake()
    {
        hudImage = GetComponentInChildren<Image>();
    }

    public void SetHpBar(float min,float max)
    {
        if (hudImage != null)
            hudImage.fillAmount = (float)min / (float)max;
    }

}
