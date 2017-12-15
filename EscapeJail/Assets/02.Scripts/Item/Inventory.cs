using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Inventory
{
    //핵심
    private List<ItemBase> allItemList = new List<ItemBase>();
    //무기 교체용
    private List<Weapon> weaponList = new List<Weapon>();
    private List<Weapon> prefSpawnWeapons = new List<Weapon>();
    private int weaponIndex = -1;

    private int bagSize = 0;
    //Ui
    private InventoryUi inventoryUi;


    public bool isInventoryFull()
    {
        if (GamePlayerManager.Instance.PlayerName == CharacterType.Trader) return false;
        if (allItemList == null) return true;
        if (bagSize == 0) return true;

        return allItemList.Count >= bagSize;
    }

    /// <summary>
    /// value가 0이라는건 처음 디폴트상태를 의미
    /// </summary>
    /// <param name="value">늘려줄 가방의 양</param>
    public void SetInventorySize(int value)
    {
        if (bagSize >= value && value != 0) return;

        if (value == 0)
        {
            if (MyUtils.GetNowPassive() == PassiveType.ExtendedPocket)
            {
                bagSize = 5 + 5;
            }
            else
            {
                //
                bagSize = 5;
         
            }
        }
        else
        {
            if (MyUtils.GetNowPassive() == PassiveType.ExtendedPocket)
            {
                bagSize = (value+5) + 5;
            }
            else
            {
                bagSize = (value + 5);
            }

        }

        if (inventoryUi != null)
            inventoryUi.SetSlotNum(bagSize);
    }


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

        if (weaponIndex >= weaponList.Count || weaponIndex <= 0)
            weaponIndex = 0;

        return weaponList[weaponIndex];
    }
    public Weapon GetSpeceficWeapon(WeaponType weaponType)
    {
        if (weaponList == null) return null;
        if (weaponList.Count == 0) return null;
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (weaponList[i].weapontype == weaponType)
                return weaponList[i];
        }
        return null;
    }
    public Weapon GetLastWeapon()
    {
        if (weaponList == null) return null;
        if (weaponList.Count == 0) return null;
        return weaponList[weaponList.Count - 1];
    }
    // Use this for initialization

    //무기 습득용
    public void AddWeapon(Weapon weapon)
    {
        weapon = CheckPrefWeapon(weapon);

        if (weaponList != null && weapon != null)
            weaponList.Add(weapon);


        AddToInventory(weapon);

    }

    private Weapon CheckPrefWeapon(Weapon weapon)
    {
        if (prefSpawnWeapons == null) return weapon;

        for (int i = 0; i < prefSpawnWeapons.Count; i++)
        {
            if (prefSpawnWeapons[i].weapontype == weapon.weapontype)
            {
                return prefSpawnWeapons[i];
            }
        }

        return weapon;
    }

    public void RemoveWeapon(ItemBase weapon)
    {
        if (weaponList != null && weapon != null)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                if (weaponList[i].weapontype == weapon.weapontype)
                {
                    Weapon RemoveWeapon = weaponList[i];

                    weaponList.Remove(RemoveWeapon);
                    RemoveInInventory(RemoveWeapon);

                    prefSpawnWeapons.Add(RemoveWeapon);

                    weaponIndex -= 2;
                    return;
                }
            }

        }


    }

    public void AddToInventory(ItemBase itemBase)
    {
        if (itemBase == null) return;
        if (allItemList == null) return;


        if (allItemList.Count >= bagSize)
        {
            if (GamePlayerManager.Instance.PlayerName == CharacterType.Trader)
            {
                bagSize += 1;
                if (inventoryUi != null)
                    inventoryUi.SetSlotNum(bagSize);
            }
            else
            {
                return;
            }              

         
        }


        allItemList.Add(itemBase);

        //if (inventoryUi != null)
        //    inventoryUi.UpdateInventoryUi();
    }


    public void RemoveInInventory(ItemBase itemBase)
    {
        if (itemBase == null) return;
        if (itemBase.itemType == ItemType.Weapon)
        {
            //무기 벗겨줌
            RemoveWeapon(itemBase);

            CharacterBase player = GamePlayerManager.Instance.player;
            //해당 무기 생성
            ItemSpawner.Instance.SpawnWeapon(player.transform.position, itemBase.weapontype);
            player.ChangeWeapon();
        }

        allItemList.Remove(itemBase);

        if (inventoryUi != null)
            inventoryUi.UpdateInventoryUi();

        itemBase = null;
    }
    ~Inventory()
    {

    }
}
