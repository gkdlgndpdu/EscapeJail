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

    private int totalScore  =0;


    private void UpdateUiTexts()
    {
        if (scoreCounter == null)
        {
            scoreCounter = GamePlayerManager.Instance.scoreCounter;
        }
        totalScore = 0;
        //시간
        if (timeText != null)
        {
            int playTime = TimeManager.Instance.PlayTime;
            timeText.text = string.Format("{0} seconds", playTime);
        }
        if (bossKillText != null)
        {
            int bossKillScore = scoreCounter.BossKillNum * ScorePoint.BossPoint;
            totalScore += bossKillScore;
            bossKillText.text = bossKillScore.ToString() ;
        }
        if (monsterKillText != null)
        {
            int monsterKillScore = scoreCounter.MonsterKillNum * ScorePoint.EnemyPoint;
            totalScore += monsterKillScore;
            monsterKillText.text = monsterKillScore.ToString();

        }
        if (usingHeartText != null)
        {
            int heartMinus = scoreCounter.LostHeartNum * ScorePoint.HeartMinus *-1;
            totalScore += heartMinus;
            usingHeartText.text = heartMinus.ToString(); ;

        }
        if (clearRoomText != null)
        {
            int clearRoomPoint = scoreCounter.ClearRoomNum * ScorePoint.ClearRoom;
            totalScore += clearRoomPoint;
            clearRoomText.text = clearRoomPoint.ToString();

        }
        if (earingMedalText != null)
        {
            earingMedalText.text = scoreCounter.EarningMedals.ToString();

        }
        if (totalScoreText != null)
        {
            totalScore = Mathf.Clamp(totalScore, 0, int.MaxValue);
            totalScoreText.text = totalScore.ToString();
        }

    
       
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

    //임시코드
    public void ReviveInstantly()
    {
        if (linkFunc != null)
        {
            linkFunc();

        }
    }

    public void EndGame()
    {
        //점수 등록
        GoogleService.Instance.ReportScore(totalScore);

        TimeManager.Instance.ResumeTime();

        SceneManager.Instance.ChangeScene(SceneName.MenuScene);
    }

}
