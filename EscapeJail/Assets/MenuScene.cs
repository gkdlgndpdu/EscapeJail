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

  

    private void Start()
    {
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
        if (difficultyWindow == null) return;
        if (difficultyWindow.activeSelf == false)
        {
            if (NowSelectPassive.Instance.NowDifficulty == Difficulty.hard)
            {
                SelectHard();
            }
            else
            {
                SelectEasy();
            }
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

    public void SelectEasy()
    {
        NowSelectPassive.Instance.SetDifficulty(Difficulty.easy);
        SoundManager.Instance.PlaySoundEffect("Button");

        int languageKey = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);

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
        NowSelectPassive.Instance.SetDifficulty(Difficulty.hard);
        SoundManager.Instance.PlaySoundEffect("Button");

        int languageKey = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);

        //한글
        if (languageKey == 0)
        {
            SetDifficultyDescription("어려운 모드\n수동 조준 \n적은 체력 \n적은 점수 \n기타등등...", Color.red);
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
