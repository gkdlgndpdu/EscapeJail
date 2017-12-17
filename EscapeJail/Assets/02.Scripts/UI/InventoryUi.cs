using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using weapon;

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
    private UI_QuickSlot quickSlot;

    //스크롤
    [SerializeField]
    private Transform slotsParent;
    private RectTransform rectTr;

    private void Awake()
    {
        rectTr = slotsParent.GetComponent<RectTransform>();
    }

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

        UpdateInventoryUi();
    }
    public void DiscardSelectedItem()
    {
        if (nowSelectedSlot == null) return;
        nowSelectedSlot.DiscardItem();
        UpdateQuickSlot();

        UpdateInventoryUi();
    }

    public void RegistQuickSlotSelectedItem()
    {
        if (quickSlot == null) return;
        if (nowSelectedSlot == null) return;        

        quickSlot.SetQuickSlot(nowSelectedSlot);

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

        if (rectTr != null)
        {
            float eachDistance = grid.cellSize.y + grid.spacing.y;
            rectTr.transform.localPosition = new Vector3(0f, -(float)itemSlots.Count / 5f * 70f,0f);
            rectTr.sizeDelta = new Vector2(500f, eachDistance * ((float)((float)itemSlots.Count/5f)));
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
        if (this.gameObject.activeSelf == false) return;
        if (itemSlots == null) return;
        if (allItemList == null) return;

        WeaponType nowEquipWeapon = GamePlayerManager.Instance.player.GetNowEquipWeapon();

        for(int i = 0; i < itemSlots.Count; i++)
        {
            if (i < allItemList.Count)
            {
                if (allItemList[i].weapontype == nowEquipWeapon && nowEquipWeapon != WeaponType.PlayerWeaponStart)
                    itemSlots[i].SetSlotEquip(true);                
                else
                    itemSlots[i].SetSlotEquip(false);

                itemSlots[i].SetSlot(allItemList[i]);

            }
            else
            {
                itemSlots[i].ResetSlot();
                
            }

        }
   
    }
    public void InventoryOnOff()
    {     
        this.gameObject.SetActive(!this.gameObject.activeSelf);

        //if(!this.gameObject.activeSelf == true)
        //    UpdateInventoryUi(); 
    }


    // Use this for initialization
    void Start()
    {
        SetSlotNum(1);  

    }
    private void OnEnable()
    {

        UpdateInventoryUi();
        TimeManager.Instance.StopTime();
        System.GC.Collect();
    }

    private void OnDisable()
    {
        TimeManager.Instance.ResumeTime();
    }

}
