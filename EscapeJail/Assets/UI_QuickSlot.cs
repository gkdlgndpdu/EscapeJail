using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_QuickSlot : MonoBehaviour
{

    private UI_ItemSlot targetSlot;
    [SerializeField]
    private Image iconImage;

    private ItemBase prefItem;
    public void SetQuickSlot(UI_ItemSlot slot)
    {             
        targetSlot = slot;
        prefItem = targetSlot.ItemBase;

        SetIcon();
    }

    public void UpdateQuickSlot()
    {
        if (targetSlot == null) return;

        if (targetSlot.ItemBase != prefItem)
        {
            //리셋
            iconImage.sprite = null;
            iconImage.color = new Color(0f, 0f, 0f, 0f);
            prefItem = null;
        }
    }

    public void SetIcon()
    {
        if (iconImage == null) return;

        ItemBase nowItem = targetSlot.ItemBase;

        switch (nowItem.itemType)
        {
            case ItemType.Weapon:
                {
                    string path = string.Format("Sprites/Icons/{0}", nowItem.weapontype.ToString());
                    Sprite sprite = Resources.Load<Sprite>(path);
                    iconImage.sprite = sprite;
                    iconImage.color = Color.white;
                }
                break;
            default:
                {             
                    string path = string.Format("Sprites/Icons/{0}", nowItem.itemName);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    iconImage.sprite = sprite;
                    iconImage.color = Color.white;
                }
                break;
        }


    }
  

    public void ClickButton()
    {
        if (targetSlot == null) return;

        targetSlot.UseItem();

        UpdateQuickSlot();
        
    }


}
