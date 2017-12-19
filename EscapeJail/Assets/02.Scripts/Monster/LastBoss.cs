using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class LastBoss : BossBase
{

    [SerializeField]
    private WeaponHandler weaponHandler1;
    [SerializeField]
    private WeaponHandler weaponHandler2;

    private Transform PlayerTr;
    private Vector3 moveDir;
    private bool canMove = true;
    private bool isHideOn = false;

    [SerializeField]
    private List<LastBossFirePosition> firePosits;

    [SerializeField]
    public Transform leftTop;
    [SerializeField]
    public Transform rightTop;


    private Dictionary<WeaponType, Weapon> weaponDic1 = new Dictionary<WeaponType, Weapon>();
    private Dictionary<WeaponType, Weapon> weaponDic2 = new Dictionary<WeaponType, Weapon>();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="OnOff">true하면 무기 숨김 false는 반대</param>
    private void WeaponHideOnOff(bool OnOff)
    {
        if (OnOff == true)
        {
            if (weaponHandler1 != null)
                iTween.FadeTo(weaponHandler1.gameObject, 0f, 0.8f);

            if (weaponHandler2 != null)
                iTween.FadeTo(weaponHandler2.gameObject, 0f, 0.8f);

        }
        else if (OnOff == false)
        {
            if (weaponHandler1 != null)
                iTween.FadeTo(weaponHandler1.gameObject, 1f, 0.8f);

            if (weaponHandler2 != null)
                iTween.FadeTo(weaponHandler2.gameObject, 1f, 0.8f);

        }

    }
    private void HideOn()
    {
        isHideOn = true;
        DeleteInList();

        if (boxCollider != null)
            boxCollider.enabled = false;

        if (spriteRenderer != null)
            iTween.FadeTo(this.gameObject, 0f, 0.8f);

    }


    private void HideOff()
    {
        isHideOn = false;
        AddToList();

        if (boxCollider != null)
            boxCollider.enabled = true;

        if (spriteRenderer != null)
            iTween.FadeTo(this.gameObject, 1f, 0.8f);

    }

    protected override void BossDie()
    {
        base.BossDie();
        canMove = false;
        WeaponHideOnOff(true);
    }

    private new void Awake()
    {
        base.Awake();
        SetHp(100);
        RegistPatternToQueue();
        SetWeapon();
        moveSpeed = 2f;
        WeaponHideOnOff(true);

    }

    private void Start()
    {
   
        LinkPlayer();
    }

    private void Update()
    {
        if (isPatternStart == false) return;
        if (isHideOn == true)
        {
            if (rb != null)
                rb.velocity = Vector3.zero;
            return;
        }
        RotateWeapon();
        MoveToTarget();
    }

    private void SetWeapon()
    {
        weaponDic1.Add(WeaponType.LastBoss_Pistol, new LastBoss_Pistol());
        weaponDic1.Add(WeaponType.LastBoss_MinuGun, new LastBoss_MinuGun());
        weaponDic1.Add(WeaponType.LastBoss_Bazooka, new LastBoss_Bazooka());

        weaponDic2.Add(WeaponType.LastBoss_Pistol, new LastBoss_Pistol());
        weaponDic2.Add(WeaponType.LastBoss_MinuGun, new LastBoss_MinuGun());
        weaponDic2.Add(WeaponType.LastBoss_Bazooka, new LastBoss_Bazooka());


    }

    private void ChangeRandomWeapon()
    {
        if (weaponDic1 == null) return;

        List<WeaponType> keyList1 = new List<WeaponType>(weaponDic1.Keys);
        List<WeaponType> keyList2 = new List<WeaponType>(weaponDic2.Keys);

        if (keyList1 == null) return;
        if (keyList2 == null) return;

        Weapon randWeapon1 = weaponDic1[keyList1[Random.Range(0, keyList1.Count)]];
        Weapon randWeapon2 = weaponDic2[keyList2[Random.Range(0, keyList2.Count)]];

        weaponHandler1.ChangeWeapon(randWeapon1);
        weaponHandler2.ChangeWeapon(randWeapon2);
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
        if (weaponHandler1 != null)
            weaponHandler1.transform.rotation = Quaternion.Euler(0f, 0f, weaponAngle);

        if (weaponHandler2 != null)
            weaponHandler2.transform.rotation = Quaternion.Euler(0f, 0f, weaponAngle);

        //flip
        if ((weaponAngle >= 0f && weaponAngle <= 90) ||
              weaponAngle >= 270f && weaponAngle <= 360)
        {
            if (weaponHandler1 != null)
            {
                weaponHandler1.FlipWeapon(false);
                FlipCharacter(false);
            }

            if (weaponHandler2 != null)
            {
                weaponHandler2.FlipWeapon(false);
                FlipCharacter(false);
            }


        }
        else
        {
            if (weaponHandler1 != null)
            {
                weaponHandler1.FlipWeapon(true);
                FlipCharacter(true);

            }

            if (weaponHandler2 != null)
            {
                weaponHandler2.FlipWeapon(true);
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
        if (weaponHandler1 != null)
        {
            Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
            weaponHandler1.FireBullet(weaponHandler1.transform.position, fireDir);
        }

        if (weaponHandler2 != null)
        {
            Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
            weaponHandler2.FireBullet(weaponHandler2.transform.position, fireDir);
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
         //bossEventQueue.AddEvent("SpawnNinjaRoutine");
         bossEventQueue.AddEvent("HideRoutine");
        bossEventQueue.AddEvent("BulletRainRoutine");
    }

    IEnumerator TempFireRoutine1()
    {
        WeaponHideOnOff(false);
        ChangeRandomWeapon();

        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 30; i++)
        {
            FireNowWeapon();
            yield return new WaitForSeconds(0.1f);
        }

        WeaponHideOnOff(true);
        yield return new WaitForSeconds(1.0f);
    }


    IEnumerator SpreadBomb()
    {
        canMove = false;
        Action(Actions.BombAttackStart);
        yield return new WaitForSeconds(1.5f);
        Action(Actions.BombAttackStart);
        yield return new WaitForSeconds(1.5f);
        Action(Actions.BombAttackStart);
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }

    IEnumerator SpawnNinjaRoutine()
    {

        MonsterSpawnEffect spawnEffect = ObjectManager.Instance.monsterSpawnEffectPool.GetItem();
        spawnEffect.Initialize(GamePlayerManager.Instance.player.transform.position, SpawnNinja);

        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        HideOn();
        FirePositsFireOnOff(true);
        yield return new WaitForSeconds(5.0f);
        HideOff();
        FirePositsFireOnOff(false);
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator BulletRainRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        HideOn();

        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                FireBulletRain();
            }
            yield return new WaitForSeconds(0.2f);

        }

        HideOff();
        yield return new WaitForSeconds(2.0f);
    }

    private void FireBulletRain()
    {
        if (leftTop == null || rightTop == null) return;
        float y = leftTop.transform.position.y;
        float x = Random.Range(leftTop.transform.position.x, rightTop.transform.position.x);
        Vector3 firePos = new Vector3(x, y);


        for (int i = 0; i < 3; i++)
        {
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.gameObject.SetActive(true);
                bullet.Initialize(firePos, Vector3.down, 12f - (float)i * 0.6f, BulletType.EnemyBullet);
                bullet.InitializeImage("white", false);
                bullet.SetBloom(true, Color.green);
                bullet.SetEffectName("revolver");
                bullet.SetDestroyByCollision(false, false);
            }
        }


    }

    private void FirePositsFireOnOff(bool OnOff)
    {
        if (firePosits == null) return;
        if (firePosits.Count == 0) return;

        for (int i = 0; i < firePosits.Count; i++)
        {
            if (OnOff == true)
                firePosits[i].FireStart();
            else if (OnOff == false)
                firePosits[i].EndFire();
        }

    }

    public void SpawnNinja(Vector3 posit)
    {
        MonsterManager.Instance.SpawnSpecificMonster(MonsterName.Last3, posit);
    }
    #endregion


    public void SpreadDynamiteAndGranade()
    {

        Vector3 fireDirection = Vector3.right;
        for (int i = 0; i < 25; i++)
        {
            float dynamaiteSpeed = Random.Range(1f, 15f);
            fireDirection = Quaternion.Euler(0f, 0f, Random.Range(50f, 60f)) * fireDirection;
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.Initialize(this.transform.position, fireDirection.normalized, dynamaiteSpeed, BulletType.EnemyBullet, 1f, 1, 1.5f);
                bullet.InitializeImage("Dynamite", true);
                bullet.SetBloom(false);
                bullet.SetDestroyByCollision(false);
                bullet.SetMoveLifetime(1f);
                bullet.SetEffectName("Explode_1", 3f);
                bullet.SetExplosion(1.5f);

            }
        }


        Vector3 fireDirection2 = Quaternion.Euler(0f, 0f, 22.5f) * Vector3.right;
        for (int i = 0; i < 25; i++)
        {
            float dynamaiteSpeed = Random.Range(1f, 15f);
            fireDirection2 = Quaternion.Euler(0f, 0f, Random.Range(50f, 60f)) * fireDirection2;
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {
                bullet.Initialize(this.transform.position, fireDirection2.normalized, dynamaiteSpeed, BulletType.EnemyBullet, 1f, 1, 1.5f);
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
        {
            Action(Actions.Walk);
            rb.velocity = moveDir.normalized * moveSpeed;
        }
        else if (canMove == false)
        {
            Action(Actions.Idle);
            rb.velocity = Vector3.zero;
        }

    }

}

