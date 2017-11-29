using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    LobbyScene,
    SelectScene,
    GameScene
}
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void ChangeScene(SceneName sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)sceneName);

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeScene(SceneName.LobbyScene);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeScene(SceneName.SelectScene);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeScene(SceneName.GameScene);
        }
    }
}
  
