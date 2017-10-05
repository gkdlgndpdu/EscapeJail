﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using weapon;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : CharacterInfo
{
    //컴포넌트    
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;



    //값변수
    protected float moveSpeed = 5f;

    //무기 회전 관련
    protected float weaponAngle = 0f;
    protected bool nowWeaponRotate = false;
    [SerializeField]
    protected WeaponRotator weaponRotator;
    //protected int hp = 0;
    //protected int maxHp = 0;

    //무기
    [SerializeField]
    protected WeaponHandler weaponHandler;

    //무기 장착 위치
    [SerializeField]
    protected Transform weaponPosit;

    [SerializeField]
    protected Transform FirePos;

    //UI
    [SerializeField]
    protected PlayerUI playerUI;
    [SerializeField]
    protected InventoryUi inventoryUi;

    //인벤토리
    protected Inventory inventory;

    protected void Awake()
    {
        SetLayerAndTag();
        Initialize();

        if (weaponRotator != null && weaponHandler != null)
            weaponHandler.SetWeaponRotateFunc(weaponRotator.RotateWeapon);
    }

    private void SetLayerAndTag()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        this.gameObject.tag = "Player";
    }
    protected void Initialize()
    {
        //컴포넌트
        SetupComponent();

        //스크립트
        if (inventoryUi != null)
            inventory = new Inventory(inventoryUi);

        if (inventory != null)
            inventory.SetInventorySize(0);
    }

    public void GetBag(int level)
    {
        if (inventory != null)
            inventory.SetInventorySize(level);
    }



    protected void SetupComponent()
    {
        //컴포넌트
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {

        HandleNowWeapon();

#if UNITY_ANDROID
        MoveInMobie();
#endif
#if UNITY_EDITOR
        InputOnPc();
#endif

    }

    protected void InputOnPc()
    {
        MoveInPc();
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReactiveButtonClick();
        }       


    }

    public void FireWeapon()
    {
        GameObject nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);

        if (nearEnemy != null && weaponHandler != null)
        {
            Vector3 fireDir = nearEnemy.transform.position - this.transform.position;
            weaponHandler.FireBullet(this.FirePos.position, fireDir);


        }
        else if (nearEnemy == null)
        {
            Vector3 fireDir = Vector3.right;
            weaponHandler.FireBullet(this.FirePos.position, fireDir);
        }


    }


    private void RotateWeapon()
    {
        if (weaponRotator.nowRotate == true) return;

        GameObject nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
        if (nearEnemy != null)
            RotateWeapon(nearEnemy.transform.position);
        else
        {
            RotateWeapon(this.transform.position + Vector3.right);
        }
    }

    protected void HandleNowWeapon()
    {
        GameObject nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);


#if UNITY_EDITOR
        //발사
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireWeapon();

        }
        //무기 변경
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }
#endif


        RotateWeapon();

    }

    public void ChangeWeapon()
    {
        weaponHandler.ChangeWeapon(inventory.GetWeapon());
    }

    protected void RotateWeapon(Vector3 enemyPos)
    {
        if (nowWeaponRotate == true) return;

        Vector3 nearestEnemyPos = enemyPos;
        weaponAngle = MyUtils.GetAngle(nearestEnemyPos, this.transform.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, weaponAngle);

        //flip
        if ((weaponAngle >= 0f && weaponAngle <= 90) ||
              weaponAngle >= 270f && weaponAngle <= 360)
        {
            if (weaponHandler != null)
            {
                weaponHandler.FlipWeapon(false);
                FlipCharacter(true);
            }
        }
        else
        {
            if (weaponHandler != null)
            {
                weaponHandler.FlipWeapon(true);
                FlipCharacter(false);
            }
        }

    }

    protected void NearWeaponRotate()
    {
        nowWeaponRotate = true;

    }

    protected void NearWeaponRotateEnd()
    {
        Debug.Log("들어오냐?");
        iTween.Stop(weaponPosit.gameObject);
        nowWeaponRotate = false;
    }

    protected void MoveInPc()
    {
        if (rb != null)
            rb.velocity = Vector2.zero;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = Vector3.right * h + Vector3.up * v;
        moveDir.Normalize();

        //이동
        if (rb != null)
            rb.velocity = moveDir * moveSpeed;

        //애니메이션
        AnimControl(moveDir);

    }

    protected void MoveInMobie()
    {
        if (rb != null)
            rb.velocity = Vector2.zero;

        Vector3 moveDir = JoyStick.Instance.MoveDir;
        //이동
        if (rb != null)
            rb.velocity = moveDir * moveSpeed;
        //애니메이션
        AnimControl(moveDir);
    }





    protected void AnimControl(Vector3 MoveDir)
    {
        ChangeAnimation(MoveDir);
    }

    protected void ChangeAnimation(Vector3 MoveDir)
    {
        if (animator == null) return;

        float SpeedValue = Mathf.Abs(MoveDir.x) + Mathf.Abs(MoveDir.y);
        animator.SetFloat("Speed", SpeedValue);


    }

    protected void FlipCharacter(bool flip)
    {
        if (spriteRenderer == null) return;

        spriteRenderer.flipX = flip;



    }

    public override void GetDamage(int damage)
    {
        hp -= damage;

        UIUpdate();

        if (hp <= 0)
        {
            DieAction();
        }
    }

    protected void UIUpdate()
    {
        if (playerUI != null)
            playerUI.SetHpBar(hp, hpMax);
    }

    protected void DieAction()
    {
        Debug.Log("CharacterDie");
    }

    public void HandleItem()
    {



    }

    public void GetBulletItem()
    {
        if (weaponHandler != null)
        {
            weaponHandler.GetBulletItem();

        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (inventory != null && weapon != null)
        {
            inventory.AddWeapon(weapon);
            weaponHandler.ChangeWeapon(inventory.GetWeapon());
        }
    }

    public void RemoveWeapon(ItemBase weapon)
    {
        if (inventory != null && weapon != null)
        {
            inventory.RemoveWeapon(weapon);
            weaponHandler.ChangeWeapon(inventory.GetWeapon());
        }
    }
    public void AddItem(ItemBase item)
    {
        if (inventory != null && item != null)
        {
            inventory.AddToInventory(item);
        }
    }
    public void RemoveItem(ItemBase item)
    {
        if (inventory != null && item != null)
        {
            inventory.RemoveInInventory(item);
        }
    }

    public bool isInventoryFull()
    {
        if (inventory == null) return true;

        return inventory.isInventoryFull();
    }

    //반응키 눌렸을때
    public void ReactiveButtonClick()
    {

        int activeLayer = MyUtils.GetLayerMaskByString("DropItem");
        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, 0.5f, activeLayer);

        if (colls == null) return;
        if (colls.Length == 0) return;

        if (colls.Length == 1)
        {
            iReactiveAction action = colls[0].gameObject.GetComponent<iReactiveAction>();
            if (action != null)
                action.ClickAction();
        }
        else if (colls.Length > 1)
        {
            Array.Sort(colls, (a, b) =>
            {
                if (Vector3.Distance(this.transform.position, a.transform.position) >
                Vector3.Distance(this.transform.position, b.transform.position))
                    return 1;
                else return -1;

            });

            iReactiveAction action = colls[0].gameObject.GetComponent<iReactiveAction>();
            if (action != null)
                action.ClickAction();

        }



    }


}
