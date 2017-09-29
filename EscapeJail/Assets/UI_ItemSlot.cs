using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    private ItemBase itemBase;
    private Text itemText;
    private Image image;
    private bool isEmpty = true;

    private void Awake()
    {
        itemText = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
    }

    public void SetSlot(ItemBase itemBase)
    {
        if (itemBase == null) return;
        this.itemBase = itemBase;

        isEmpty = false;

        UpdateSlotInfo();
    }

    public void ClearSlot()
    {
        if (itemBase == null) return;
        itemBase = null;
        isEmpty = true;
    }

    public void UpdateSlotInfo()
    {
        if (itemText == null || image == null|| itemBase==null) return;

        switch (itemBase.itemType)
        {
            case ItemType.Weapon:
                {
                    itemText.text = itemBase.weapontype.ToString();
                } break;
            default:
                {
                    itemText.text = itemBase.itemName.ToString();
                } break;
        }
       

    }
	
}
