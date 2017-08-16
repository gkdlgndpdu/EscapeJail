﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : MonoBehaviour
{
    //컴포넌트    
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    //값변수
    [SerializeField]
    protected float moveSpeed = 10f;

    //무기
    [SerializeField]
    protected WeaponBase nowWeapon;

    //무기 장착 위치
    [SerializeField]
    protected Transform weaponPosit;

    protected List<Weapon> nowHaveItem;




    protected void Awake()
    {
        SetLayerAndTag();
        Initialize();
    }

    private void SetLayerAndTag()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        this.gameObject.tag = "Player";
    }
    protected void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        nowHaveItem = new List<Weapon>();

        //임시로 총기 넣어줌
        nowHaveItem.Add(new Revolver());
        nowHaveItem.Add(new ShotGun());
        nowWeapon.SetWeapon(nowHaveItem[0]);

        //컴포넌트
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();


    }

    // Use this for initialization
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {

        HandleNowWeapon();
    }

    protected void FixedUpdate()
    {
        MoveInPc();
    }

    protected void HandleNowWeapon()
    {
        //발사
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (nowWeapon == null) return;
            nowWeapon.FireBullet(this.transform.position);
        }

        //회전
        RotateWeapon();

        //무기 변경
        ChangeWeapon();
    }

    protected void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nowWeapon.SetWeapon(nowHaveItem[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nowWeapon.SetWeapon(nowHaveItem[1]);
        }
    }

    protected void RotateWeapon()
    {
        Vector3 nearestEnemyPos = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
        float angle = MyUtils.GetAngle(nearestEnemyPos, this.transform.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);

        //flip
        if ((angle >= 0f && angle <= 90) ||
              angle >= 270f && angle <= 360)
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(false);

        }
        else
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(true);
        }




    }

    protected void MoveInPc()
    {
        rb.velocity = Vector2.zero;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");       

        Vector3 moveDir = Vector3.right * h + Vector3.up * v;
        // moveDir.Normalize();

        //이동
        if (rb != null)
            rb.velocity = moveDir * moveSpeed * Time.deltaTime;

        //애니메이션
        AnimControl(moveDir);
    }

    protected void AnimControl(Vector3 MoveDir)
    {
        ChangeAnimation(MoveDir);
        FlipCharacter(MoveDir);

        //if (SpeedValue > 0.2f)
        //{
        //    animator.speed = 1f;
        //    animator.SetFloat("DirectionX", MoveDir.x);
        //    animator.SetFloat("DirectionY", MoveDir.y);
        //}
        //else
        //{
        //    animator.Play("Walk", -1, 0f);
        //    animator.speed = 0f;
        //}

    }

    protected void ChangeAnimation(Vector3 MoveDir)
    {
        if (animator == null) return;

        float SpeedValue = Mathf.Abs(MoveDir.x) + Mathf.Abs(MoveDir.y);
        animator.SetFloat("Speed", SpeedValue);

        FlipCharacter(MoveDir);

    }

    protected void FlipCharacter(Vector3 MoveDir)
    {
        if (spriteRenderer == null) return;
        if (MoveDir.x < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (MoveDir.x > 0f)
        {
            spriteRenderer.flipX = false;
        }
    }
}
