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
    private SlashObject slashObject;

    [SerializeField]
    private WeaponUI weaponUI;

    private Vector3 originPosit;

    //
    private Slider weaponSlider=null;


    private System.Action weaponRotateFunc;

    public void SetSlider(Slider weaponSlider)
    {
        this.weaponSlider = weaponSlider;
    }

    public void SetWeaponRotateFunc(System.Action action)
    {
        weaponRotateFunc = action;
    }

    public void SetSlashObject(SlashObject slashObject)
    {
        this.slashObject = slashObject;
    }

    private void Awake()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();    
    }

    public void GetBulletItem()
    {
        if (nowWeapon == null) return;
        nowWeapon.GetBullet();

        UpdateWeaponUI();
    }

    public void FlipWeapon(bool value)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipY = value;
    }



    public void ChangeWeapon(Weapon weapon)
    {
        //무기가 없을때 들어옴(아무것도 없을때)
        if (weapon == null)
        {
            nowWeapon = null;
            
            //이미지 제거
            if(animator!=null)
            animator.runtimeAnimatorController = null;

            if (spriteRenderer != null)
                spriteRenderer.sprite = null;

            return;
        }
        //무기가 있을때
        else
        {
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

            //바뀐 무기가 근접무기일경우 근접무기 세팅
            if (nowWeapon.AttackType == AttackType.near)
            {
                if (slashObject != null)
                {
                    slashObject.Initialize(nowWeapon.damage, nowWeapon.slashColor, nowWeapon.slashSize);
                }
            }

            //근접무기가 아니면 꺼줌
            else
            {
                if (slashObject != null)
                    SlashObjectOnOff(false);
            }

            UpdateWeaponUI();
        }     

    }

    private void SlashObjectOnOff(bool OnOff)
    {
        if (slashObject != null)
            slashObject.gameObject.SetActive(OnOff);
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

        //근접무기용
        if (nowWeapon.AttackType == AttackType.near && weaponRotateFunc != null && nowWeapon.canFire() == true)
        {
            weaponRotateFunc();
            SlashObjectOnOff(true);
        }

        fireDirection.Normalize();

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
            nowWeapon.WeaponUpdate(weaponSlider);
    }
}
