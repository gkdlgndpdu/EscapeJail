using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public static BossHpBar Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    
    }
    [SerializeField]
    private Image hpBar; 

    public void UpdateBar(float min,float max)
    {
        if (hpBar != null)
            hpBar.fillAmount = min / max;
        
    }

    private void OnDestroy()
    {
        if (Instance != null)
            Instance = null;
    }

}
