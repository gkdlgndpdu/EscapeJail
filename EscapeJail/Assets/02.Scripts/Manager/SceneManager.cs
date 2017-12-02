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
            Application.targetFrameRate = 120;
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

}

