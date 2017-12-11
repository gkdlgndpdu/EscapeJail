using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResultUi : MonoBehaviour
{
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text bossKillText;
    [SerializeField]
    private Text monsterKillText;
    [SerializeField]
    private Text usingHeartText;
    [SerializeField]
    private Text clearRoomText;
    [SerializeField]
    private Text clearStageText;
    [SerializeField]
    private Text earingMedalText;

    [SerializeField]
    private Text totalScoreText;

    private ScoreCounter scoreCounter;

 

    private void UpdateUiTexts()
    {
        if (scoreCounter == null) return;

        //시간
        if (timeText != null)
        {
            int playTime = TimeManager.Instance.PlayTime;
            timeText.text = string.Format("{0} seconds",playTime);
        }
        if (bossKillText != null)
            bossKillText.text = scoreCounter.BossKillNum.ToString();
        if (monsterKillText != null)
            monsterKillText.text = scoreCounter.MonsterKillNum.ToString();
        if (usingHeartText != null)
            usingHeartText.text = scoreCounter.LostHeartNum.ToString();
        if (clearRoomText != null)
            clearRoomText.text = scoreCounter.ClearRoomNum.ToString();
        if (earingMedalText != null)
            earingMedalText.text = scoreCounter.EarningMedals.ToString();
    }

    private Action linkFunc;
    public Action LinkFunc
    {
        set
        {
            linkFunc = value;
        }
    }
    

    private void Start()
    {
        this.gameObject.SetActive(false);
        scoreCounter = GamePlayerManager.Instance.scoreCounter;
    }
    private void OnEnable()
    {
        TimeManager.Instance.StopTime();
        UpdateUiTexts();
    }

    private void OnDisable()
    {
        TimeManager.Instance.ResumeTime();
    }

    public void LinkedFuncExecute()
    {
        if (linkFunc != null)
        {
            UnityAdsHelper.Instance.RevivalFunc = linkFunc;

        }
        UnityAdsHelper.Instance.ShowRewardedAd();

     
    }

}
