using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUi : MonoBehaviour
{
    private List<UI_ItemSlot> itemSlots;
    private List<ItemBase> allItemList;

    private GridLayoutGroup grid;

    [SerializeField]
    private GameObject slotPrefab;
    private void Awake()
    {
        itemSlots = new List<UI_ItemSlot>();
        grid = GetComponentInChildren<GridLayoutGroup>();
    }

    public void LinkAllItemList(List<ItemBase> allItemList)
    {
        this.allItemList = allItemList;
    }
    public void SetSlotNum(int num)
    {
        for (int i = 0; i < num; i++)
        {
            MakeSlot();
        }
    }

    public void MakeSlot()
    {
        if (slotPrefab == null) return;
        GameObject loadObj = Instantiate(slotPrefab, grid.transform);
        if (loadObj != null)
        {
            UI_ItemSlot slot = loadObj.GetComponent<UI_ItemSlot>();
            if (slot != null)
                itemSlots.Add(slot);
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
  

    // Use this for initialization
    void Start()
    {
        SetSlotNum(1);

    }
    private void OnEnable()
    {
        UpdateInventoryUi();

    }
   
}
