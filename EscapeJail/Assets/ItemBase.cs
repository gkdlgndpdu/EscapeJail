using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public enum ItemType
{
    Weapon,
    Consumables,
    Bullet,
    Armor
}

public enum ItemName
{
    Armor1
}


public class ItemBase 
{
    public ItemType itemType;
    public WeaponType weaponName;
    public ItemName ItemName;
}
