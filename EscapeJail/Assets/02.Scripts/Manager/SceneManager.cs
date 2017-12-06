using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum SceneName
{
    LobbyScene,
    SelectScene,
    StoryScene,
    GameScene
}
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    [SerializeField]
    private Image fadeMask;
    private SceneName nowSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Application.targetFrameRate = 50;
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
        nowSceneName = sceneName;
        StartCoroutine(FadeRoutine());

    }

    public IEnumerator FadeRoutine()
    {
        MaskOnOff(true);
        iTween.FadeTo(fadeMask.gameObject, 1f, 1f);
        yield return new WaitForSeconds(1.0f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)nowSceneName);       
        yield return new WaitForSeconds(1.0f);
        iTween.FadeTo(fadeMask.gameObject, 0f, 1f);
        yield return new WaitForSeconds(1.0f);
        MaskOnOff(false);
    }

    public void MaskOnOff(bool OnOff)
    {
        if (fadeMask != null)
            fadeMask.gameObject.SetActive(OnOff);
    }

}

