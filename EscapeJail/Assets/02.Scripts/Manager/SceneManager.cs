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
}
  
