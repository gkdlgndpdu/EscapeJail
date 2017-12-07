using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReboundProgress : MonoBehaviour
{
    [SerializeField]
    private Image fore;

    public void SetProgress(float min , float max)
    {
        if (fore != null)
            fore.fillAmount = min / max;
    }


}
