using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuScene : MonoBehaviour
{
    [SerializeField]
    private GameObject difficultyWindow;
    [SerializeField]
    private GameObject optionWindow;
    [SerializeField]
    private GameObject shopWindow;
    [SerializeField]
    private GameObject tutorialWindow;
    [SerializeField]
    private Text difficultyDescription;
    [SerializeField]
    private GameObject creditUi;
    [SerializeField]
    private GameObject askWindow;

    [SerializeField]
    private Button startButton;

    private int originSize;

    private void Awake()
    {
        SoundManager.Instance.ChangeBgm("Menu");  
    }

    private void Start()
    {
        originSize = difficultyDescription.fontSize;
        //BGM
        SelectEasy();
    }
    public void StartGame()
    {
        SceneManager.Instance.ChangeScene(SceneName.SelectScene);
    }

    //난이도설정창
    public void DifficultySelectWindowOnOff()
    {
        //게임이 처음이면 듀토리얼 볼건지 확인
        if (PlayerPrefs.GetInt(PlayerPrefKeys.FirstPlayKey, 1) == 1)
        {
            AskWindowOnOff();           
        }

        if (difficultyWindow == null) return;
        if (difficultyWindow.activeSelf == false)
        {
            SelectEasy();
      
        }
        difficultyWindow.SetActive(!difficultyWindow.activeSelf);

  
    }
    public void OptionWindowOnOff()
    {
        if (optionWindow == null) return;
        optionWindow.SetActive(!optionWindow.gameObject.activeSelf);
    }
    public void ShopWindowOnOff()
    {
        if (shopWindow == null) return;
        shopWindow.SetActive(!shopWindow.gameObject.activeSelf);
    }
    public void TutorialWindowOnOff()
    {
        if (tutorialWindow == null) return;
        tutorialWindow.SetActive(!tutorialWindow.gameObject.activeSelf);
    }
    public void CreditWindowOnOff()
    {
        if (creditUi == null) return;
        creditUi.SetActive(!creditUi.activeSelf);
    }

    public void AskWindowOnOff()
    {
        if (askWindow == null) return;
        if (askWindow.activeSelf == true)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.FirstPlayKey, 0);
        }
        askWindow.SetActive(!askWindow.activeSelf);
    }
    public void AskWindowOkButtonClick()
    {
        AskWindowOnOff();
        TutorialWindowOnOff();
    }

    public void SelectEasy()
    {
        NowSelectPassive.Instance.SetDifficulty(Difficulty.easy);
        SoundManager.Instance.PlaySoundEffect("Button");

        int languageKey = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);

        if (startButton != null)
            startButton.gameObject.SetActive(true);

        //한글
        if (languageKey == 0)
        {
            SetDifficultyDescription("일반 모드 \n자동 조준 \n많은 체력 \n적은 점수 \n기타등등...", Color.green);
        }
        //영어
        else
        {
            SetDifficultyDescription("Normal Mode \nAuto Aiming \nMore hp \nLess score \nEtc...", Color.green);
        }

      
    }
 
    public void SelectHard()
    {
        int languageKey = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);

        //하드모드 열렸는지체크
        if (GoogleService.Instance.IsAchivementClear(GPGSIds.achievement_hard_mode) == false)
        {
            if (startButton != null)
                startButton.gameObject.SetActive(false);

            //한글
            if (languageKey == 0)
            {
                SetDifficultyDescription("노말모드를 먼저 클리어 하세요", Color.red);
            }
            //영어
            else
            {
                SetDifficultyDescription("Please clear the normal mode first.", Color.red);
            }

            return;
        }


        NowSelectPassive.Instance.SetDifficulty(Difficulty.hard);
        SoundManager.Instance.PlaySoundEffect("Button");

      

        //한글
        if (languageKey == 0)
        {
            SetDifficultyDescription("어려운 모드\n수동 조준 \n적은 체력 \n많은 점수 \n기타등등...", Color.red);
        }
        //영어
        else
        {
            SetDifficultyDescription("Hard Mode \nManual Aiming \nLess hp \nMore score \nEtc...", Color.red);
        }

       
    }

    private void SetDifficultyDescription(string text, Color color)
    {
        if (difficultyDescription == null) return;

        if (Language.Instance.NowLanguage == LanguageType.Korean)
        {
            difficultyDescription.font = Language.Instance.KoreanFont;
            difficultyDescription.fontSize = originSize+ 5;
        }
        else
        {
            difficultyDescription.font = Language.Instance.EnglishFont;
            difficultyDescription.fontSize = originSize ;
        }

            difficultyDescription.text = text;
        difficultyDescription.color = color;
    }
  

    public void ShowRanking()
    {
        GoogleService.Instance.ShowLeaderBoardUi();
    }
    public void ShowAchivement()
    {
        GoogleService.Instance.ShowAchivement();

    }

}
