using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public enum SceneName
{
    LobbyScene,
    MenuScene,
    SelectScene,
    StoryScene,
    GameScene,
    EndingScene
}
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    [SerializeField]
    private Image fadeMask;
    private SceneName nowSceneName;
    public SceneName NowSceneName
    {
        get
        {
            return nowSceneName;
        }

    }
    private bool nowChangeScene =false;

    void Awake()
    {
        if (Instance == null)
        {
            Application.targetFrameRate = 60;
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
        if (nowChangeScene == true)
            StopAllCoroutines();

        nowSceneName = sceneName;
        StartCoroutine(FadeRoutine());
        
        if(sceneName !=SceneName.StoryScene&& sceneName != SceneName.GameScene)
        NowSelectPassive.Instance.ClearPassives();


        System.GC.Collect();
    }

    public IEnumerator FadeRoutine()
    {
        nowChangeScene = true;
        MaskOnOff(true);
        iTween.FadeTo(fadeMask.gameObject, 1f, 1f);
        yield return new WaitForSeconds(1.0f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)nowSceneName);       
        yield return new WaitForSeconds(1.0f);
        iTween.FadeTo(fadeMask.gameObject, 0f, 1f);
        yield return new WaitForSeconds(1.0f);
        MaskOnOff(false);
        nowChangeScene = false;
    }

    public void MaskOnOff(bool OnOff)
    {
        if (fadeMask != null)
            fadeMask.gameObject.SetActive(OnOff);
    }




}

