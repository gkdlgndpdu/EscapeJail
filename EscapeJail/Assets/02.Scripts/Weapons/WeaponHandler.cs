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
    public AttackType attackType = AttackType.near;  
    private WeaponUI weaponUi;

    private Vector3 originPosit;

    //
    private Slider weaponSlider=null;




    public void SetWeaponUi(WeaponUI weaponUi)
    {
        this.weaponUi = weaponUi;
    }
    public void SetSlider(Slider weaponSlider)
    {
        this.weaponSlider = weaponSlider;
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

            UpdateWeaponUI();

            attackType = AttackType.near;

            return;
        }
        //무기가 있을때
        else
        {
            if (animator != null && weapon.weapontype != WeaponType.Flamethrower)
            {
                animator.speed = 0f;
                animator.Play(0,0);
            }

      

            nowWeapon = weapon;

            attackType = nowWeapon.AttackType;

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
        if (nowWeapon.AttackType == AttackType.near && nowWeapon.canFire() == true)
        {       
            SlashObjectOnOff(true);
        }

        fireDirection.Normalize();

        nowWeapon.FireBullet(firePos, fireDirection);

        UpdateWeaponUI();
    }
    
    private void UpdateWeaponUI()
    {
        if (weaponUi != null && nowWeapon != null)
            weaponUi.SetWeaponUI(nowWeapon.nowAmmo, nowWeapon.maxAmmo, nowWeapon.weapontype.ToString());
        else if (nowWeapon == null&& weaponUi != null)
            weaponUi.SetWeaponUiDefault();
    }

    private void OnDestroy()
    {
        nowWeapon = null;
    }

    private void Update()
    {
        if (nowWeapon != null)
            nowWeapon.WeaponUpdate(weaponSlider);
        else if (nowWeapon == null)
        {
            if(weaponSlider!=null)
            weaponSlider.gameObject.SetActive(false);
        }
    }
}
