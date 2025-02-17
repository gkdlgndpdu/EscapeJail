﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUi : MonoBehaviour
{

    private int nowOrder = -1;

    [SerializeField]
    private List<GameObject> descriptionList;
    [SerializeField]
    private GameObject itemInfoWindow;
    [SerializeField]
    private Text text; 

    public void ItemInfoOnOff()
    {
        if (itemInfoWindow == null) return;
        itemInfoWindow.SetActive(!itemInfoWindow.activeSelf);
    }

    public void OnEnable()
    {
        
        PlayerPrefs.SetInt(PlayerPrefKeys.FirstPlayKey, 0);


        nowOrder = -1;
        NextButtonClick();

        if (itemInfoWindow != null)
        {
            if (itemInfoWindow.activeSelf == true)
            {
                ItemInfoOnOff();
            }
        }
    }

    public void NextButtonClick()
    {     
        if(nowOrder<descriptionList.Count-1)
        nowOrder++;

        ChangeTexts(nowOrder);
    }

    public void BackButtonClick()
    {
        if (nowOrder > 0)
            nowOrder--;

        ChangeTexts(nowOrder);
    }


    private void ChangeTexts(int level)
    {
        text.text = string.Format("{0}/{1}", level+1, descriptionList.Count);

        for (int i = 0; i < descriptionList.Count; i++)
        {
            if (i == level)
            {
                descriptionList[i].gameObject.SetActive(true);
            }
            else
            {
                descriptionList[i].gameObject.SetActive(false);
            }

        }
    }

}
