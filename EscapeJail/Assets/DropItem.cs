using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;



[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class DropItem : MonoBehaviour, iReactiveAction
{
    //컴포넌트
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //속성

    private ItemType itemType = ItemType.Weapon;
    private WeaponType weapontype = WeaponType.Revolver;

    private CharacterBase player;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.speed = 0f;
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
        itemType = ItemType.Weapon;
        weapontype = weapon;

        if (spriteRenderer != null)
            spriteRenderer.sprite = null;

        if (animator != null)
        {
            RuntimeAnimatorController obj = ObjectManager.LoadGameObject(string.Format("Animators/Weapon/{0}", weapon.ToString())) as RuntimeAnimatorController;
            if (obj != null)
                animator.runtimeAnimatorController = obj;
        }


        SetColliderSize();


    }





    public void ClickAction()
    {
        Destroy(this.gameObject);
        switch (itemType)
        {
            case ItemType.Weapon:
                {
                    if (player != null)
                    {
                        Type type = Type.GetType("weapon." + weapontype.ToString());
                        if (type == null) return;
                        Weapon instance = Activator.CreateInstance(type) as Weapon;
                        if (instance == null) return;
                        player.AddWeapon(instance);
                    }

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
        }
    }


}
