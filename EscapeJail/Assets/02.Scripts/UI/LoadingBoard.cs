using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingBoard : MonoBehaviour
{
    public static LoadingBoard Instance;
    [SerializeField]
    private Image backGround;
    [SerializeField]
    private Text creatingText;
    [SerializeField]
    private Text percentText;

    float fadeTime = 1f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Instance = null;
            Instance = this;
        }
        LoadingStart();
    }
    public void LoadingStart()
    {   
        ImageAndTextOnOff(true);
        SetAlphaToImage(1f);

    }
    public void LoadingEnd()
    {    
          StartCoroutine(OffImages());

        SoundManager.Instance.ChangeBgm(string.Format("Stage{0}", StagerController.Instance.NowStageLevel.ToString()));

    }
    IEnumerator OffImages()
    {
        float count = 0f;
        while (true)
        {
            count += Time.deltaTime;
            SetAlphaToImage(1f - count);
            if (count > 1)
            {
                ImageAndTextOnOff(false);
                yield break;
            }
            yield return null;
        }

  
    }



    private void SetAlphaToImage(float alpha)
    {
        Color color = new Color(1f, 1f, 1f, alpha);

         if (backGround != null)
            backGround.color = color;

        if (creatingText != null)
            creatingText.color = color;

        if (percentText != null)
            percentText.color = color;
    }



    private void ImageAndTextOnOff(bool OnOff)
    {
        if (backGround != null)
            backGround.gameObject.SetActive(OnOff);

        if (creatingText != null)
            creatingText.gameObject.SetActive(OnOff);

        if (percentText != null)
            percentText.gameObject.SetActive(OnOff);

    }


}
