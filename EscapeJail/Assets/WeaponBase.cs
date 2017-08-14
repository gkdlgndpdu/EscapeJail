using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class WeaponBase : MonoBehaviour
{
    //컴포넌트
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Weapon nowWeapon;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    public void ChangeWeapon()
    {

    }

    public void FlipWeapon(bool value)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipY = value;
    }

    public void SetWeapon(Weapon weapon)
    {
        nowWeapon = weapon;
    
        animator.runtimeAnimatorController = Resources.Load(string.Format("Weapon/{0}", weapon.weaponName)) as RuntimeAnimatorController;
    
    }

    public void FireBullet(Vector3 firePos)
    {
        if (nowWeapon != null)
            nowWeapon.FireBullet(firePos);

        if (animator != null)
            animator.SetTrigger("FireTrigger");

    }
}
