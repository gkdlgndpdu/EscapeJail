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

    public void LoadingEnd()
    {
        iTween.FadeTo(this.gameObject, 0f, 1f);
        StartCoroutine(OffImages());
    }
    IEnumerator OffImages()
    {
        yield return new WaitForSeconds(1.0f);
        ImageAndTextOnOff(false);
    }

    public void LoadingStart()
    {
        ImageAndTextOnOff(true);
        SetAlphaToImage(1f);

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
