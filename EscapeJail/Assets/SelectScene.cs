using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour
{
    public void Awake()
    {
        SoundManager.Instance.ChangeBgm("Select");
    }
    public void GoToMainMenu()
    {
        SceneManager.Instance.ChangeScene(SceneName.MenuScene);
    }
	
}
