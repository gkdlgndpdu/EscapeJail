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
    public Weapon NowWeapon
    {
        get
        {
            return nowWeapon;
        }
    }
    private SlashObject slashObject;
    [HideInInspector]
    public AttackType attackType = AttackType.near;
    private WeaponUI weaponUi;

    private Vector3 originPosit;

    //
    private Slider weaponSlider = null;
    private ReboundProgress reboundProgress = null;

    private AudioSource audioSource;
    public void SetAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = SoundManager.Instance.EffectVolume;
        AudioClip clip = SoundManager.Instance.GetClip("noammo");
        if (clip != null)
            audioSource.clip = clip;
    }

    public bool CanRotateWeapon()
    {
        if (nowWeapon == null) return false;
        if (nowWeapon.weapontype == WeaponType.ChickenGun||
            nowWeapon.weapontype == WeaponType.MemoryEraser||
            nowWeapon.weapontype == WeaponType.Gramophone) return false;

        return true;
    }

    //총알 채울수 있는지
    public bool CanReload()
    {
        //무기를 안꼇거나
        if (nowWeapon == null) return false;
        //총알이 풀이면 안됨
        if (nowWeapon.nowAmmo == nowWeapon.maxAmmo) return false;

        return true;
    }

    public void SetWeaponUi(WeaponUI weaponUi)
    {
        this.weaponUi = weaponUi;
    }
    public void SetSlider(Slider weaponSlider, ReboundProgress reboundProgress)
    {
        this.weaponSlider = weaponSlider;
        this.reboundProgress = reboundProgress;
    }


    public void SetSlashObject(SlashObject slashObject)
    {
        this.slashObject = slashObject;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = null;


    }
         
    public void GetBulletItem(int value)
    {
        if (nowWeapon == null) return;
        nowWeapon.GetBullet(value);

        UpdateWeaponUI();
    }

    public void FlipWeapon(bool value)
    {
        if (CanRotateWeapon() == false)
        {
            if (spriteRenderer != null)
                spriteRenderer.flipY = false;
            return;
        }
       
        if (spriteRenderer != null)
            spriteRenderer.flipY = value;
    }



    public void ChangeWeapon(Weapon weapon)
    {
        if (reboundProgress != null)
            reboundProgress.gameObject.SetActive(false);

        //무기가 없을때 들어옴(아무것도 없을때)
        if (weapon == null)
        {
            nowWeapon = null;

            //이미지 제거
            if (animator != null)
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
            SoundManager.Instance.PlaySoundEffect("changeWeapon");
            nowWeapon = weapon;

            attackType = nowWeapon.AttackType;

            //if (animator == null)
            //    animator=GetComponent<Animator>();

            if (animator != null)
            {
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(string.Format("Animators/Weapon/{0}", weapon.weapontype));

                if (nowWeapon != null)
                {
                    nowWeapon.Initialize(animator);
                    SetScale(nowWeapon.weaponScale);
                    SetPostion(nowWeapon.relativePosition);

                }

                if (animator != null && weapon.weapontype != WeaponType.Flamethrower)
                {
                    animator.Play("Fire", 0, 0f);
                    animator.speed = 0f;

                    if (hasIdleAnimation())
                        animator.speed = 1f;
                }

                if (CanRotateWeapon() == false)
                    FlipWeapon(false);
            }

            //바뀐 무기가 근접무기일경우 근접무기 세팅
            if (nowWeapon.AttackType == AttackType.near)
            {
                if (slashObject != null)
                {
                    slashObject.Initialize(nowWeapon.damage, nowWeapon.slashColor, nowWeapon.slashSize,nowWeapon.weapontype);
                }
            }
            //근접무기가 아니면 꺼줌
            else
            {
                if (slashObject != null)
                    SlashObjectOnOff(false);
            }

            //카드케이스
            if (nowWeapon != null)
            {
                if (nowWeapon.weapontype == WeaponType.CardCase)
                {

                    GamePlayerManager.Instance.player.CardCaseCardOnOff(true);
                }
                else
                {
                    GamePlayerManager.Instance.player.CardCaseCardOnOff(false);
                }
            }
            else if (nowWeapon == null)
            {
                GamePlayerManager.Instance.player.CardCaseCardOnOff(false);
            }

            UpdateWeaponUI();
        }

    }

    private bool hasIdleAnimation()
    {
        if (nowWeapon == null) return false;

        if (nowWeapon.weapontype == WeaponType.ChickenGun ||
            nowWeapon.weapontype == WeaponType.RhinoGun ||
            nowWeapon.weapontype == WeaponType.PowerGauntlet ||
             nowWeapon.weapontype == WeaponType.Sturgeon ||
              nowWeapon.weapontype == WeaponType.RoseGun ||
                 nowWeapon.weapontype == WeaponType.G403||
                    nowWeapon.weapontype == WeaponType.GiraffeSword||
                    nowWeapon.weapontype ==WeaponType.Gramophone
             )
            return true;

        return false;
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

        if (audioSource != null)
        {
            

            if (nowWeapon.hasAmmo() == false && audioSource.isPlaying==false)
            {
                audioSource.volume = SoundManager.Instance.EffectVolume;
                audioSource.Play();
                MessageBar.Instance.ShowInfoBar("Out of ammo",Color.red);
            }
        }
   

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
        else if (nowWeapon == null && weaponUi != null)
            weaponUi.SetWeaponUiDefault();
    }

    private void OnDestroy()
    {
        nowWeapon = null;
       
    }

    private void Update()
    {
        if (nowWeapon != null)
            nowWeapon.WeaponUpdate(weaponSlider, reboundProgress);
        else if (nowWeapon == null)
        {
            if (weaponSlider != null)
                weaponSlider.gameObject.SetActive(false);
        }
    }
}
