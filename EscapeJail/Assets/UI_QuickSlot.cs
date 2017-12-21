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
    private bool isEmpty = true;
    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }
    }

    private ItemBase prefItem;
    public void SetQuickSlot(UI_ItemSlot slot)
    {
        targetSlot = slot;
        prefItem = targetSlot.ItemBase;
        isEmpty = false;
        SetIcon();
    }
    private void Start()
    {
        if(iconImage!=null)
        iconImage.color = new Color(0f, 0f, 0f, 0f);
    }

    public void UpdateQuickSlot()
    {
        if (targetSlot == null) return;

        //리셋
        iconImage.sprite = null;
        iconImage.color = new Color(0f, 0f, 0f, 0f);
        prefItem = null;
        targetSlot = null;
        isEmpty = true;

    }   

    public void SetIcon()
    {
        if (iconImage == null) return;

        ItemBase nowItem = targetSlot.ItemBase;

        iconImage.color = new Color(1f, 1f, 1f, 1f);

        switch (nowItem.itemType)
        {
            case ItemType.Weapon:
                {
                    string path = string.Format("Sprites/icon/{0}", nowItem.weapontype.ToString());
                    Sprite sprite = Resources.Load<Sprite>(path);
                    iconImage.sprite = sprite;
                    iconImage.color = Color.white;
                }
                break;
            default:
                {
                    string path = string.Format("Sprites/icon/{0}", nowItem.itemName);
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

        switch (targetSlot.ItemBase.itemType)
        {
      
            case ItemType.Bullet:
                {
                    if (GamePlayerManager.Instance.player.CanFillBullet() == false)
                        return;
                }
                break;        
            case ItemType.Stimulant:
                {
                    if (GamePlayerManager.Instance.player.CanUseStimulant() == false)
                        return;

                }
                break;
            case ItemType.Medicine:
                {
                    if (GamePlayerManager.Instance.player.CanHeal() == false)
                        return;
                }
                break;
           
        }
        targetSlot.UseItem();

        UpdateQuickSlot();

    }


}
