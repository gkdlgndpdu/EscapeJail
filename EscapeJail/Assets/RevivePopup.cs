using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RevivePopup : MonoBehaviour
{
    private Action reviveFunc;
    public Action ReviveFunc
    {
        set
        {
            reviveFunc =value;
        }
    }
    
    public void OkButtonClick()
    {
        if (reviveFunc == null) return;
        UnityAdsHelper.Instance.LinkFunc = reviveFunc;
        UnityAdsHelper.Instance.ShowRewardedAd();
    }
    public void NoButtonClick()
    {
        UiOnOff(false);
        //결과창 띄워줌
        GamePlayerManager.Instance.player.playerUi.ResultUiOnOff(true);
    }

    public void UiOnOff(bool OnOff)
    {
        this.gameObject.SetActive(OnOff);

        if (OnOff == true)
            Time.timeScale = 0.5f;
        else if (OnOff == false)
            Time.timeScale = 1f;
    }

}
