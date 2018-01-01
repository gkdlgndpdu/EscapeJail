using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_QuickSlot : MonoBehaviour
{

    private ItemBase quickItem;
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
  
    public void SetQuickSlot(ItemBase item)
    {
        quickItem = item; 
        isEmpty = false;
        SetIcon();
    }
    private void Start()
    {
        if(iconImage!=null)
        iconImage.color = new Color(0f, 0f, 0f, 0f);
    }

    public void UpdateQuickSlot(ItemBase UsingItem)
    {
        if (quickItem == null) return;
        if (UsingItem == null) return;
        if (quickItem != UsingItem) return;   

        //리셋
        iconImage.sprite = null;
        iconImage.color = new Color(0f, 0f, 0f, 0f);
        quickItem = null;
        isEmpty = true;

    }   

    public void SetIcon()
    {
        if (iconImage == null) return;
        if (quickItem == null) return;    

        iconImage.color = new Color(1f, 1f, 1f, 1f);

        switch (quickItem.itemType)
        {
            case ItemType.Weapon:
                {
                    string path = string.Format("Sprites/icon/{0}", quickItem.weapontype.ToString());
                    Sprite sprite = Resources.Load<Sprite>(path);
                    iconImage.sprite = sprite;
                    iconImage.color = Color.white;
                }
                break;
            default:
                {
                    string path = string.Format("Sprites/icon/{0}", quickItem.itemName);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    iconImage.sprite = sprite;
                    iconImage.color = Color.white;
                }
                break;
        }


    }


    public void ClickButton()
    {
        if (quickItem == null) return;

        switch (quickItem.itemType)
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
        quickItem.ItemAction();

        UpdateQuickSlot(quickItem);

    }


}
