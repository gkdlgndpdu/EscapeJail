using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum MonsterName
{
    Mouse1,
    Mouse2,
    Mouse3,
    Mouse4,
    Criminal1,
    Criminal2,
    Criminal3,
    Criminal4,
    Criminal5,
    Guard1,
    Guard2,
    Guard3,
    Guard4,
    Scientist1,
    Scientist2,
    Scientist3,
    Scientist4,
    Slime,
    Last1,
    Last2,
    Last3,
    Last4,
    Last5,
    Last6,
    EndMonster
}

public enum MonsterState
{
    Idle,
    Walk,
    Attack,
    Dead
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class MonsterBase : CharacterInfo
{
    public bool isInitialize = false;

    //대상 타겟
    protected Transform target;

    //컴포넌트   
    protected Vector3 moveDir;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected CapsuleCollider2D capsuleCollider;

    //속성값 (속도 etc...)
    protected MonsterName monsterName;
    protected int attackPower = 1;
    protected float moveSpeed = 1f;
    protected float nearestAcessDistance = 1f;
    protected bool nowAttack = false;
    protected float attackDelay = 0f;
  
    protected bool isMoveRandom = false;
    protected MonsterDB monsterDB;
    //사정거리 확인용
    protected float activeDistance = 10;

    //Hud
    protected EnemyHpBar enemyHpBar;
    //무기
    protected WeaponHandler nowWeapon;
    [SerializeField]
    protected Transform weaponPosit;

    [SerializeField]
    protected AttackObject attackObject;

    protected MapModule parentModule;

    //앞에 벽이 있다
    protected bool hasWall = false;

    //소매치기 패시브용
    protected bool hasBullet = false;

    public void SetMapModule(MapModule mapModule)
    {
        parentModule = mapModule;
    }

    /// <summary>
    /// 풀에서 나올때의 생성자
    /// </summary>
    public virtual void ResetMonster()
    {

        ResetCondition();
        hp = hpMax;
        nowAttack = false;
        isDead = false;

        ColliderOnOff(true);

        isMoveRandom = false;

        isStun = false;

        AddToList();


        if (NowSelectPassive.Instance.HasPassive(PassiveType.Scouter) == true)
            HudOnOff(true);
#if UNITY_EDITOR
        HudOnOff(true);
#endif

        UpdateHud();

        AttackOff();

        StartMyCoroutine();
    }


    protected bool canMove()
    {
        //죽었거나             랜덤이동   스턴    //밀림
        if (isDead == true || isMoveRandom == true || isStun == true || isPushed == true) return false;

        return true;
    }

    protected void HudOnOff(bool OnOff)
    {
        if (enemyHpBar != null)
            enemyHpBar.gameObject.SetActive(OnOff);
    }


    protected void AddToList()
    {
        if (MonsterManager.Instance != null)
            MonsterManager.Instance.AddToList(this.gameObject);
    }

    protected void RemoveInList()
    {
        if (MonsterManager.Instance != null)
            MonsterManager.Instance.DeleteInList(this.gameObject);
    }

    protected void OnEnable()
    {
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(false);
    }

    protected void Awake()
    {



    }

    private void SetLayerAndTag()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.gameObject.tag = "Enemy";
    }

