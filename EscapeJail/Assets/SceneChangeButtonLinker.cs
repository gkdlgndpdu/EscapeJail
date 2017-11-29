using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButtonLinker : MonoBehaviour
{ 
    [SerializeField]
    private SceneName targetScene;
  

    public void ButtonClick()
    {
        SceneManager.Instance.ChangeScene(targetScene);
    }

  
}
