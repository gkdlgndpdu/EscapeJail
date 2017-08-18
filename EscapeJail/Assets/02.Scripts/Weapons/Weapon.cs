using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : iWeaponFire
{
    protected Animator animator;

    public string weaponName;

    protected float bulletSpeed = 0f;

    protected Color BulletColor = Color.yellow;

    public virtual void Initialize(Animator animator)
    {
        this.animator = animator;
    }

    public virtual void FireBullet(Vector3 firePos)
    {
        throw new NotImplementedException();
    }

 
 
}
