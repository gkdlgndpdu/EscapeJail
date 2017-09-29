using System;
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
    private ItemBase itemBase;

    private CharacterBase player;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemBase = new ItemBase();

    }

    private void OnEnable()
    {


    }

    private void SetColliderSize()
    {
        if (boxCollider != null && spriteRenderer != null)
            boxCollider.size = spriteRenderer.size;
    }

    private void Start()
    {
        player = GamePlayerManager.Instance.player.GetComponent<CharacterBase>();
    }

    public void SetItemToWeapon(WeaponType weapon)
    {
        itemBase.itemType = ItemType.Weapon;
        itemBase.weapontype = weapon;

        if (spriteRenderer != null)
            spriteRenderer.sprite = null;

        if (spriteRenderer != null)
        {
            
            string ItemPath = string.Format("Sprites/Weapons/{0}", weapon.ToString());
            spriteRenderer.sprite = Resources.Load<Sprite>(ItemPath);
        }
        SetColliderSize();


    }

    public void SetItemToArmor(int level)
    {

        itemBase.itemType = ItemType.Armor;
        itemBase.itemName = ItemName.Armor1;
    
        if (spriteRenderer != null)
        {
            string ItemPath = string.Format("Sprites/Items/{0}", itemBase.itemName.ToString());
            spriteRenderer.sprite = Resources.Load<Sprite>(ItemPath);
        }

        SetColliderSize();

    }




    public void ClickAction()
    {
        if (player == null) return;

        Destroy(this.gameObject);
        switch (itemBase.itemType)
        {
            case ItemType.Weapon:
                {                   
                        Type type = Type.GetType("weapon." + itemBase.weapontype.ToString());
                        if (type == null) return;
                        Weapon instance = Activator.CreateInstance(type) as Weapon;
                        if (instance == null) return;
                        player.AddWeapon(instance);
                }
                break;
            case ItemType.Consumables:
                {

                }
                break;
            case ItemType.Bullet:
                {

                }
                break;
            case ItemType.Armor:
                {
                    player.AddArmor();
                } break;
        }
    }


}
