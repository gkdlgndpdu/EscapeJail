using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuScene : MonoBehaviour
{
    [SerializeField]
    private GameObject difficultyWindow;
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

    public void SelectEasy()
    {
        NowSelectPassive.Instance.SetDifficulty(Difficulty.easy);
        SetDifficultyDescription("Easy Mode \nAutoAiming \nMore hp \nLess score \nEtc...");
    }
    private void SetDifficultyDescription(string text)
    {
        if (difficultyDescription == null) return;
        difficultyDescription.text = text;
    }
    public void SelectHard()
    {
        NowSelectPassive.Instance.SetDifficulty(Difficulty.hard);
        SetDifficultyDescription("Hard Mode \nAiming yourself \nLess hp \nMore score \nEtc...");
    }

}
