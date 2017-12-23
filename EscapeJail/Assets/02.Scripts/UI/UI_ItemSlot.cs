using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using weapon;
[RequireComponent(typeof(Image))]
public class UI_ItemSlot : MonoBehaviour
{
    private ItemBase itemBase;
    public ItemBase ItemBase
    {
        get
        {
            return itemBase;
        }
    }

    [SerializeField]
    private Image image;
    private bool isEmpty = true;
    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }
    }
    private InventoryUi inventoryUi;

    [SerializeField]
    private Image selectedFrame;

    [SerializeField]
    private Text equippedText;

    [SerializeField]
    private Text itemText;

    private bool isSelected = false;

    private bool isQuickSlot = false;

    private void Awake()
    {  

        if (selectedFrame != null)
            selectedFrame.gameObject.SetActive(false);
    }

    public void SetSlotEquip(bool OnOff)
    {
        if (equippedText == null) return;
        equippedText.gameObject.SetActive(OnOff);

    }

    public void SelectOnOff(bool OnOff)
    {
        //선택
        if (OnOff == true)
        {
            if (selectedFrame != null)
                selectedFrame.gameObject.SetActive(true);
        }
        //해제
        else if (OnOff == false)
        {
            if (selectedFrame != null)
                selectedFrame.gameObject.SetActive(false);
        }
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

    public void ResetSlot()
    {
        SetSlotSprite(null);
        SetSlotText("Empty");
        itemBase = null;
        SetSlotEquip(false);


    }

    public void ClearSlot()
    {
        if (itemBase == null) return;
        itemBase = null;
        isEmpty = true;
    }
   
    public void UseItem()
    {
        if(itemBase!=null)
        itemBase.ItemAction();

   

        //if(inventoryUi!=null)
        //inventoryUi.ResetSelectedSlot();
    }

    public void DiscardItem()
    {
        if (itemBase != null)
            itemBase.RemoveItem();
    }

    public void UpdateSlotInfo()
    {
        if (itemText == null || image == null|| itemBase==null) return;

        switch (itemBase.itemType)
        {
            case ItemType.Weapon:
                {
                    if(itemBase is Weapon == true)
                    {
                        Weapon nowWeapon = itemBase as Weapon;
                        if (nowWeapon != null)
                            SetSlotText(string.Format("{0}/{1}",nowWeapon.nowAmmo,nowWeapon.maxAmmo));
                    }                    
                         
                    string path = string.Format("Sprites/icon/{0}", itemBase.weapontype.ToString());               
                    Sprite sprite = Resources.Load<Sprite>(path);

                    SetSlotSprite(sprite);

                } break;
            default:
                {
                    SetSlotText(itemBase.showItemName);
                    string path = string.Format("Sprites/icon/{0}", itemBase.itemName);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    SetSlotSprite(sprite);

                } break;
        }       

    }


    public void SetSlotSprite(Sprite sprite)
    {
        if (image == null) return;

            image.sprite = sprite;

        if (sprite != null)
            image.color = Color.white;
        else if (sprite == null)
            image.color = new Color(0f, 0f, 0f, 0f);
            
    }
    public void SetSlotText(string text)
    {
        if(itemText!=null)
        itemText.text = text;
    }

    public void OnClick()
    {
        if (inventoryUi == null) return;

        inventoryUi.SetSelectSlot(this);
    }

}

