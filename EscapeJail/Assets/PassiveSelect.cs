using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject passiveUiScreen;
    public void PassiveUiOnOff()
    {
        if (passiveUiScreen == null) return;
        passiveUiScreen.SetActive(!passiveUiScreen.activeSelf);
    }

    public void GameStart()
    {
        SceneManager.Instance.ChangeScene(SceneName.StoryScene);
    }

}
