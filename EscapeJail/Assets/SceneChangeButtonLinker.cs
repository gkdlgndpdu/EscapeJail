using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButtonLinker : MonoBehaviour
{ 
    [SerializeField]
    private SceneName targetScene;

    [SerializeField]
    private GoogleService googleService;

    public void ButtonClick()
    {
        if (googleService == null) return;
        if (googleService.CanStart == false) return;
        SceneManager.Instance.ChangeScene(targetScene);
    }

  
}
