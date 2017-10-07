using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public enum ItemType
{
    Weapon,
    Armor,
    Bullet,
    Bag,
    Consumables,
    ItemTypeEnd
}




public class ItemBase
{
    public ItemType itemType;
    public WeaponType weapontype;
    public int ItemLevel = 0;
    protected CharacterBase player;
    public string itemName
    {
        get
        {
            if (ItemLevel != 0)
                return string.Format("{0}{1}", itemType.ToString(), ItemLevel);
            else
                return itemType.ToString();
        }
    }
    public ItemBase()
    {
        player = GamePlayerManager.Instance.player;
    }
    //인벤토리에서 클릭했을때의 효과
    public virtual void ItemAction()
    {
        Debug.Log("순수 itemAction 호출");
    }

    public virtual void RemoveItem()
    {
        if (player == null) return;


        if (itemType == ItemType.Weapon)
            player.RemoveWeapon(this);
        else
            player.RemoveItem(this);
    }
}
public class Item_Weapon : ItemBase
{
    public Item_Weapon(WeaponType weapontype)
    {
        itemType = ItemType.Weapon;
        this.weapontype = weapontype;
    }

    public override void ItemAction()
    {
        Debug.Log("무기클릭");
    }


}
public class Item_Bullet : ItemBase
{
    public Item_Bullet()
    {
        itemType = ItemType.Bullet;
        ItemLevel = 0;
    }
    public override void ItemAction()
    {
        if (player != null)
        {
            player.GetBulletItem();
            player.RemoveItem(this);
        }

        Debug.Log("총알 클릭");

    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        ItemSpawner.Instance.SpawnBullet(player.transform.position);    
    }
}

public class Item_Armor : ItemBase
{

    public Item_Armor(int level)
    {
        itemType = ItemType.Armor;
        ItemLevel = level;

    }
    public override void ItemAction()
    {
        Debug.Log("아머 클릭");

    }
}

public class Item_Bag : ItemBase
{
    public Item_Bag(int level)
    {
        itemType = ItemType.Bag;
        ItemLevel = level;
    }

    public override void ItemAction()
    {
        Debug.Log("아머 클릭");

    }

}
