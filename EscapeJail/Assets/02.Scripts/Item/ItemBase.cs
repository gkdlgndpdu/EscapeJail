using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

//ItemProbabilityDB와 연동
public enum ItemType
{
    Armor,
    Bullet,
    Bag,
    Stimulant,
    Medicine,  
    EnergyDrink,
    Weapon,  //Weapon은 상자에서 나오기 떄문에 위치가 여기여야 현재 테이블에서 안나옴
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
        //Debug.Log("순수 itemAction 호출");
    }

    public virtual void RemoveItem()
    {
        if (player == null) return;

        if (itemType != ItemType.Weapon)
            player.RemoveItem(this);
        else
            player.RemoveWeapon(this);
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

       // Debug.Log("총알 클릭");

    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        ItemSpawner.Instance.SpawnBullet(player.transform.position,null);    
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
    //    Debug.Log("아머 클릭");

    }
}

public class Item_Bag : ItemBase
{
    public Item_Bag()
    {
        //랜덤
        itemType = ItemType.Bag;
        ItemLevel = 1;
    }

    public override void ItemAction()
    {
      //  Debug.Log("아머 클릭");

    }

}
