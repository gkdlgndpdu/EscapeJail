using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUi : MonoBehaviour
{
    private List<UI_ItemSlot> itemSlots= new List<UI_ItemSlot>();
    private List<ItemBase> allItemList;

    [SerializeField]
    private GridLayoutGroup grid;

    [SerializeField]
    private GameObject slotPrefab;

    private UI_ItemSlot nowSelectedSlot;


    [SerializeField]
    private Image backGroundImage;

    [SerializeField]
    private Image ArmorImage;
    [SerializeField]
    private Image ArmorRedImage;

    [SerializeField]
    private UI_QuickSlot quickSlot;

    public void SetSelectSlot(UI_ItemSlot slot)
    {
        if (slot == null) return;

        //기존 선택 항목 해제
        if (nowSelectedSlot != null)
        {
            nowSelectedSlot.SelectOnOff(false);
        }

        //새로 선택 항목으로 갱신
        slot.SelectOnOff(true);
        nowSelectedSlot = slot;
    }

    public void ResetSelectedSlot()
    {
        if (nowSelectedSlot != null)
        {
            nowSelectedSlot.SelectOnOff(false);
        }
        nowSelectedSlot = null;
    }

    public void UseSelectedItem()
    {
        if (nowSelectedSlot == null) return;
        nowSelectedSlot.UseItem();
        UpdateQuickSlot();
    }
    public void DiscardSelectedItem()
    {
        if (nowSelectedSlot == null) return;
        nowSelectedSlot.DiscardItem();
        UpdateQuickSlot();
    }

    public void RegistQuickSlotSelectedItem()
    {
        if (quickSlot == null) return;
        if (nowSelectedSlot == null) return;        

        quickSlot.SetQuickSlot(nowSelectedSlot);

    }


    public void SetArmorUi(float ratio)
    {
        if (ArmorRedImage != null)
        {
            ArmorRedImage.fillAmount =1f- ratio;            
        }
        
    }

    public void SetArmorImage(int level)
    {
        level = Mathf.Clamp(level, 1, 3);
        string path = string.Format("Sprites/Icons/Armor{0}", level.ToString());

        if (ArmorImage != null && ArmorRedImage != null)
        {
            Sprite loadImage = Resources.Load<Sprite>(path);
            if (loadImage != null)
            {
                ArmorImage.sprite = loadImage;
                ArmorRedImage.sprite = loadImage;
            }
        }

    }

    public void UpdateInventoryArmorUi(float ratio)
    {
        if (ArmorRedImage != null)
            ArmorRedImage.fillAmount = ratio;
    }


    public void UpdateQuickSlot()
    {
        if (quickSlot == null) return;
        quickSlot.UpdateQuickSlot();
    }


    private void ChangeBackGroundColor(Color color)
    {
        if (backGroundImage == null) return;
        backGroundImage.color = color;
    }

    public void LinkAllItemList(List<ItemBase> allItemList)
    {
        this.allItemList = allItemList;
    }
    public void SetSlotNum(int num)
    {
        if (itemSlots == null) return;
        for (int i = itemSlots.Count; i < num; i++)
        {
            MakeSlot();
        }

        UpdateInventoryUi();
    }

    public void MakeSlot()
    {
        if (slotPrefab == null) return;
        GameObject loadObj = Instantiate(slotPrefab, grid.transform);
        if (loadObj != null)
        {
            UI_ItemSlot slot = loadObj.GetComponent<UI_ItemSlot>();
            if (slot != null)
            {
                slot.LinkInventoryUi(this);
                itemSlots.Add(slot);
            }
        }
    }

    public void UpdateInventoryUi()
    {
        if (itemSlots == null) return;
        if (allItemList == null) return;


        for(int i = 0; i < itemSlots.Count; i++)
        {
            if (i < allItemList.Count)
                itemSlots[i].SetSlot(allItemList[i]);
            else
                itemSlots[i].ResetSlot();

        }
   
    }
    public void InventoryOnOff()
    {     
        this.gameObject.SetActive(!this.gameObject.activeSelf);

        if(!this.gameObject.activeSelf == true)
            UpdateInventoryUi(); 
    }


    // Use this for initialization
    void Start()
    {
        SetSlotNum(1);
        UpdateInventoryUi();

    }
    private void OnEnable()
    {
  
   

        GameManager.Instance.StopTime();

        System.GC.Collect();
    }

    private void OnDisable()
    {      
        GameManager.Instance.ResumeTime();
    }

}
