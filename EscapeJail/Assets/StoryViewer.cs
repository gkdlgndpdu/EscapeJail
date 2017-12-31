using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class StoryViewer : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image image;

    [SerializeField]
    private GameObject thankyou1;
    [SerializeField]
    private GameObject thankyou2;
    [SerializeField]
    private GameObject thankyou3;

    private bool nowThankYouShow = false;

    [SerializeField]
    private Sprite beginingTexture;

    [SerializeField]
    private Sprite endingTexture;

    private string storyText;

    private StringBuilder stringBuilder;
    private char[] texts;
    private float textSpeed = 0.05f;
    private float commaDelayTime = 1f;
    private bool isStoryAllShow = false;

    private void Start()
    {
        // storyText = "The near future, A new country called Molestan appears in peaceful time.Molstan begins developing weapons secretly in order to wage war.In the World Union, which knows the situation, sends The Alpha agent team;for prevent this situation.Can they prevent war?";
        storyText = text.text;
        stringBuilder = new StringBuilder();
        texts = storyText.ToCharArray();

        StartCoroutine(ShowText());

        if (SceneManager.Instance.NowSceneName == SceneName.StoryScene)
            SoundManager.Instance.ChangeBgm("Story");
        else if (SceneManager.Instance.NowSceneName == SceneName.EndingScene)
            SoundManager.Instance.ChangeBgm("Ending");


        if (thankyou1 != null)
            thankyou1.gameObject.SetActive(false);
        if (thankyou2 != null)
            thankyou2.gameObject.SetActive(false);
        if (thankyou3 != null)
            thankyou3.gameObject.SetActive(false);

    }



    private IEnumerator ShowText()
    {
        if (stringBuilder == null || texts == null) yield break;

        for (int i = 0; i < texts.Length; i++)
        {
            if (stringBuilder != null)
            {
                stringBuilder.Append(texts[i]);

                if (texts[i] == '.')
                {
                    stringBuilder.Append("\n");
                    if (text != null)
                        text.text = stringBuilder.ToString();
                    yield return new WaitForSeconds(commaDelayTime);
                }
                else if (texts[i] == ';')
                {
                    stringBuilder.Append("\n");
                    if (text != null)
                        text.text = stringBuilder.ToString();
                }
                else
                {
                    if (text != null)
                        text.text = stringBuilder.ToString();
                    yield return new WaitForSeconds(textSpeed);
                }
            }


        }
        isStoryAllShow = true;
    }

    public void ChangeScene()
    {
        if (SceneManager.Instance.NowSceneName == SceneName.StoryScene)
            SceneManager.Instance.ChangeScene(SceneName.GameScene);
        else if (SceneManager.Instance.NowSceneName == SceneName.EndingScene)
            SceneManager.Instance.ChangeScene(SceneName.MenuScene);
    }
    public void EndingSKipButtonClick()
    {
        if (nowThankYouShow == true) return;
        nowThankYouShow = true;
        StartCoroutine(ThankYouRoutine());
    }

    private IEnumerator ThankYouRoutine()
    {
        if (thankyou1 == null || thankyou2 == null || thankyou3 == null) yield break;
        thankyou1.gameObject.SetActive(true);
        iTween.FadeTo(thankyou1.gameObject, 1f, 3f);
        yield return new WaitForSeconds(3f);
        thankyou3.gameObject.SetActive(true);
        iTween.FadeTo(thankyou3.gameObject, 1f, 3f);
        yield return new WaitForSeconds(3f);
        thankyou2.gameObject.SetActive(true);
        iTween.FadeTo(thankyou2.gameObject, 1f, 3f);
        yield return new WaitForSeconds(3f);
    }



    public void ShowAllText()
    {
        if (stringBuilder == null) return;
        if (isStoryAllShow == true)
        {
            if (SceneManager.Instance.NowSceneName == SceneName.StoryScene)
                SceneManager.Instance.ChangeScene(SceneName.GameScene);
            else if (SceneManager.Instance.NowSceneName == SceneName.EndingScene)
            {
                EndingSKipButtonClick();
                return;
            }

        }

        StopAllCoroutines();

        stringBuilder.Length = 0;
        for (int i = 0; i < texts.Length; i++)
        {
            if (stringBuilder != null)
            {
                stringBuilder.Append(texts[i]);

                if (texts[i] == '.')
                {
                    stringBuilder.Append("\n");
                    if (text != null)
                        text.text = stringBuilder.ToString();

                }
                else if (texts[i] == ';')
                {
                    stringBuilder.Append("\n");
                    if (text != null)
                        text.text = stringBuilder.ToString();
                }
                else
                {
                    if (text != null)
                        text.text = stringBuilder.ToString();

                }
            }


        }
        isStoryAllShow = true;
    }


}
