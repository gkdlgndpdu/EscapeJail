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
        SetDifficultyDescription("Normal Mode \nAuto Aiming \nMore hp \nLess score \nEtc...",Color.green);
    }
    private void SetDifficultyDescription(string text,Color color)
    {
        if (difficultyDescription == null) return;
        difficultyDescription.text = text;
        difficultyDescription.color = color;
    }
    public void SelectHard()
    {
        NowSelectPassive.Instance.SetDifficulty(Difficulty.hard);
        SoundManager.Instance.PlaySoundEffect("Button");
        SetDifficultyDescription("Hard Mode \nManual Aiming \nLess hp \nMore score \nEtc...",Color.red);
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
