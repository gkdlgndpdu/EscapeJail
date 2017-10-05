using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryState
{
    Select,
    Delete
}
public class InventoryUi : MonoBehaviour
{
    private List<UI_ItemSlot> itemSlots= new List<UI_ItemSlot>();
    private List<ItemBase> allItemList;

    [SerializeField]
    private GridLayoutGroup grid;

    [SerializeField]
    private GameObject slotPrefab;

    public InventoryState inventoryState = InventoryState.Select;

    [SerializeField]
    private Image backGroundImage;

    public void InventoryStateButtonClick()
    {
        if(inventoryState == InventoryState.Select)
        {
            inventoryState = InventoryState.Delete;
            ChangeBackGroundColor(Color.red);
        }
        else
        {
            inventoryState = InventoryState.Select;
            ChangeBackGroundColor(Color.white);
        }
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
    }


    // Use this for initialization
    void Start()
    {
        SetSlotNum(1);
        UpdateInventoryUi();

    }
    private void OnEnable()
    {
        UpdateInventoryUi();

    }
   
}
