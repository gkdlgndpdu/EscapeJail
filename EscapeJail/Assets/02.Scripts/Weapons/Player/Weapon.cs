using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace weapon
{
    //WeaponTable과 연동
    //반드시 클래스명과 일치해야 함
    public enum WeaponType
    {
        PlayerWeaponStart,
        Revolver, //1
        ShotGun,  //2
        WaterGun, //3
        AssaultRifle, //4
        Bazooka, //5
        LightSaber, //6
        Hammer, //7
        Flamethrower, //8
        BasicSniper, //9
        Minigun, //10
        Sword, //11
        Baseballbat, //12
        Shortknife, //13
        LaserPistol, //14
        CardCase, //15
        Condender,
        D_Eagle,
        AKL,
        Scal,
        Tal_21,
        WellRoad,
        M400,
        N16,
        Fars,
        Vectol,
        Scolpigeon,
        DoubleP2,
        K_Cobra,
        Gluck,
        Fire_seveN,
        M1G1,
        M1G2,
        PumpAction,
        //////////////////////////////////
        PlayerWeaponEnd,
        //////////////////////////////////
        MouseGun,
        CriminalPistol,
        CriminalShotGun,
        CriminalUzi,
        GuardPistol,
        GuardRifle,
        MouseRifle,
        Scientist_GasGun,
        Last1Gun,
        Last2Sniper,
        Last5Bazooka,
        LastBoss_Bazooka,
        LastBoss_MinuGun,
        LastBoss_Pistol,
        Scientist_PoisionGun,
        Last6Rifle,
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
        public Vector3 slashSize = Vector3.one * 7f;
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

        public void WeaponUpdate(Slider slider = null)
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

                if (slider != null)
                    slider.gameObject.SetActive(false);

                return;
            }

            if (slider != null)
            {
                if (slider != null)
                {
                    slider.gameObject.SetActive(true);
                    slider.value = fireCount / fireDelay;
                }

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

        protected void SetNearWeapon(Color slashColor, Vector3 slashSize)
        {
            attackType = AttackType.near;
            this.slashColor = slashColor;
            this.slashSize = slashSize;
            SetAmmo(1);
        }

    }
}