using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : MonoBehaviour
{
    public void Awake()
    {
        SoundManager.Instance.ChangeBgm("Select");
        //클라우드에 저장
      //  GoogleCloudSave.instance.SaveMyPassivies();
     //   GoogleCloudSave.instance.LoadPassiveFromCloud();

    }

    public void GoToMainMenu()
    {
        SceneManager.Instance.ChangeScene(SceneName.MenuScene);
    }

    
	
}
