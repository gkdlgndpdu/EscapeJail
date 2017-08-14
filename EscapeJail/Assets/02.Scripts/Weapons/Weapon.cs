using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : iWeaponFire
{
   
    public virtual void Initialize(Animator animator)
    {
        this.animator = animator;
    }

    public virtual void FireBullet(Vector3 firePos)
    {
        throw new NotImplementedException();
    }

    protected Animator animator;

    public string weaponName;
 
}
