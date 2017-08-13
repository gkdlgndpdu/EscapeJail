using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public virtual void FireBullet(Vector3 firePos)
    {
        throw new NotImplementedException();
    }
    public void Initialize(Animator animator)
    {
        this.animator = animator;
    }   
    protected Animator animator;
 
}
