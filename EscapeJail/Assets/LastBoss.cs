using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class LastBoss : BossBase
{

    [SerializeField]
    private WeaponHandler weaponHandler;

    private Transform PlayerTr;
    private Vector3 moveDir;
    private bool canMove = true;



    private Dictionary<WeaponType, Weapon> weaponDic = new Dictionary<WeaponType, Weapon>();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="OnOff">true하면 무기 숨김 false는 반대</param>
    private void WeaponHideOnOff(bool OnOff)
    {
        if (OnOff == true)
        {
            if (weaponHandler != null)
                iTween.FadeTo(weaponHandler.gameObject, 0f, 0.8f);
        }
        else if (OnOff == false)
        {
            if (weaponHandler != null)
                iTween.FadeTo(weaponHandler.gameObject, 1f, 0.8f);
        }

    }

    private new void Awake()
    {
        base.Awake();
        SetHp(30);
        RegistPatternToQueue();
        SetWeapon();

        WeaponHideOnOff(true);

    }

    private new void Start()
    {
        LinkPlayer();
        weaponHandler.ChangeWeapon(new LastBoss_Pistol());
    }

    private void Update()
    {
        if (isPatternStart == false) return;
        RotateWeapon();
        MoveToTarget();
    }

    private void SetWeapon()
    {
        weaponDic.Add(WeaponType.LastBoss_Pistol, new LastBoss_Pistol());
        weaponDic.Add(WeaponType.LastBoss_MinuGun, new LastBoss_MinuGun());
        weaponDic.Add(WeaponType.LastBoss_Bazooka, new LastBoss_Bazooka());
    }

    private void ChangeWeapon(WeaponType weaponType)
    {
        if (weaponDic == null) return;

        if (weaponDic.ContainsKey(weaponType) == true)
        {
            weaponHandler.ChangeWeapon(weaponDic[weaponType]);
        }
    }

    private void LinkPlayer()
    {
        PlayerTr = GamePlayerManager.Instance.player.transform;
    }


    protected void RotateWeapon()
    {
        Vector3 playerPos = Vector3.one;

        if (PlayerTr != null)
            playerPos = PlayerTr.position;

        float weaponAngle = MyUtils.GetAngle(playerPos, this.transform.position);
        if (weaponHandler != null)
            weaponHandler.transform.rotation = Quaternion.Euler(0f, 0f, weaponAngle);

        //flip
        if ((weaponAngle >= 0f && weaponAngle <= 90) ||
              weaponAngle >= 270f && weaponAngle <= 360)
        {
            if (weaponHandler != null)
            {
                weaponHandler.FlipWeapon(false);
                FlipCharacter(false);
            }
        }
        else
        {
            if (weaponHandler != null)
            {
                weaponHandler.FlipWeapon(true);
                FlipCharacter(true);

            }
        }

    }

    private void FlipCharacter(bool OnOff)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = OnOff;
    }

    private void FireNowWeapon()
    {
        if (weaponHandler != null)
        {
            Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
            weaponHandler.FireBullet(this.transform.position, fireDir);
        }
    }


    private enum Actions
    {
        FireStartTrigger,
        DeadTrigger,
        FireEndTrigger,
        BombAttackStart,
        Walk,
        Idle
    }

    public override void StartBossPattern()
    {
        base.StartBossPattern();
        Debug.Log("CriminalPattern Start");

        if (bossEventQueue != null)
            bossEventQueue.StartEventQueue();

        isPatternStart = true;
    }



    private void Action(Actions action)
    {
        switch (action)
        {
            case Actions.FireStartTrigger:
                {
                    if (animator != null)
                        animator.SetTrigger("FireStartTrigger");
                }
                break;
            case Actions.DeadTrigger:
                {
                    if (animator != null)
                        animator.SetTrigger("DeadTrigger");
                }
                break;
            case Actions.FireEndTrigger:
                {
                    if (animator != null)
                        animator.SetTrigger("FireEndTrigger");
                }
                break;
            case Actions.BombAttackStart:
                {
                    if (animator != null)
                        animator.SetTrigger("BombAttackStart");
                }
                break;
            case Actions.Walk:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 1f);
                }
                break;
            case Actions.Idle:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 0f);
                }
                break;
        }
    }

    #region Pattern
    private void RegistPatternToQueue()
    {

        bossEventQueue.Initialize(this, EventOrder.InOrder);

        bossEventQueue.AddEvent("SpreadBomb");
        bossEventQueue.AddEvent("TempFireRoutine1");
        bossEventQueue.AddEvent("TempFireRoutine2");
        bossEventQueue.AddEvent("TempFireRoutine3");

    }

    IEnumerator TempFireRoutine1()
    {
        Action(Actions.FireStartTrigger);
        WeaponHideOnOff(false);
        ChangeWeapon(WeaponType.LastBoss_Pistol);
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 10; i++)
        {
            FireNowWeapon();
            yield return new WaitForSeconds(0.5f);
        }
        Action(Actions.FireEndTrigger);
        WeaponHideOnOff(true);
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator TempFireRoutine2()
    {
        Action(Actions.FireStartTrigger);
        WeaponHideOnOff(false);
        ChangeWeapon(WeaponType.LastBoss_MinuGun);
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 20; i++)
        {
            FireNowWeapon();
            yield return new WaitForSeconds(0.1f);
        }
        Action(Actions.FireEndTrigger);
        WeaponHideOnOff(true);

        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator TempFireRoutine3()
    {
        Action(Actions.FireStartTrigger);
        WeaponHideOnOff(false);
        ChangeWeapon(WeaponType.LastBoss_Bazooka);
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 5; i++)
        {
            FireNowWeapon();
            yield return new WaitForSeconds(0.5f);
        }
        Action(Actions.FireEndTrigger);
        WeaponHideOnOff(true);
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator SpreadBomb()
    {
        canMove = false;
        Action(Actions.BombAttackStart);
        yield return new WaitForSeconds(3.0f);
        canMove = true;
    }
    #endregion


    public void SpreadDynamiteAndGranade()
    {           
        Vector3 fireDirection = Vector3.right;
        for (int i = 0; i < 8; i++)
        {
            float dynamaiteSpeed = Random.Range(1f, 5f);
            fireDirection = Quaternion.Euler(0f, 0f, 45f) * fireDirection;
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.Initialize(this.transform.position, fireDirection.normalized, dynamaiteSpeed, BulletType.EnemyBullet, 1f, 1, 3f);
                bullet.InitializeImage("Dynamite", true);
                bullet.SetBloom(false);
                bullet.SetDestroyByCollision(false);
                bullet.SetMoveLifetime(1f);
                bullet.SetEffectName("Explode_1", 3f);
                bullet.SetExplosion(1.5f);
               
            }
        }


        Vector3 fireDirection2 =Quaternion.Euler(0f,0f,22.5f)* Vector3.right;
        for (int i = 0; i < 8; i++)
        {
            float dynamaiteSpeed = Random.Range(5f, 8f);
            fireDirection2 = Quaternion.Euler(0f, 0f, 45f) * fireDirection2;
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.Initialize(this.transform.position, fireDirection2.normalized, dynamaiteSpeed, BulletType.EnemyBullet, 1f, 1, 3f);
                bullet.InitializeImage("Dynamite", true);
                bullet.SetBloom(false);
                bullet.SetDestroyByCollision(false);
                bullet.SetMoveLifetime(1f);
                bullet.SetEffectName("Explode_1", 3f);
                bullet.SetExplosion(1.5f);

            }
        }

    }
    protected void CalculateMoveDIr()
    {
        if (PlayerTr == null) return;
        moveDir = PlayerTr.position - this.transform.position;
    }

    protected void MoveToTarget()
    {
        if (rb == null) return;

        CalculateMoveDIr();

        if (PlayerTr == null) return;

        if (rb != null && canMove == true)
            rb.velocity = moveDir.normalized * moveSpeed;
        else if (canMove == false)
            rb.velocity = Vector3.zero;

    }

}

