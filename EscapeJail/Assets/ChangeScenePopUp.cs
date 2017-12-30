using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScenePopUp : MonoBehaviour
{

    private void OnEnable()
    {
        TimeManager.Instance.StopTime();
    }

    public void OkButtonClick()
    {
        TimeManager.Instance.ResumeTime();
        this.gameObject.SetActive(false);
        if (StagerController.Instance.NowStageLevel < GameConstants.lastStageLevel)
        {
            GameManager.Instance.ChangeStage();
        }
        else if (StagerController.Instance.NowStageLevel >= GameConstants.lastStageLevel)
        {
            GamePlayerManager.Instance.player.playerUi.ResultUiOnOff(true);
        }

        

    }
    public void WaitButtonClickI()
    {
        TimeManager.Instance.ResumeTime();
        this.gameObject.SetActive(false);
    }
}
