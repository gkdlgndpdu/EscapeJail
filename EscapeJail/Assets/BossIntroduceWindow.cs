using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BossIntroduceWindow : MonoBehaviour
{
    [SerializeField]
    private Image img;

    private Dictionary<string, Sprite> imageDic;
    private Action endImageFunc;

    private void Awake()
    {
        LoadImages();
    }

    private void LoadImages()
    {
        imageDic = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/BossTexture/");
        if (sprites == null|| imageDic==null) return;

        for(int i = 0; i < sprites.Length; i++)
        {
            imageDic.Add(sprites[i].name, sprites[i]);
        }


    }

    public void ShowImage(Action endFunc)
    {
        if (img == null|| imageDic==null) return;
        int stage = StagerController.Instance.NowStageLevel;
        string fileName = string.Format("Stage{0}", stage.ToString());
        if (imageDic.ContainsKey(fileName) == false) return;
        img.gameObject.SetActive(true);
        img.sprite = imageDic[fileName];  
        iTween.FadeTo(img.gameObject, iTween.Hash("alpha", 0f, "time", 1f, "oncompletetarget", gameObject,"oncomplete", "OnComplete"));
        
    }
    public void OnComplete()
    {
        img.gameObject.SetActive(false);

        if (endImageFunc != null)
            endImageFunc();
    }

    public void HideImage()
    {

    }



}
