using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace weapon
{
    public enum WeaponKind
    {
        Pistol,
        AR,
        SMG,
    }

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
        ASI,
        HP5,
        TomSon,
        UP45,
        Uze,
        Dragunoob,
        Mtw,
        War2000,
        PMP45,
        PPAP900,
        K2G,
        Saigong12,
        Spa20,
        SuperShort,
        Car98,
        H249,
        HWMMG,
        ChickenGun,
        RhinoGun,
        PowerGauntlet,
        DragonBow,
        E_Desert,
        FingerGun,
        TenisBallShooter,
        Sturgeon,
        RoseGun,
        G403,
        GiraffeSword,
        Boomerang,
        MemoryEraser,
        BomberGun,
        AirGun,
        CueGun,
        MagicStick,
        Bfe,
        GravityGun,
        MindArrow,
        Gramophone,
        BubbleGun,
        GuitarCase,
        KeyBoardShotGun,
        Mjolnir,
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
        private WeaponKind weaponKind;

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

        private float reBoundValue = 5f;
        public float ReBoundValue
        {
            get
            {

                return reBoundValue;
            }
        }

        //반동관련
        protected float reboundDuringFire = 0f;
        protected bool hasRebuondDuringFire = false;
        protected float reboundAddValue = 0f;
        protected float reboundRecoverValue = 0f;
        protected float lastFireTime = 0f;
        protected float maxRebound;
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

        protected void SetWeaponKind(WeaponKind kind)
        {
            this.weaponKind = kind;
            SetAmmoByKind();
        }

        protected void SetReBound(float bound)
        {
            reBoundValue = bound;
        }

        public virtual void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            Debug.Log("자식에서 구현");
        }

        public override void ItemAction()
        {
            Debug.Log("무기변경" + weapontype.ToString());
            GamePlayerManager.Instance.player.ChangeSpeceficWeapon(weapontype);
        }


        protected void AddRebound()
        {
            if (reboundDuringFire <= maxRebound)
                reboundDuringFire += reboundAddValue;


        }



        /// <summary>
        /// normalize 값 반환
        /// </summary>       
        protected Vector3 ApplyReboundDirection(Vector3 originDir)
        {
            Vector3 returnValue = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-reboundDuringFire, reboundDuringFire)) * originDir;
            return returnValue.normalized;
        }

        public void WeaponUpdate(Slider slider = null, Slider reboundSlider = null)
        {
            if (hasRebuondDuringFire == true)
            {
                if (fireCount < fireDelay)
                    lastFireTime += Time.deltaTime * 1.1f;

                lastFireTime -= Time.deltaTime;
                if (lastFireTime <= 0)
                {
                    reboundDuringFire = 0f;
                    lastFireTime = 0f;
                    if (reboundSlider != null)
                        reboundSlider.gameObject.SetActive(false);
                }

                if (reboundDuringFire > 0f)
                {
                    reboundDuringFire -= reboundRecoverValue * Time.deltaTime;
                    if (reboundSlider != null)
                    {
                        reboundSlider.gameObject.SetActive(true);
                        reboundSlider.value = reboundDuringFire / maxRebound;
                    }
                }
                if (reboundDuringFire < 0f)
                {
                    reboundDuringFire = 0f;
                    if (reboundSlider != null)
                        reboundSlider.gameObject.SetActive(false);
                }
            }
            else if (hasRebuondDuringFire == false)
            {
                if (reboundSlider != null)
                    reboundSlider.gameObject.SetActive(false);
            }

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



        protected void SetReboundDuringFire(float addValue, float recoverValue, float maxRebound = 45f)
        {
            hasRebuondDuringFire = true;
            reboundDuringFire = 0f;
            reboundAddValue = addValue;
            reboundRecoverValue = recoverValue;
            this.maxRebound = maxRebound;
        }





        protected void SetAmmo(int num)
        {
            if ((PassiveType)PlayerPrefs.GetInt(GameConstants.PassiveKeyValue) == PassiveType.ExtendedMag && num != 1)
            {
                num += (int)((float)num * 0.5f);
            }

            nowAmmo = num;
            maxAmmo = num;
        }

        private void SetAmmoByKind()
        {
            switch (weaponKind)
            {
                case WeaponKind.Pistol:
                    SetAmmo(100);
                    break;
                case WeaponKind.AR:
                    SetAmmo(200);
                    break;
                case WeaponKind.SMG:
                    SetAmmo(150);
                    break;
            }
        }

        protected void SetNearWeapon(Color slashColor, Vector3 slashSize)
        {
            attackType = AttackType.near;
            this.slashColor = slashColor;
            this.slashSize = slashSize;
            SetAmmo(1);
        }

        protected void FireHitScan(Vector3 firePos, Vector3 fireDirection, int damage, Color color = default(Color), bool setPush = false, float pushPower = 3f)
        {
            int layerMask = (1 << LayerMask.NameToLayer("Enemy") | (1 << LayerMask.NameToLayer("Tile")) | (1 << LayerMask.NameToLayer("ItemTable")));
            Ray2D ray = new Ray2D(firePos, fireDirection);
            RaycastHit2D hit = Physics2D.Raycast(firePos, fireDirection, 50f, layerMask);
            if (hit == true)
            {
                CharacterInfo characterInfo = hit.transform.gameObject.GetComponent<CharacterInfo>();
                if (characterInfo != null)
                {
                    if (setPush == true)
                        characterInfo.SetPush(firePos, pushPower, 0);

                    characterInfo.GetDamage(damage);
                }
                //라인 그려주기
                DrawLiner line = ObjectManager.Instance.linePool.GetItem();
                if (line != null)
                {
                    line.Initialize(firePos, hit.point);
                }

                if (color != default(Color))
                {
                    line.SetLineColor(color);
                }
            }
        }



    }
}
