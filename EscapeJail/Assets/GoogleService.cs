using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using System.Text;

//업적 ui 여는법 Social.ShowAchievementsUI()  로그인이 안돼있을경우 로그인 재요청하는 예외처리 필요

//리더보드의 UI를 열려면
//Social.ShowLeaderboardUI()
//PlayGamesPlatform.Instance.ShowLeaderboardUI()
//GameCenterPlatform.ShowLeaderboardUI("순위표 고유 ID", TimeScope)
//를 상황에 맞게 호출
//* 마찬가지로, 소셜에 로그인이 되어있지 않다면 로그인 후 재요청하는 예외처리 필요.
//GameCenterPlatform.ShowLeaderboardUI("순위표 고유 ID", TimeScope) 이함수 마지막인자 allTime (전체기간순위) today 오늘순위 week 이번주 순위




public class GoogleService : MonoBehaviour
{
    private bool canStart = false;
    public static GoogleService Instance;
    public bool CanStart
    {
        get
        {
            return canStart;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }  

    public void ShowLeaderBoardUi()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    // Use this for initialization
    void Start()
    {
        PlayGamesPlatform.Instance.Authenticate((bool success) =>
            {
                if (success == true)
                {
               
                }
                else
                {
               
                }
#if UNITY_EDITOR
                Debug.Log("로그인 시도 완료");
#endif

                canStart = true;
            });

    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    //업적
    public void UnlockAchievement(int score)
    {
        if (score >= 100)
        {
#if UNITY_ANDROID
                                                        //GPGSIDS.cs 에있는 고유 is입력 //0f-100f 업적 진행도 //세번째는 콜백함수(필요없을떄는 null)
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.leaderboard_score, 100f, null);

#endif
        }
    }


    public void ReportScore(int score)
    {  

#if UNITY_ANDROID
        //점수 , "리더보드 고유 id",callback
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_score, (bool success) =>
        {
            if (success)
            {             
             //   ShowLeaderBoardUi();
            }
            else
            {
         
            }
        });

#endif
    }


  


}
