using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour
{
    public void Awake()
    {
        SoundManager.Instance.ChangeBgm("Select");

        GoogleCloudSave.instance.LoadPassiveFromCloud();

    }

    public void GoToMainMenu()
    {
        SceneManager.Instance.ChangeScene(SceneName.MenuScene);
    }

    
	
}
