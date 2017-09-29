using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using weapon;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class WeaponHandler : MonoBehaviour
{
    //컴포넌트
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Weapon nowWeapon;

    [SerializeField]
    private WeaponUI weaponUI;

    private Vector3 originPosit;
    private bool isShake = false;

    private System.Action weaponRotateFunc;

    public void SetWeaponRotateFunc(System.Action action)
    {
        weaponRotateFunc = action;
    }

    private void Awake()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    public void FlipWeapon(bool value)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipY = value;
    }



    public void ChangeWeapon(Weapon weapon)
    {
        if (weapon == null) return;

        if (animator != null)
            animator.speed = 0f;

        nowWeapon = weapon;

        if (animator != null)
        {
            animator.runtimeAnimatorController = ObjectManager.LoadGameObject(string.Format("Animators/Weapon/{0}", weapon.weapontype)) as RuntimeAnimatorController;

            if (nowWeapon != null)
            {
                nowWeapon.Initialize(animator);
                SetScale(nowWeapon.weaponScale);
                SetPostion(nowWeapon.relativePosition);

            }
        }

        UpdateWeaponUI();

    }

    private void SetScale(Vector3 size)
    {
        this.transform.localScale = size;
    }

    private void SetPostion(Vector3 posit)
    {
        originPosit = posit;
        this.transform.localPosition = posit;
    }

 

    public void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {
        if (nowWeapon == null) return;

        if (animator != null)
            animator.speed = 1f;

        if (nowWeapon.AttackType == AttackType.near && weaponRotateFunc != null)
            weaponRotateFunc();

        nowWeapon.FireBullet(firePos, fireDirection);

        UpdateWeaponUI();
    }
    
    private void UpdateWeaponUI()
    {
        if (weaponUI != null&& nowWeapon!=null)
            weaponUI.SetWeaponUI(nowWeapon.nowAmmo, nowWeapon.maxAmmo, nowWeapon.weapontype.ToString());
    }

    private void OnDestroy()
    {
        nowWeapon = null;
    }

    private void Update()
    {
        if (nowWeapon != null)
            nowWeapon.WeaponUpdate();
    }
}
