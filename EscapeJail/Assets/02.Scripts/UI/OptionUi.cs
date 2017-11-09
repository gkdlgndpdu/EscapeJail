using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUi : MonoBehaviour
{
    //쏘는방식 변경용
    [SerializeField]
    private Toggle fireStyleChange;
    [SerializeField]
    private GameObject autoStick;
    [SerializeField]
    private GameObject manualStick;

    public void ChangeFireStyle()
    {
        if (fireStyleChange == null) return;

        if (fireStyleChange.isOn == true)
        {     
            GameOption.ChangeFireStype(FireStyle.Auto);
            autoStick.SetActive(true);
            manualStick.SetActive(false);
        }
        else
        {         
            GameOption.ChangeFireStype(FireStyle.Manual);
            autoStick.SetActive(false);
            manualStick.SetActive(true);
        }

    }
}
