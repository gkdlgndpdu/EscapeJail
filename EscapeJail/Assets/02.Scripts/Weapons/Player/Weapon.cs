using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace weapon
{
    public enum WeaponType
    {
        PlayerWeaponStart,
        Revolver,
        ShotGun,
        WaterGun,
        AssaultRifle,
        Bazooka,
        LightSaber,
        Hammer,
        Flamethrower,
        BasicSniper,
        Minigun,
        sword,
        baseballbat,
        shortknife,
        laserpistol,
        CardCase,
        PlayerWeaponEnd,
        MouseGun,
        CriminalPistol,
        CriminalShotGun,
        CriminalUzi,
        GuardPistol,
        GuardRifle,
        AroundGun,
        Scientist_GasGun,
        Last1Gun,
        Last2Sniper,
        Last5Bazooka,
        LastBoss_Bazooka,
        LastBoss_MinuGun,
        LastBoss_Pistol,
        MonsterWeaponEnd
    }

    public enum AttackType
    {      
        gun,
        near        
    }

    public class Weapon : ItemBase
    {
        protected Animator animator;


        protected BulletType bulletType;
        protected AttackType attackType = AttackType.gun;
        public AttackType AttackType
        {
            get
            {
                return attackType;
            }
        }
        protected float bulletSpeed = 0f;

        protected Color BulletColor = Color.yellow;

        protected float fireDelay = 0f;
        protected float fireCount = 0f;
        protected bool isFireDelayFinish = true;

        public int maxAmmo = 10;
        public int nowAmmo = 10;
        public int needBulletToFire = 1;
        
        public Vector3 weaponScale = Vector3.one;
        public Vector3 relativePosition = Vector3.zero;

        public int damage = 1;

        //근접무기용
        public Color slashColor = Color.green;
        public Vector3 slashSize = Vector3.one*7f;
        public Weapon()
        {
            itemType = ItemType.Weapon;
        }

        public void GetBullet()
        {
            nowAmmo = maxAmmo;
        }
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

        public void WeaponUpdate(Slider slider=null)
        {
            if (isFireDelayFinish == true)
            {
                if (slider != null)
                {
                    slider.value = 0f;               
                }
                return;
            } 


            fireCount += Time.deltaTime;       
            if (fireCount >= fireDelay)
            {
                isFireDelayFinish = true;
                slider.gameObject.SetActive(false);
                return;
            }

            if (slider != null)
            {
                slider.gameObject.SetActive(true);
                slider.value = fireCount / fireDelay;
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



        protected void SetAmmo(int num)
        {
            nowAmmo = num;
            maxAmmo = num;
        }

        protected void SetNearWeapon(Color slashColor,Vector3 slashSize)
        {
            attackType = AttackType.near;
            this.slashColor = slashColor;
            this.slashSize = slashSize;
            SetAmmo(1);
        }

    }
}