using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using weapon;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : CharacterInfo
{
    //컴포넌트    
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected CapsuleCollider bodyCollider;

    //값변수
    protected float moveSpeed = 3f;
    protected float originSpeed = 0f;
    protected float burstSpeed = 5f;
    //단위 초 
    protected float immuneTime = 1f;

    //상태
    protected bool isImmune = false;
    protected bool isBurstMoveOn = true;
    //이동
    protected Vector3 moveDir = Vector3.zero;
    protected Vector3 lastMoveDir = Vector3.zero;

    //무기 회전 관련
    protected float weaponAngle = 0f;

    private bool hasBag = false;

    private Vector3 lastFireDirection = Vector3.zero;

    //무기
    [SerializeField]
    protected WeaponHandler weaponHandler;
    [SerializeField]
    protected SlashObject slashObject;


    //무기 장착 위치
    [SerializeField]
    protected Transform weaponPosit;

    [SerializeField]
    protected Transform FirePos;

    //UI
    [HideInInspector]
    protected PlayerUI playerUi;

    private CharacterSliders characterSliders;
    //인벤토리
    protected Inventory inventory;

    //아머
    protected ArmorSystem armorSystem;

    //진통제

    protected int stimulantRecoverTime = 30;
    protected bool nowUseStimulant = false;

    //버프효과
    protected BuffEffect buffEffect;

    //재화
    protected int coin = 0;
    public int Coin
    {
        get
        {
            return coin;
        }
    }
    protected int medal = 0;


    private LineRenderer redDotLine;
    private CardCaseCard cardCaseCard;
    public CardCaseCard NowCard
    {
        get
        {
            return cardCaseCard;
        }
    }
    public void GetCoin(int coin)
    {
        this.coin += coin;

        if (playerUi != null)
            playerUi.goodsUi.SetCoin(this.coin);
    }
    public void GetMedal(int medal)
    {
        this.medal += medal;
        if (playerUi != null)
            playerUi.goodsUi.SetMedal(this.medal);
    }
    private void UpdateMedal()
    {
        int prefMedal = PlayerPrefs.GetInt(GoodsType.Medal.ToString(), 0);
        prefMedal += medal;
        PlayerPrefs.SetInt(GoodsType.Medal.ToString(), prefMedal);
    }
    public void UseCoin(int coin)
    {


        this.coin -= coin;
        if (playerUi != null)
            playerUi.goodsUi.SetCoin(this.coin);
    }

    protected void LoadPrefabs()
    {
        GameObject loadObj = Resources.Load<GameObject>("Prefabs/ETC/Sliders");
        if (loadObj != null)
        {
            GameObject makeObj = Instantiate(loadObj, this.transform);
            if (makeObj != null)
            {
                characterSliders = makeObj.GetComponent<CharacterSliders>();
            }
        }
    }
    //패시브와 관련된 설정
    private void PassiveSetting()
    {
        //망토
        if (NowSelectPassive.Instance.HasPassive(PassiveType.HolyCape) == true)
            immuneTime = 2f;

        //인삼
        if (NowSelectPassive.Instance.HasPassive(PassiveType.Ginseng) == true)
            isImmuneAnyState = true;


        if (NowSelectPassive.Instance.NowDifficulty == Difficulty.easy)
        {
            GameOption.ChangeFireStype(FireStyle.Auto);
            playerUi.ChangeFireStyle(FireStyle.Auto);
        }
        else if (NowSelectPassive.Instance.NowDifficulty == Difficulty.hard)
        {
            GameOption.ChangeFireStype(FireStyle.Manual);
            playerUi.ChangeFireStyle(FireStyle.Manual);
        }

        //        //오토에임
        //if (MyUtils.GetNowPassive() == PassiveType.AutoAim)
        //{
        //    GameOption.ChangeFireStype(FireStyle.Auto);
        //    playerUi.ChangeFireStyle(FireStyle.Auto);
        //}
        //else
        //{
        //    GameOption.ChangeFireStype(FireStyle.Manual);
        //    playerUi.ChangeFireStyle(FireStyle.Manual);
        //}



        //신발
        if (NowSelectPassive.Instance.HasPassive(PassiveType.WingShoes) == true)
        {
            moveSpeed = 5f;
            burstSpeed = moveSpeed + 2;
            originSpeed = moveSpeed;

        }
       

        ////조준기
        //if (MyUtils.GetNowPassive() == PassiveType.RedDotSight)
        //{
        //    redDotLine = gameObject.AddComponent<LineRenderer>();
        //    if (redDotLine != null)
        //    {
        //        redDotLine.material = Resources.Load<Material>("Materials/SpriteMaterial");
        //        redDotLine.SetWidth(0.05f, 0.05f);
        //        redDotLine.sortingOrder = 30;
        //        redDotLine.receiveShadows = false;
        //        redDotLine.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //        redDotLine.SetColors(Color.red, Color.red);
        //        redDotLine.positionCount = 2;

        //        StartCoroutine(RedDotLineRenderRoutine());

        //    }
        //}
    }

    private IEnumerator RedDotLineRenderRoutine()
    {
        while (true)
        {
            int layerMask = (1 << LayerMask.NameToLayer("Enemy")) |
                                (1 << LayerMask.NameToLayer("Tile")) |
                                (1 << LayerMask.NameToLayer("ItemTable"));



            if (lastFireDirection != Vector3.zero)
            {
                RaycastHit2D castHit = Physics2D.Raycast(this.transform.position, lastFireDirection, 50f, layerMask);

                redDotLine.SetPosition(0, this.transform.position);
                redDotLine.SetPosition(1, castHit.point);
            }
            else if (lastFireDirection == Vector3.zero)
            {
                RaycastHit2D castHit = Physics2D.Raycast(this.transform.position, lastMoveDir, 50f, layerMask);

                redDotLine.SetPosition(0, this.transform.position);
                redDotLine.SetPosition(1, castHit.point);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    protected void Awake()
    {

        SetLayerAndTag();
        LoadPrefabs();
        Initialize();

        SetBuffEffect();

        if (weaponHandler != null && slashObject != null)
        {
            weaponHandler.SetSlashObject(slashObject);
            slashObject.gameObject.SetActive(false);
        }

        if (weaponHandler != null && characterSliders != null)
        {
            weaponHandler.SetSlider(characterSliders.weaponDelaySlider, characterSliders.weaponReboundSlider);
        }

        if (weaponHandler != null && playerUi != null)
        {
            weaponHandler.SetWeaponUi(playerUi.weaponUi);
        }

        if (weaponHandler != null)
            weaponHandler.SetAudioSource();


        PassiveSetting();
        SetCardCaseCard();
        if (NowSelectPassive.Instance.NowDifficulty == Difficulty.easy)
        {
            SetHp(10, true);
        }
        else if (NowSelectPassive.Instance.NowDifficulty == Difficulty.hard)
        {
            SetHp(5, true);
        }
    }

    public bool CanReload()
    {
        if (weaponHandler == null)
            return false;

        return weaponHandler.CanReload();
    }

    protected void SetBuffEffect()
    {
        GameObject loadObj = Resources.Load<GameObject>("Prefabs/Objects/BuffEffect");
        if (loadObj != null)
        {
            GameObject makingObject = Instantiate(loadObj, this.transform);
            if (makingObject != null)
            {
                BuffEffect effect = makingObject.GetComponent<BuffEffect>();
                if (effect != null)
                {
                    this.buffEffect = effect;
                    BuffEffectOnOff(false);
                }
            }

        }
    }
    //CardCaseCard
    protected void SetCardCaseCard()
    {
        GameObject loadObj = Resources.Load<GameObject>("Prefabs/Objects/CardCaseCard");
        if (loadObj != null)
        {
            GameObject makingObject = Instantiate(loadObj, this.transform);
            if (makingObject != null)
            {
                CardCaseCard card = makingObject.GetComponent<CardCaseCard>();
                if (card != null)
                {
                    cardCaseCard = card;
                    cardCaseCard.gameObject.SetActive(false);
                }
            }

        }
    }

    protected void BuffEffectOnOff(bool OnOff)
    {
        if (buffEffect != null)
            buffEffect.gameObject.SetActive(OnOff);
    }
    protected void SetBuffEffectColor(Color color)
    {
        if (buffEffect != null)
            buffEffect.SetBuffColor(color);
    }

    protected void SetWeapon()
    {
        AddWeapon(new BasicSniper());
        AddWeapon(new Revolver());
        AddWeapon(new H249());
    }

    public WeaponType GetNowEquipWeapon()
    {
        if (weaponHandler == null) return WeaponType.PlayerWeaponStart;
        if (weaponHandler.NowWeapon == null) return WeaponType.PlayerWeaponStart;
        return weaponHandler.NowWeapon.weapontype;
    }

    public virtual void SetBurstSpeed(bool OnOff)
    {
        isBurstMoveOn = OnOff;

        if (OnOff == true)
        {
            moveSpeed = burstSpeed;
        }
        else
        {
            moveSpeed = originSpeed;
        }
    }

    private void SetLayerAndTag()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        this.gameObject.tag = "Player";
    }

    private void GetUiInfo()
    {
        GameObject playerUiobj = GameObject.Find("PlayerUi");
        if (playerUiobj != null)
        {
            playerUi = playerUiobj.GetComponent<PlayerUI>();
        }

    }
    protected void Initialize()
    {
        //컴포넌트
        SetupComponent();
        GetUiInfo();
        //스크립트
        if (playerUi != null)
            inventory = new Inventory(playerUi.inventoryUi);

        if (inventory != null)
            inventory.SetInventorySize(0, 0);

        if (armorSystem == null)
        {
            armorSystem = new ArmorSystem(playerUi.inventoryUi, playerUi, playerUi.hpUi.SetArmor);
            //임시
            SetArmor(0);
        }
    }



    public void GetBag(int level, int value)
    {
        if (inventory != null)
        {
            inventory.SetInventorySize(level, value);
            hasBag = true;
        }
    }

    public bool HasBag()
    {
        return hasBag;
    }


    public void SetArmor(int value)
    {
        if (armorSystem != null)
            armorSystem.SetArmor(value);
    }

    protected void SetupComponent()
    {
        //컴포넌트
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider>();
    }

    // Use this for initialization
    protected void Start()
    {
      //  SetBurstSpeed(true);
        StartCoroutine(FindItemRoutine());
        UIUpdate();
        originSpeed = moveSpeed;
    }

    protected IEnumerator FindItemRoutine()
    {
        while (true)
        {
            ReactiveItem();
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            GetCoin(100);
        }
#endif

        HandleNowWeapon();

#if UNITY_ANDROID
        MoveInMobile();
#endif
#if UNITY_EDITOR
        InputOnPc();
#endif

    }
    protected void InputOnPc()
    {
        if (isDead == true) return;
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GetDamage(1);
        }
        MoveInPc();
    }

    public virtual void FireWeapon()
    {
        if (isDead == true) return;

        if (weaponHandler.NowWeapon != null)
        {
            if (weaponHandler.NowWeapon.hasAmmo() == false)
            {
                UseSpecificItem(ItemType.Bullet);

            }
        }

        if (GameOption.FireStyle == FireStyle.Auto)
        {
            GameObject nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);

            if (nearEnemy != null && weaponHandler != null)
            {
                Vector3 fireDir = nearEnemy.transform.position - this.transform.position;
                weaponHandler.FireBullet(this.FirePos.position, fireDir);
            }
            else if (nearEnemy == null)
            {
                if (lastMoveDir != Vector3.zero)
                    weaponHandler.FireBullet(this.FirePos.position, lastMoveDir);
                else
                    weaponHandler.FireBullet(this.FirePos.position, Vector3.left);
            }
        }
        else if (GameOption.FireStyle == FireStyle.Manual)
        {
            if (weaponHandler != null)
            {
                Vector3 fd = FireJoyStick.Instance.FireDir;
                if (fd != Vector3.zero)
                {
                    weaponHandler.FireBullet(this.FirePos.position, fd);
                    lastFireDirection = fd;
                }
                else
                    weaponHandler.FireBullet(this.FirePos.position, lastFireDirection);

            }
        }
    }


    private void RotateWeapon()
    {
        if (GameOption.FireStyle == FireStyle.Auto)
        {
            GameObject nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
            if (nearEnemy != null && weaponHandler.attackType == AttackType.gun)
                RotateWeapon(nearEnemy.transform.position);
            else if (nearEnemy == null || weaponHandler.attackType == AttackType.near)
            {
                RotateWeapon(this.transform.position + lastMoveDir);
            }
        }
        else if (GameOption.FireStyle == FireStyle.Manual)
        {
            if (lastFireDirection != Vector3.zero)
                RotateWeapon(this.transform.position + lastFireDirection);
        }
    }

    protected void HandleNowWeapon()
    {
        GameObject nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);


#if UNITY_EDITOR
        //발사
        if (Input.GetKey(KeyCode.Mouse1))
        {
            FireWeapon();

        }

#endif


        RotateWeapon();

    }

    public void ChangeWeapon()
    {
        if (weaponHandler == null) return;

        weaponHandler.ChangeWeapon(inventory.GetWeapon());

    }

    public void CardCaseCardOnOff(bool OnOff)
    {
        if (cardCaseCard == null) return;
        cardCaseCard.gameObject.SetActive(OnOff);
    }

    public void ChangeSpeceficWeapon(WeaponType weaponType)
    {
        weaponHandler.ChangeWeapon(inventory.GetSpeceficWeapon(weaponType));
    }

    protected void RotateWeapon(Vector3 enemyPos)
    {
        Vector3 nearestEnemyPos = enemyPos;
        weaponAngle = MyUtils.GetAngle(nearestEnemyPos, this.transform.position);

        if (weaponHandler.CanRotateWeapon() == true)
        {


            if (weaponPosit != null)
            {
                weaponPosit.rotation = Quaternion.Euler(0f, 0f, weaponAngle);
            }
        }
        else
        {
            if (weaponPosit != null)
            {
                weaponPosit.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }



        //flip
        if ((weaponAngle >= 0f && weaponAngle <= 90) ||
              weaponAngle >= 270f && weaponAngle <= 360)
        {
            if (weaponHandler != null)
            {
                if (weaponHandler.CanRotateWeapon() == true)
                    weaponHandler.FlipWeapon(false);

                FlipCharacter(true);
            }
        }
        else
        {
            if (weaponHandler != null)
            {
                if (weaponHandler.CanRotateWeapon() == true)
                    weaponHandler.FlipWeapon(true);

                FlipCharacter(false);
            }
        }

    }

    protected virtual void MoveInPc()
    {
        if (rb != null)
            rb.velocity = Vector2.zero;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = Vector3.right * h + Vector3.up * v;
        moveDir.Normalize();

        if (moveDir != Vector3.zero)
            lastMoveDir = moveDir;

        //이동
        if (rb != null)
            rb.velocity = moveDir * moveSpeed;

        //애니메이션
        AnimControl(moveDir);

    }

    protected virtual void MoveInMobile()
    {
        if (isDead == true) return;

        if (rb != null)
            rb.velocity = Vector2.zero;

        moveDir = JoyStick.Instance.MoveDir;

        if (moveDir != Vector3.zero)
            lastMoveDir = moveDir;
        //이동
        if (rb != null)
            rb.velocity = moveDir * moveSpeed;
        //애니메이션
        AnimControl(moveDir);
    }





    protected void AnimControl(Vector3 MoveDir)
    {
        ChangeAnimation(MoveDir);
    }

    protected void ChangeAnimation(Vector3 MoveDir)
    {
        if (animator == null) return;

        float SpeedValue = Mathf.Abs(MoveDir.x) + Mathf.Abs(MoveDir.y);
        animator.SetFloat("Speed", SpeedValue);


    }

    protected void FlipCharacter(bool flip)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = flip;

        if (slashObject != null)
            slashObject.FlipOnOff(flip);

    }

    private void StartImmuine()
    {
        if (isImmune == true) return;
        StartCoroutine(ImmuneRoutine());
        //빤짝빤짝
    }

    IEnumerator ImmuneRoutine()
    {
        if (spriteRenderer == null) yield break;

        isImmune = true;
        GameObject target = spriteRenderer.gameObject;
        iTween.ColorTo(target, iTween.Hash("loopType", "pingPong", "Time", 0.1f, "Color", Color.red));

        yield return new WaitForSeconds(immuneTime);

        iTween.Stop(this.gameObject);
        iTween.ColorTo(target, Color.white, 0.1f);
        isImmune = false;
    }

    public override void GetDamage(int damage)
    {

        if (isImmune == true) return;
        if (isDead == true) return;
        if (NowSelectPassive.Instance.HasPassive(PassiveType.CrossCounter) == true)
        {
            damage *= 2;
        }

        //흔들리는 효과
        CameraController.Instance.ShakeCamera(3f, 0.4f);

        //진동? 
        Handheld.Vibrate();

        //갑옷 적용
        if (armorSystem.hasArmor() == true)
        {
            armorSystem.UseArmor(damage);
            StartImmuine();
            return;
        }
        else
        {
            hp -= damage;
            SoundManager.Instance.PlaySoundEffect("monsterDown");
            GamePlayerManager.Instance.scoreCounter.GetDamage();

            //회복템 자동사용
            // UseSpecificItem(ItemType.Medicine);

            UIUpdate();

            if (hp <= 0 && isDead == false)
            {
                DieAction();
            }
            else
            {
                StartImmuine();
            }
        }




    }

    protected void UIUpdate()
    {
        if (playerUi != null)
            playerUi.hpUi.SetHp(hp);
    }

    //죽은다음에 처리해아할 것들
    protected virtual void DieAction()
    {
        isDead = true;

        GamePlayerManager.Instance.scoreCounter.EarningMedals = medal;
        GamePlayerManager.Instance.scoreCounter.RemainCoin = coin;

        //결과창 띄워줌
        if (playerUi != null)
        {
            playerUi.ResultUiOnOff(true);
            playerUi.resultUi.LinkFunc = () =>
            {
                RevivePlayer();
                playerUi.ResultUiOnOff(false);
            };
        }
        SkillOff();
        UpdateMedal();

    }

    public void GetBulletItem(int value)
    {
        if (weaponHandler != null)
        {
            weaponHandler.GetBulletItem(value);

        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (inventory != null && weapon != null)
        {
            inventory.AddWeapon(weapon);
            weaponHandler.ChangeWeapon(inventory.GetLastWeapon());
        }
    }

    public void RemoveWeapon(ItemBase weapon)
    {
        if (inventory != null && weapon != null)
        {
            inventory.RemoveWeapon(weapon);
            weaponHandler.ChangeWeapon(inventory.GetWeapon());
        }
    }
    public void AddItem(ItemBase item)
    {
        if (inventory != null && item != null)
        {
            inventory.AddToInventory(item);
        }
    }
    public void RemoveItem(ItemBase item)
    {
        if (inventory != null && item != null)
        {
            inventory.RemoveInInventory(item);
            item = null;
        }
    }

    public void UseSpecificItem(ItemType itemType)
    {
        inventory.UseSpecificItem(itemType);
    }

    public bool isInventoryFull()
    {
        if (inventory == null) return true;

        bool returnValue = inventory.isInventoryFull();

        if (returnValue == true)
        {
            MessageBar.Instance.ShowInfoBar("Inventory Is Full", Color.red);
        }

        return returnValue;
    }


    public void ReactiveItem()
    {
        int activeLayer = MyUtils.GetLayerMaskByString("DropItem");
        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, 0.5f, activeLayer);

        if (colls == null)
        {
            ItemInfoBar.Instance.ResetItemBar();
            return;
        }
        if (colls.Length == 0)
        {
            ItemInfoBar.Instance.ResetItemBar();
            return;
        }
        if (colls.Length == 1)
        {
            DropItem dropItem = colls[0].gameObject.GetComponent<DropItem>();
            if (dropItem != null)
            {
                if (dropItem.IsSalesItem == false)
                    ItemInfoBar.Instance.SetItemBar(dropItem.itemBase, dropItem.ClickAction);
                else
                    ItemInfoBar.Instance.SetItemBar(dropItem.itemBase, dropItem.ClickAction, true, dropItem.Price);
                return;
            }
            WeaponBox weaponBox = colls[0].gameObject.GetComponent<WeaponBox>();
            if (weaponBox != null)
            {
                ItemInfoBar.Instance.SetItemBar(null, weaponBox.ClickAction);
                return;
            }


        }
        else if (colls.Length > 1)
        {
            Collider2D neariestCollider = null;
            float neariestDistance = 999f;
            for (int i = 0; i < colls.Length; i++)
            {
                float distance = Vector3.Distance(this.transform.position, colls[i].gameObject.transform.position);
                if (distance < neariestDistance)
                {
                    neariestDistance = distance;
                    neariestCollider = colls[i];
                }
            }

            DropItem dropItem = neariestCollider.gameObject.GetComponent<DropItem>();
            if (dropItem != null)
            {
                if (dropItem.IsSalesItem == false)
                    ItemInfoBar.Instance.SetItemBar(dropItem.itemBase, dropItem.ClickAction);
                else
                    ItemInfoBar.Instance.SetItemBar(dropItem.itemBase, dropItem.ClickAction, true, dropItem.Price);

                return;
            }
            WeaponBox weaponBox = neariestCollider.gameObject.GetComponent<WeaponBox>();
            if (weaponBox != null)
            {
                ItemInfoBar.Instance.SetItemBar(null, weaponBox.ClickAction);
                return;
            }

        }
    }




    public bool CanHeal()
    {
        bool returnValue = hp < hpMax;
        if (returnValue == false)
        {
            MessageBar.Instance.ShowInfoBar("Life is full", Color.red);
        }
        return returnValue;
    }

    public void GetHp(int value)
    {
        Debug.Log("Heal" + value.ToString());

        MessageBar.Instance.ShowInfoBar(string.Format("Heal {0}", value), Color.white);

        hp += value;
        if (hp > hpMax)
        {
            hp = hpMax;
        }

        UIUpdate();
    }

    public bool CanUseStimulant()
    {
        bool returnValue = !nowUseStimulant;
        if (returnValue == false)
        {
            MessageBar.Instance.ShowInfoBar("Already use stimulant.", Color.red);
        }

        //이미 사용중이면 사용 불가
        return returnValue;
    }
    public bool CanFillBullet()
    {
        if (weaponHandler == null) return false;
        return weaponHandler.CanFIllBullet();
    }

    //위에 조건이 충족되면 여기로 들어옴
    public void UseStimulant(int value)
    {
        BuffEffectOnOff(true);
        SetBuffEffectColor(Color.green);

        nowUseStimulant = true;
        StopCoroutine("StimulantRoutine");
        StartCoroutine("StimulantRoutine", value);
    }






    private IEnumerator StimulantRoutine(int value)
    {
        float durationTime = GameConstants.StimulantDurationTime;

        float addValue = (float)value / durationTime;
        float countValue = 0f;
        float elapsedTime = 0f;

        while (true)
        {
            countValue += addValue;
            if (countValue >= 1f)
            {
                GetHp(1);
                countValue -= 1f;
            }

            elapsedTime += 1f;
            if (elapsedTime >= durationTime)
            {
                nowUseStimulant = false;
                BuffEffectOnOff(false);
                Debug.Log("끝 " + countValue.ToString());
                yield break;
            }

            yield return new WaitForSeconds(1.0f);
        }


    }

    private void PushNearEnemies()
    {
        int layerMask = MyUtils.GetLayerMaskByString("Enemy");


        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, 10f, layerMask);
        if (colls == null) return;

        for (int i = 0; i < colls.Length; i++)
        {
            CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
            if (characterInfo != null)
                characterInfo.SetPush(this.transform.position, 10f, 0);
        }
    }

    public void RevivePlayer()
    {
        isDead = false;
        GetHp(hpMax);

        //총알삭제
        ObjectManager.Instance.AllEnemyBulletDestroy();
        //주변 적들을 밀어냄
        PushNearEnemies();

        //모든효과 삭제
        AllStateClear();

        //특수스킬 초기화
        ResetAbility();



        //시간진행

    }

    public virtual void UseCharacterSkill()
    {

    }
    //부활시 능력관련 값 리셋
    protected virtual void ResetAbility()
    {

    }
    public void SetArmorFull()
    {
        if (armorSystem == null) return;
        armorSystem.SetArmorFull();


    }
    protected virtual void SkillOff()
    {

    }
}
