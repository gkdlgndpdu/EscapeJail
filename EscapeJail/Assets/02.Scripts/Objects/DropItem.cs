﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;


//스프라이트 명과 동일


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class DropItem : MonoBehaviour, iReactiveAction
{
    //컴포넌트
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    //속성
    public ItemBase itemBase;

    private CharacterBase player;
    private bool isSalesItem = false;
    private bool isSoldOut = false;
    public bool IsSalesItem
    {
        get
        {
            return isSalesItem;
        }
    }
    private int price = 0;
    public int Price
    {
        get
        {
            return price;
        }
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    private void OnEnable()
    {


    }

    private void SetColliderSize()
    {
        if (boxCollider != null && spriteRenderer != null)
            boxCollider.size = spriteRenderer.size * 0.5f;
    }

    private void Start()
    {
        player = GamePlayerManager.Instance.player.GetComponent<CharacterBase>();
    }

    public void SetItemToWeapon(WeaponType weapon)
    {
        itemBase = new Item_Weapon(weapon);


        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.weapontype.ToString());

        SetDropItemImage(ItemPath);
        SetColliderSize();

    }
    public void SetItemToSales()
    {
        isSalesItem = true;

        if (itemBase.itemType == ItemType.Weapon)
        {
            WeaponDB weaponData = DatabaseLoader.Instance.GetWeaponDB(itemBase.weapontype);
            if (weaponData != null)
            {
                price = weaponData.level * 1000;

                if (NowSelectPassive.Instance.HasPassive(PassiveType.DiscountCoupon) == true)
                {
                    price = (int)((float)price * 0.5f);
                }

            }
        }
        else
        {

        }


    }
    public void SetItemToArmor()
    {
        itemBase = new Item_Armor();

        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.itemName);
        SetDropItemImage(ItemPath);


        SetColliderSize();

    }
    public void SetItemToTurret()
    {
        itemBase = new Item_Turret();

        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.itemName);
        SetDropItemImage(ItemPath);


        SetColliderSize();
    }

    public void SetItemToBullet()
    {
        itemBase = new Item_Bullet();

        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.itemName);
        SetDropItemImage(ItemPath);


        SetColliderSize();
    }

    public void SetItemToMedicine(int level = 999)
    {
        itemBase = null;


        if (level == 999)
            itemBase = new Item_Medicine();
        else
            itemBase = new Item_Medicine(level);

        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.itemName);
        SetDropItemImage(ItemPath);


        SetColliderSize();
    }

    public void SetItemToStimulant(int level = 999)
    {
        itemBase = null;


        if (level == 999)
            itemBase = new Item_Stimulant();
        else
            itemBase = new Item_Stimulant(level);

        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.itemName);
        SetDropItemImage(ItemPath);


        SetColliderSize();
    }

    public void SetItemToBag()
    {
        itemBase = new Item_Bag();

        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.itemName);
        SetDropItemImage(ItemPath);


        SetColliderSize();
    }

    public void SetItemToFlashBang()
    {
        itemBase = new Item_FlashBang();

        string ItemPath = string.Format("Sprites/icon/{0}", itemBase.itemName);
        SetDropItemImage(ItemPath);


        SetColliderSize();

    }

    private void SetDropItemImage(string path)
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = Resources.Load<Sprite>(path);
    }





    //반응키로 눌렀을때
    public void ClickAction()
    {
        if (player == null) return;
        if (itemBase == null) return;

        if (player.isInventoryFull() == true &&
            //가방 , 아머일때는 인벤토리 크기 상관 x
            (itemBase.itemType != ItemType.Bag && itemBase.itemType != ItemType.Armor))
        {

            if (Language.Instance.NowLanguage == LanguageType.English)
                MessageBar.Instance.ShowInfoBar("Inventory Is Full", Color.red);
            else
                MessageBar.Instance.ShowInfoBar("가방이 가득 찼어요", Color.red);
            SoundManager.Instance.PlaySoundEffect("error");
            return;
        }

        if (isSalesItem == true)
        {
            if (player.Coin < price)
            {
                if (Language.Instance.NowLanguage == LanguageType.English)
                    MessageBar.Instance.ShowInfoBar("Not enough money", Color.red);
                else
                    MessageBar.Instance.ShowInfoBar("돈이 부족합니다.", Color.red);
                return;
            }
            else
            {
                if (player.isInventoryFull() == true) return;
                player.UseCoin(price);
                SoundManager.Instance.PlaySoundEffect("ItemBuy");
                isSoldOut = true;
            }
        }

        switch (itemBase.itemType)
        {
            case ItemType.Weapon:
                {
                    Type type = Type.GetType("weapon." + itemBase.weapontype.ToString());
                    if (type == null) break;
                    Weapon instance = Activator.CreateInstance(type) as Weapon;
                    if (instance == null) break;
                    player.AddWeapon(instance);
                    itemBase = null;

                }
                break;
            case ItemType.Bag:
                {
                    Item_Bag bag = itemBase as Item_Bag;
                    if (bag == null) return;

                    if (bag != null)
                        player.GetBag(bag.ItemLevel,bag.Value);
                    //해제
                    itemBase = null;

                }
                break;
            case ItemType.Armor:
                {
                    player.SetArmor(itemBase.Value);
                }
                break;
            default:
                {
                    player.AddItem(itemBase);
                }
                break;

        }
        Destroy(this.gameObject);

    }

    private void OnDestroy()
    { 
        if(itemBase==null)return;

        if (isSoldOut == false&&itemBase.itemType==ItemType.Weapon&&isSalesItem==true)
        {
            WeaponDB weaponDB = DatabaseLoader.Instance.GetWeaponDB(itemBase.weapontype);
            if (weaponDB != null)
                DatabaseLoader.Instance.weaponRandomGenerator.AddToList(itemBase.weapontype, weaponDB.Probability); ;
        }
    }


}