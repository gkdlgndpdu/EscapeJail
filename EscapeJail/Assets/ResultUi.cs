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

    [SerializeField]
    private GameObject winningLotteryIcon;

    private ScoreCounter scoreCounter;

    private int totalScore = 0;


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

            int second = playTime % 60;
            int minute = playTime / 60;
            timeText.text = string.Format("{0}:{1}", minute, second);
        }
        if (bossKillText != null)
        {
            int bossKillScore = scoreCounter.BossKillNum * ScorePoint.BossPoint;
            totalScore += bossKillScore;
            bossKillText.text = bossKillScore.ToString();
        }
        if (monsterKillText != null)
        {
            int monsterKillScore = scoreCounter.MonsterKillNum * ScorePoint.EnemyPoint;
            totalScore += monsterKillScore;
            monsterKillText.text = monsterKillScore.ToString();

        }
        if (usingHeartText != null)
        {
            int heartMinus = scoreCounter.LostHeartNum * ScorePoint.HeartMinus * -1;
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

        if (clearStageText != null)
        {
            int clearScore = StagerController.Instance.NowStageLevel * ScorePoint.StagePoint;
            totalScore += clearScore;
            clearStageText.text = clearScore.ToString();

        }


        if (totalScoreText != null)
        {
            totalScore = Mathf.Clamp(totalScore, 0, int.MaxValue);
            if (NowSelectPassive.Instance.HasPassive(PassiveType.WinningLottery) == true)
            {
                if (winningLotteryIcon != null)
                    winningLotteryIcon.gameObject.SetActive(true);

                totalScore += (int)((float)totalScore * 1.3f);
            }
            else
            {
                if (winningLotteryIcon != null)
                    winningLotteryIcon.gameObject.SetActive(false);
            }
            totalScoreText.text = string.Format("Total : {0}", totalScore.ToString());
        }



    }



    private void OnEnable()
    {
        TimeManager.Instance.StopTime();
        totalScore = 0;
        UpdateUiTexts();
    }

    private void OnDisable()
    {
        TimeManager.Instance.ResumeTime();
    }


    public void EndGame()
    {
        //점수 등록
        GoogleService.Instance.ReportScore(totalScore);

        TimeManager.Instance.ResumeTime();

        GoogleService.Instance.SetCharacterAchivement(CharacterType.Scientist);

        if (StagerController.Instance.NowStageLevel < GameConstants.lastStageLevel)
        {
            SceneManager.Instance.ChangeScene(SceneName.MenuScene);
        }
        else if (StagerController.Instance.NowStageLevel >= GameConstants.lastStageLevel)
        {
            SceneManager.Instance.ChangeScene(SceneName.EndingScene);
        }
    }

}
