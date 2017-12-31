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
    private string LeaderBoardID = "CgkIm6Xsx9oYEAIQAQ";
    [SerializeField]
    private GameObject loginScreen;
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
        if (PlayGamesPlatform.Instance.IsAuthenticated())
            PlayGamesPlatform.Instance.ShowLeaderboardUI();


    }
    public void ShowAchivement()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        else
            SignIn();


    }

    // Use this for initialization
    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate((bool success) =>
        {
            canStart = true;
            loginScreen.gameObject.SetActive(false);
            if (success == true)
            {
                ReadPrefAchivment();

            }
        });

    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }


    public void ReportScore(int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_score, null);
    }
    //실제용 함수
    public void SetCharacterAchivement(CharacterType characterType)
    {
        ReportSocial((int)characterType);
    }

    private void ReadPrefAchivment()
    {
        for (int i = 0; i < (int)CharacterType.CharacterEnd; i++)
        {
            string achivId = string.Empty;

            switch ((CharacterType)i)
            {

                case CharacterType.Scientist:
                    {
                        achivId = GPGSIds.achievement_get_a_scientist;
                    }
                    break;
                case CharacterType.Defender:
                    {
                        achivId = GPGSIds.achievement_get_a_defender;
                    }
                    break;
                case CharacterType.Sniper:
                    {

                        achivId = GPGSIds.achievement_get_a_sniper;
                    }
                    break;
                case CharacterType.Engineer:
                    {
                        achivId = GPGSIds.achievement_get_a_engineer;
                    }
                    break;
                case CharacterType.Trader:
                    {
                        achivId = GPGSIds.achievement_get_a_trader;
                    }
                    break;                  
            }

            var achivData = PlayGamesPlatform.Instance.GetAchievement(achivId);
            if (achivData != null)
            {
                if (achivData.IsUnlocked == true)
                {
                    DatabaseLoader.Instance.BuyCharacter((CharacterType)i);

                }
            }



        }
    }
    public bool IsAchivementClear(string AchiveId)
    {        
        var achivData = PlayGamesPlatform.Instance.GetAchievement(AchiveId);
        if (achivData != null)
        {
            if (achivData.IsUnlocked == true)
            {
                return true;
            }
            else if (achivData.IsUnlocked == false)
            {
                return false;
            }
        }
        else if (achivData == null)
        {
            return false;
        }
        return false;
    }
    public void UnlockHardMode()
    {
        if (IsAchivementClear(GPGSIds.achievement_hard_mode) == true) return;
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_hard_mode, (bool success) =>
        {
            if (success == true)
            {
                Debug.Log("hardmodeUnlick");
            }
        });
    }

    //테스트용함수
    public void ReportSocial(int type)
    {
        CharacterType characterType = (CharacterType)type;

        CharacterDB getData = DatabaseLoader.Instance.GetCharacterDB(characterType);
        if (getData != null)
        {
            if (getData.hasCharacter == true)
                return;
        }

        string achivId = string.Empty;

        switch (characterType)
        {

            case CharacterType.Scientist:
                {
                    achivId = GPGSIds.achievement_get_a_scientist;
                }
                break;
            case CharacterType.Defender:
                {
                    achivId = GPGSIds.achievement_get_a_defender;
                }
                break;
            case CharacterType.Sniper:
                {

                    achivId = GPGSIds.achievement_get_a_sniper;
                }
                break;
            case CharacterType.Engineer:
                {
                    achivId = GPGSIds.achievement_get_a_engineer;
                }
                break;
            case CharacterType.Trader:
                {
                    achivId = GPGSIds.achievement_get_a_trader;
                } break;
        }

        if (achivId == string.Empty)
            return;


        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            if (characterType != CharacterType.Trader)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(achivId, 1, (bool success) =>
                {
                    if (success == true)
                    {
                        //완료했는지 체크
                        var achivData = PlayGamesPlatform.Instance.GetAchievement(achivId);
                        if (achivData != null)
                        {
                            if (achivData.IsUnlocked == true)
                            {
                                DatabaseLoader.Instance.BuyCharacter(characterType);

                            }
                            else if (achivData.IsUnlocked == false)
                            {
                                //아무것도안해
                            }
                        }

                    }
                });
            }
         else if(characterType==CharacterType.Trader)
            {
                PlayGamesPlatform.Instance.UnlockAchievement(achivId, (bool success) =>
                 {
                     if (success == true)
                     {

                         DatabaseLoader.Instance.BuyCharacter(characterType);

                     }
                 });
            }
        }


    }





}
