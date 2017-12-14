using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BossIntroduceWindow : MonoBehaviour
{
    public static BossIntroduceWindow Instance;
    [SerializeField]
    private Image img;

    private Dictionary<string, Sprite> imageDic;
    private Action endImageFunc;

    private void Awake()
    {
        Instance = this;

        LoadImages();

        if (img != null)
            img.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance != null)
            Instance = null;
    }

    private void LoadImages()
    {
        imageDic = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/BossTexture/");
        if (sprites == null || imageDic == null) return;

        for (int i = 0; i < sprites.Length; i++)
        {
            imageDic.Add(sprites[i].name, sprites[i]);
        }


    }

    public void ShowImage(Action endFunc)
    {
        if (img == null || imageDic == null) return;

        endImageFunc = endFunc;
        SoundManager.Instance.PlayRandomBossBgm();

        int stage = StagerController.Instance.NowStageLevel;
        string fileName = string.Format("Stage{0}", stage.ToString());
        if (imageDic.ContainsKey(fileName) == false) return;

        img.gameObject.SetActive(true);
        img.sprite = imageDic[fileName];
        img.color = Color.white;

        iTween.FadeTo(img.gameObject, iTween.Hash("alpha", 0f, "time", 1f, "oncompletetarget", gameObject, "oncomplete", "OnComplete","delay",2f));

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
