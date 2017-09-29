using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public enum ItemType
{
    Weapon,
    Armor,
    Consumables,
    Bullet,
    ItemTypeEnd
}

public enum ItemName
{
    Armor1
}


public class ItemBase 
{
    public ItemType itemType;
    public WeaponType weapontype;
    public ItemName itemName;
}
