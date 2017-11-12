using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_QuickSlot : MonoBehaviour
{

    private ItemBase nowItem;
    [SerializeField]
    private Image iconImage;

    public void SetQuickSlot(ItemBase item)
    {
        if (item == null)
        {
            iconImage.sprite = null;
            return;
        }

        nowItem = item;
        SetIcon();
    }

    public void UpdateQuickSlot()
    {
        //아이템이 실제로 사용 됐다
        if (nowItem == null)
        {
            //리셋
            iconImage.sprite = null;
        }
    }

    public void SetIcon()
    {
        if (iconImage == null) return;

        

        switch (nowItem.itemType)
        {
            case ItemType.Weapon:
                {
                    string path = string.Format("Sprites/Icons/{0}", nowItem.weapontype.ToString());
                    Sprite sprite = Resources.Load<Sprite>(path);
                    iconImage.sprite = sprite;
                }
                break;
            default:
                {             
                    string path = string.Format("Sprites/Icons/{0}", nowItem.itemName);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    iconImage.sprite = sprite;
                }
                break;
        }


    }
  

    public void ClickButton()
    {
        if (nowItem == null) return;

        nowItem.ItemAction();

        UpdateQuickSlot();
        
    }


}
