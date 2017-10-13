using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    LobbyScene,
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
    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        //임시코드
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {        
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SceneName.LobbyScene);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SceneName.GameScene);
        
        }
    }
}
