using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Inventory 
{
    private List<ItemBase> allItemList = new List<ItemBase>();
    private List<Weapon> weaponList = new List<Weapon>();
    private int weaponIndex = -1;

    private int inventoryLevel;
    private int inventorySlotNum =15;

    private InventoryUi inventoryUi;
    public Inventory(InventoryUi inventoryUi) 
    {
        this.inventoryUi = inventoryUi;
        this.inventoryUi.LinkAllItemList(allItemList);
    }
    //무기 교체용
    public Weapon GetWeapon()
    {
        if (weaponList == null) return null;
        if (weaponList.Count == 0) return null;

        weaponIndex++;

        if (weaponIndex >= weaponList.Count)
            weaponIndex = 0;

        return weaponList[weaponIndex];
    }
    // Use this for initialization

    //무기 습득용
    public void AddWeapon(Weapon weapon)
    {
        if (weaponList != null && weapon != null)
            weaponList.Add(weapon);


        AddToInventory(weapon);     
    }

    private void AddToInventory(ItemBase itemBase)
    {
        if (itemBase == null) return;
        if (allItemList == null) return;
        if (allItemList.Count > inventorySlotNum) return;

        allItemList.Add(itemBase);
    }

    ~Inventory()
    {

    }
}
