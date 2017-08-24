using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    protected Animator animator;

    public string weaponName;

    protected float bulletSpeed = 0f;

    protected Color BulletColor = Color.yellow;

    protected float fireDelay = 0f;
    protected float fireCount = 0f;
    protected bool isFireDelayFinish = true;

    public int maxAmmo = 10;
    public int nowAmmo = 10;
    public int needBulletToFire = 1;


    public void Initialize(Animator animator)
    {
        this.animator = animator;
    }

    public bool canFire()
    {
        if (nowAmmo <= 0)
            return false;
        else if (isFireDelayFinish == false)
            return false;
        else
            return true;
    }

    protected void useBullet()
    {
        nowAmmo -= needBulletToFire;
        if (nowAmmo <= 0)
        {
            nowAmmo = 0;
        }
    }

    public virtual void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {
        Debug.Log("자식에서 구현");
    }

    public void WeaponUpdate()
    {
        if (isFireDelayFinish == true) return;

        fireCount += Time.deltaTime;
        if (fireCount >= fireDelay)
        {
            isFireDelayFinish = true;
        }

    }

    public void FireDelayOn()
    {
        fireCount = 0f;
        isFireDelayFinish = false;
    }

    protected void PlayFireAnim()
    {
        if (animator != null)
            animator.SetTrigger("FireTrigger");
    }







}
