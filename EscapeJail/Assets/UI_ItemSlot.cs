using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_ItemSlot : MonoBehaviour
{
    private ItemBase itemBase;
    private Text itemText;
    [SerializeField]
    private Image image;
    private bool isEmpty = true;
    private InventoryUi inventoryUi;

    private void Awake()
    {
        itemText = GetComponentInChildren<Text>();
     
    }
    public void LinkInventoryUi(InventoryUi inventoryUi)
    {
        this.inventoryUi = inventoryUi;
    }

    public void SetSlot(ItemBase itemBase)
    {
        if (itemBase == null)
        {
            this.itemBase = null;

            isEmpty = true;

            UpdateSlotInfo();
        }
        else
        {
            this.itemBase = itemBase;

            isEmpty = false;

            UpdateSlotInfo();
        }
    
    }

    public void ClearSlot()
    {
        if (itemBase == null) return;
        itemBase = null;
        isEmpty = true;
    }
    public void OnClick()
    {
        if (inventoryUi == null|| itemBase==null) return;

        switch (inventoryUi.inventoryState)
        {
            case InventoryState.Select:
                {                  
                  itemBase.ItemAction();                    
                } break;
            case InventoryState.Delete:
                {
                   itemBase.RemoveItem();
                } break;
        }
       
    
    }
    public void UpdateSlotInfo()
    {
        if (itemText == null || image == null|| itemBase==null) return;

        switch (itemBase.itemType)
        {
            case ItemType.Weapon:
                {
                    SetSlotText(itemBase.weapontype.ToString());          
                         
                    string path = string.Format("Sprites/Icons/{0}", itemBase.weapontype.ToString());               
                    Sprite sprite = Resources.Load<Sprite>(path);

                    SetSlotSprite(sprite);

                } break;
            default:
                {
                    SetSlotText(itemBase.itemName);
                    string path = string.Format("Sprites/Icons/{0}", itemBase.itemName);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    SetSlotSprite(sprite);

                } break;
        }
       

    }

    public void ResetSlot()
    {
        SetSlotSprite(null);
        SetSlotText("Empty");
    
    }

    public void SetSlotSprite(Sprite sprite)
    {
        if (image != null)
            image.sprite = sprite;
    }
    public void SetSlotText(string text)
    {
        if(itemText!=null)
        itemText.text = text;
    }

}

