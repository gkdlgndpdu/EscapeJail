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
        storyText = "The near future, A new country called Molestan appears in peaceful time.Molstan begins developing weapons secretly in order to wage war.In the World Union, which knows the situation, sends The Alpha agent team;for prevent this situation.Can they prevent war?";
        stringBuilder = new StringBuilder();
        texts = storyText.ToCharArray();
        
        StartCoroutine(ShowText());

        SoundManager.Instance.ChangeBgm("Story");

    }

    

    private IEnumerator ShowText()
    {
        if (stringBuilder == null || texts==null) yield break;

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
                else if(texts[i]== ';' )
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
        SceneManager.Instance.ChangeScene(SceneName.GameScene);
    }

    public void ShowAllText()
    {
        if (stringBuilder == null) return;
        if (isStoryAllShow == true)
        {
            ChangeScene();
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
