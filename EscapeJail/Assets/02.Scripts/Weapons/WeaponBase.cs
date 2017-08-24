using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class WeaponBase : MonoBehaviour
{
    //컴포넌트
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Weapon nowWeapon;

    [SerializeField]
    private WeaponUI weaponUI;

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

        if (animator != null)
        {
            animator.runtimeAnimatorController = ObjectManager.LoadGameObject(string.Format("Animators/Weapon/{0}", weapon.weaponName)) as RuntimeAnimatorController;
      
            if (nowWeapon != null)
                nowWeapon.Initialize(animator);
        }

        UpdateWeaponUI();

    }

    public void FireBullet(Vector3 firePos, Vector3 fireDirection)
    {
        if (nowWeapon == null) return;
        nowWeapon.FireBullet(firePos, fireDirection);

        UpdateWeaponUI();
    }

    private void UpdateWeaponUI()
    {
        if (weaponUI != null&& nowWeapon!=null)
            weaponUI.SetWeaponUI(nowWeapon.nowAmmo, nowWeapon.maxAmmo, nowWeapon.weaponName);
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
