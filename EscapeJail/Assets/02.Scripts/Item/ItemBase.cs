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
    Turret,
    FlashBang,
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
    public string showItemName
    {
        get
        {
            if (ItemLevel != 0)
                return string.Format("{0} Lv{1}", itemType.ToString(), ItemLevel);
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
        LoadDBData();
    }

    protected void LoadDBData()
    {
        ItemDB itemDB = DatabaseLoader.Instance.GetItemDBData(itemType.ToString());
        if (itemDB != null)
            this.Value = itemDB.Value * ItemLevel;
    }
}
public class Item_Weapon : ItemBase
{
    public Item_Weapon(WeaponType weapontype)
    {
        itemType = ItemType.Weapon;
        this.weapontype = weapontype;
    }



}
public class Item_Bullet : ItemBase
{
    public Item_Bullet(int level =999)
    {
        itemType = ItemType.Bullet;
        if (level == 999)
        {
            SetItemLevel();
        }
        else
        {
            ItemLevel = level;
            LoadDBData();
        }
    }
    public override void ItemAction()
    {
        if (player != null)
        {
            if (player.CanReload() == false) return;

            player.GetBulletItem(this.Value);
            player.RemoveItem(this);
        }

        // Debug.Log("총알 클릭");

    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        ItemSpawner.Instance.SpawnItem(itemType, player.transform.position, null);
    }
}

public class Item_Medicine : ItemBase
{
    public Item_Medicine(int level = 999)
    {
        itemType = ItemType.Medicine;

        if (level == 999)
        {
            SetItemLevel();
        }
        else
        {
            ItemLevel = level;
            LoadDBData();
        }
    }

    public override void ItemAction()
    {

        if (player != null)
        {
            if (player.CanHeal() == false)
            {
                Debug.Log("Can't Heal");
                return;

            }

            player.GetHp(Value);
            SoundManager.Instance.PlaySoundEffect("cure2");
            player.RemoveItem(this);
        }
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        ItemSpawner.Instance.SpawnItem(itemType, player.transform.position, null, false,ItemLevel);
    }
}

public class Item_Stimulant : ItemBase
{
    public Item_Stimulant(int level = 999)
    {
        itemType = ItemType.Stimulant;

        if (level == 999)
        {
            SetItemLevel();
        }
        else
        {
            ItemLevel = level;
            LoadDBData();
        }
    }

    public override void ItemAction()
    {
        if (player != null)
        {
            //이미 진통제 사용중일때는 못을어옴
            if (player.CanUseStimulant() == false)
            {
                Debug.Log("진통제 사용불가");
                return;
            }
            Debug.Log("30초간 " + Value + " 회복");
            player.UseStimulant(Value);
            SoundManager.Instance.PlaySoundEffect("cure1");
            player.RemoveItem(this);
        }
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        ItemSpawner.Instance.SpawnItem(itemType, player.transform.position, null, false,ItemLevel);
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



public class Item_Turret : ItemBase
{
    public Item_Turret()
    {
        itemType = ItemType.Turret;
    }
    public override void ItemAction()
    {
        Turret turret = ObjectManager.Instance.turretPool.GetItem();
        if (turret != null)
            turret.Initialize(player.transform.position, BulletType.PlayerBullet);
        player.RemoveItem(this);
    }
}

public class Item_FlashBang : ItemBase
{
    public Item_FlashBang()
    {
        itemType = ItemType.FlashBang;
    }

    public override void ItemAction()
    {
        //총알삭제
        ObjectManager.Instance.AllEnemyBulletDestroy();

        //스턴효과
        MonsterManager.Instance.StunAllMonster();


        SoundManager.Instance.PlaySoundEffect("flashbang2");
 
        player.RemoveItem(this);
    }
}