    protected void SetUpComponent()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyHpBar = GetComponentInChildren<EnemyHpBar>();
    }

    protected void SetUpCustomScript()
    {
        bool findObj = false;
        foreach (Transform child in this.transform)
        {
            foreach (Transform grandChild in child)
            {
                nowWeapon = grandChild.gameObject.GetComponent<WeaponHandler>();
                if (nowWeapon != null)
                {
                    findObj = true;
                    break;
                }
            }
            if (findObj == true) break;
        }



    }
    // Use this for initialization
    protected void Start()
    {
        if (target == null)
            target = GamePlayerManager.Instance.player.transform;

        SetWeapon();



    }

    protected virtual void SetWeapon()
    {

    }

    /// <summary>
    /// 몬스터 초기설정 (db읽기, 속성,체력 세팅등등)
    /// </summary>
    public void FirstInitializeMonster()
    {
        SetUpComponent();
        SetUpCustomScript();
        SetLayerAndTag();
        SetUpMonsterAttribute();
        ReadDBData();
        SetHp();
        if (NowSelectPassive.Instance.HasPassive(PassiveType.Scouter) == true)
            HudOnOff(true);
        else
            HudOnOff(false);

        isInitialize = true;
    }

    private void ReadDBData()
    {
        this.monsterDB = DatabaseLoader.Instance.GetMonsterData(this.monsterName);
    }

    protected void SetAttackPower(int power)
    {
        if (attackObject != null)
            attackObject.Initialize(power);
    }

    protected virtual void SetUpMonsterAttribute()
    {

    }

    protected void SetHp()
    {
        if (monsterDB != null)
        {
            base.SetHp(monsterDB.Hp);
        }
#if UNITY_EDITOR
        if (monsterDB == null)
            Debug.Log(monsterName.ToString() + " DB 읽기 실패");
#endif



        UpdateHud();
    }



    protected virtual IEnumerator FireRoutine()
    {
        yield return null;
    }

    protected virtual void SetDie()
    {
        //상태
        isDead = true;

        //속도
        if (rb != null)
            rb.velocity = Vector3.zero;

        //충돌체       
        ColliderOnOff(false);

        //애니메이션
        if (animator != null)
            animator.SetTrigger("DeadTrigger");

        //다시 풀로 돌아가기
        Invoke("ObjectOff", 3f);

        //근접공격대상에서 벗어나게
        RemoveInList();

        //실행중인 모든 코루틴 종료
        StopAllCoroutines();

        //스코어 올려줌
        GamePlayerManager.Instance.scoreCounter.KillMonster();
  

        //무기꺼중
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(false);


        //이펙트 청소
        foreach (KeyValuePair<CharacterCondition, CharacterStateEffect> effect in effectDic)
        {
            effectDic[effect.Key].EffectOff();
        }
        effectDic.Clear();

        //hud꺼줌
        HudOnOff(false);

        //코인생성

        DropGoods coin = ObjectManager.Instance.coinPool.GetItem();
        coin.Initiatlize(this.transform.position, 10,GoodsType.Coin);

        //사운드
        //SoundManager.Instance.PlaySoundEffect("monsterDown");
         SoundManager.Instance.PlaySoundEffect("monsterHit");

        isPushed = false;
        isStun = false;

        //소메치기패시브

        if (NowSelectPassive.Instance.HasPassive(PassiveType.PickPocket) == true)
        {
            if (hasBullet == true)
            {
                //확률
                if (MyUtils.GetPercentResult(10) == true)
                {
                    GamePlayerManager.Instance.player.GetBulletItem(10);
                    MessageBar.Instance.ShowInfoBar("Get 10 Bullets", Color.white);
                }
            }
        }
    }

    protected void ObjectOff()
    {
        //임시코드
        this.gameObject.SetActive(false);
    }


    /// <summary>
    /// 원거리 몬스터의 경우에 무기를 켜줄때 사용
    /// </summary>
    public void AttackOn()
    {
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(true);


    }

    public void AttackOff()
    {
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(false);
    }






    protected float GetDistanceToPlayer()
    {
        return Vector3.Distance(this.transform.position, GamePlayerManager.Instance.player.transform.position);
    }



    protected bool IsInAcessArea()
    {
        return GetDistanceToPlayer() <= nearestAcessDistance;
    }

    //플레이어쪽으로 이동
    protected void MoveToTarget()
    {
        if (rb == null) return;

        rb.velocity = Vector3.zero;

        if (target == null) return;
        if (nowAttack == true) return;

        if (IsInAcessArea() == true)
        {
            //flipx를 위해서 방향계산만 해줌
            CalculateMoveDIr();
            SetAnimation(MonsterState.Idle);
            return;
        }

        CalculateMoveDIr();
        rb.velocity = moveDir.normalized * moveSpeed;

        SetAnimation(MonsterState.Walk);


    }

    //플레이어 반대쪽으로 이동
    protected void MoveAgainstTarget()
    {
        if (rb == null) return;

        rb.velocity = Vector3.zero;

        if (target == null) return;
        if (nowAttack == true) return;

        if (IsInAcessArea() == false)
        {
            //flipx를 위해서 방향계산만 해줌
            CalculateMoveDIr();
            SetAnimation(MonsterState.Idle);
            return;
        }

        CalculateMoveDIr();
        rb.velocity = Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)) * -moveDir.normalized * moveSpeed;

        SetAnimation(MonsterState.Walk);
    }

    protected void CalculateMoveDIr()
    {
        if (hasWall == false)
            moveDir = target.position - this.transform.position;
    }

    protected void UpdateHud()
    {
        if (enemyHpBar != null)
            enemyHpBar.SetHpBar((float)hp, (float)hpMax);
    }

    /// <summary>
    /// NearAttackLogic에서 실행
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator AttackRoutine()
    {
        yield break;
    }

    protected void NearAttackLogic()
    {
        if (IsInAcessArea() == true && nowAttack == false)
        {
            StartCoroutine(AttackRoutine());
        }
    }



    protected void SetAnimation(MonsterState state)
    {
        if (animator == null) return;

        FlipCharacterByMoveDir();

        switch (state)
        {
            case MonsterState.Idle:
                {
                    animator.SetFloat("Speed", 0f);
                }
                break;
            case MonsterState.Walk:
                {
                    animator.SetFloat("Speed", 1f);
                }
                break;
            case MonsterState.Attack:
                {
                    animator.SetTrigger("AttackTrigger");
                }
                break;
            case MonsterState.Dead:
                {
                    animator.SetTrigger("DeadTrigger");
                }
                break;
        }

    }

    protected void FlipCharacterByMoveDir()
    {
        //임시
        if (spriteRenderer == null) return;
        if (target == null) return;

        if (this.transform.position.x < target.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }


        //if (moveDir.x > 0)
        //{
        //    spriteRenderer.flipX = false;
        //}
        //else
        //{
        //    spriteRenderer.flipX = true;
        //}
    }

    protected IEnumerator RandomMoveRoutine(Vector3 direction, int moveDistance)
    {
        isMoveRandom = true;
        Vector3 moveDirection = direction.normalized;
        moveDirection = -moveDirection;
        moveDirection = Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)) * moveDirection;

        for (int i = 0; i < moveDistance; i++)
        {
            if (rb != null)
                rb.velocity = moveDirection;

            yield return new WaitForSeconds(0.1f);
        }

        isMoveRandom = false;


    }
    protected IEnumerator RandomMovePattern(float moveTime = 2f, float randomMoveDelay = 2f)
    {
        while (true)
        {
            if (isMoveRandom == false)
            {
                nearestAcessDistance = UnityEngine.Random.Range(1f, 5f);
                if (IsInAcessArea() == true)
                {
                    //백무빙
                    isMoveRandom = true;
                    StartCoroutine(RandomBackMove(moveTime));

                }
            }
            yield return new WaitForSeconds(randomMoveDelay);
        }
    }

    protected IEnumerator RandomBackMove(float moveTime = 2f)
    {
        float count = 0f;
        Vector3 randomDirection = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-90f, 90f)) * -(moveDir.normalized);

        while (true)
        {
            if (rb != null)
                rb.velocity = randomDirection * moveSpeed;

            SetAnimation(MonsterState.Walk);

            count += Time.deltaTime;

            if (count > moveTime)
            {
                isMoveRandom = false;
                SetAnimation(MonsterState.Idle);
                yield break;
            }
            else
                yield return null;
        }
    }

    protected virtual void FireWeapon()
    {
        if (nowWeapon != null)
        {
            Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
            nowWeapon.FireBullet(this.transform.position, fireDir.normalized);
        }
    }

    public override void GetDamage(int damage)
    {

        VampiricGunEffect();

        if (NowSelectPassive.Instance.HasPassive(PassiveType.CrossCounter) == true)
        {
            damage *= 2;
        }

      

        this.hp -= damage;
        SoundManager.Instance.PlaySoundEffect("monsterdamage");
        UpdateHud();
        if (hp <= 0)
        {
            SetDie();
        }

    }


    protected void RotateWeapon()
    {
        float angle = MyUtils.GetAngle(this.transform.position, target.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);

        //flip
        if ((angle >= 0f && angle <= 90) ||
              angle >= 270f && angle <= 360)
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(false);
        }
        else
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(true);
        }

    }

    protected void NearAttackRotate()
    {

        if (weaponPosit.gameObject.activeSelf == false) return;
        float angle = MyUtils.GetAngle(target.position, this.transform.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);


    }

    protected void ColliderOnOff(bool OnOff)
    {
        if (capsuleCollider != null)
            capsuleCollider.enabled = OnOff;

    }

    protected IEnumerator PathFindRoutine()
    {
        int layerMask = MyUtils.GetLayerMaskByString("ItemTable");
        float rayDistance = 1.5f;
        //찾은길로 이동하는 시간
        float findMoveTime = 0.6f;

        while (true)
        {
            Vector3 rayDir = moveDir.normalized;
            RaycastHit2D raycastHit = Physics2D.Raycast(this.transform.position, rayDir, rayDistance, layerMask);
            //벽이없음 -> 갈길간다
            if (raycastHit.collider == null)
            {
                //  Debug.Log("벽이없음");
                hasWall = false;
            }
            //벽이 탐지됨 ->길을 찾는다
            else
            {
                //    Debug.Log("벽이있어");
                hasWall = true;

                for (int i = 1; i < 5; i++)
                {
                    Vector3 nextRayDir1 = Quaternion.Euler(0f, 0f, i * 40f) * rayDir;
                    Vector3 nextRayDir2 = Quaternion.Euler(0f, 0f, i * -40f) * rayDir;
                    RaycastHit2D raycastHit1 = Physics2D.Raycast(this.transform.position, nextRayDir1, rayDistance, layerMask);
                    RaycastHit2D raycastHit2 = Physics2D.Raycast(this.transform.position, nextRayDir2, rayDistance, layerMask);

                    bool findPath = false;
                    float pointdistance = 99f;

                    if (raycastHit1.collider == null)
                    {
                        findPath = true;
                        pointdistance = raycastHit1.distance;
                        moveDir = nextRayDir1.normalized;
                    }
                    if (raycastHit2.collider == null)
                    {
                        findPath = true;

                        //두번째 경로가 더 짧으면
                        if (raycastHit2.distance < pointdistance)
                        {
                            moveDir = nextRayDir2;

                        }
                    }

                    if (findPath == true)
                    {
                        if (rb != null)
                            rb.velocity = moveDir.normalized * moveSpeed;

                        yield return new WaitForSeconds(findMoveTime);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

    }

    public override void SetStun(bool OnOff)
    {
        if (OnOff == true)
        {
            isStun = true;
            StopAllMyCoroutine();
            StartCoroutine(StunRoutine());
            if (rb != null)
                rb.velocity = Vector3.zero;
        }
        else if (OnOff == false)
        {
            if (isStun == true&&isDead==false)
                StartMyCoroutine();

            isStun = false;
            nowAttack = false;
        }
    }

    public override void SetPush(Vector3 pushPoint, float pushPower, int damage)
    {
        StopAllMyCoroutine();
        Vector3 pushDir = this.transform.position - pushPoint;
        pushDir.Normalize();
        if (rb != null)
            rb.AddForce(pushDir * pushPower, ForceMode2D.Impulse);

        GetDamage(damage);

        isPushed = true;
        StartCoroutine(PushRoutine());
    }



    private IEnumerator PushRoutine()
    {
        yield return new WaitForSeconds(GameConstants.PushTime);
        nowAttack = false;
        isPushed = false;
        if (rb != null)
            rb.velocity = Vector3.zero;

        if(isDead==false)
        StartMyCoroutine();


    }

    private IEnumerator StunRoutine()
    {
        yield return new WaitForSeconds(GameConstants.FlashBangStunTime);
        SetStun(false);
        nowAttack = false;
    }

    protected virtual void StartMyCoroutine()
    {

    }

    protected void StopAllMyCoroutine()
    {
        StopAllCoroutines();
    }



}




