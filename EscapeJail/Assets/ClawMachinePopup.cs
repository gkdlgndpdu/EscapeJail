using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClawMachinePopup : MonoBehaviour
{
    private Action linkFunc;


    public void LinkFuncAndOpenPopup(Action linkFunc)
    {
        this.linkFunc = linkFunc;
        this.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        TimeManager.Instance.StopTime();
      
    }
    private void OnDisable()
    {
        TimeManager.Instance.ResumeTime();
    
    }

    public void OkButtonClick()
    {
        if (this.linkFunc == null) return;
        UnityAdsHelper.Instance.LinkFunc = linkFunc;
        UnityAdsHelper.Instance.ShowRewardedAd();

        this.gameObject.SetActive(false);
    }
    public void NoButtonClick()
    {
        this.gameObject.SetActive(false);
    }
	
}
