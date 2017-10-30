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
    ItemTypeEnd,
    Weapon
}


public class ItemBase
{
    public ItemType itemType;
    public WeaponType weapontype;
    private int itemLevel = 0;
    public int ItemLevel
    {
        get
        {
            return itemLevel;
        }
        set
        {
            int data = Mathf.Clamp(value, 0, 3);
            itemLevel = data;
        }
    }
    public int Value = 0;
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

    //땅에 아이템 버렸을때
    public virtual void RemoveItem()
    {
        if (player == null) return;

        if (itemType != ItemType.Weapon)
            player.RemoveItem(this);
        else
            player.RemoveWeapon(this);
    }

    //레벨이 있는 아이템은 생성자에 달아줘야함
    protected void SetItemLevel()
    {
        ItemLevel = DatabaseLoader.Instance.RandomItemLevelGenerator(itemType.ToString());
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
        ItemSpawner.Instance.SpawnBullet(player.transform.position, null);
    }
}

public class Item_Medicine : ItemBase
{
    public Item_Medicine(int level)
    {
        itemType = ItemType.Medicine;
        ItemLevel = level;

        LoadDBData();

    }

    private void LoadDBData()
    {
        ItemDB itemDB = DatabaseLoader.Instance.GetItemDBData(itemType.ToString() + ItemLevel.ToString());
        if (itemDB != null)
            this.Value = itemDB.Value;
    }

    public override void ItemAction()
    {
        if (player != null)
        {
            player.GetHp(Value);
            player.RemoveItem(this);
        }

    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        ItemSpawner.Instance.SpawnBullet(player.transform.position, null);
    }
}

public class Item_Armor : ItemBase
{
    public Item_Armor()
    {
        itemType = ItemType.Armor;
        SetItemLevel();

    }  
}

public class Item_Bag : ItemBase
{
    public Item_Bag()
    {
        //랜덤
        itemType = ItemType.Bag;
        SetItemLevel();
    }

    public override void ItemAction()
    {
        //  Debug.Log("아머 클릭");

    }

}
