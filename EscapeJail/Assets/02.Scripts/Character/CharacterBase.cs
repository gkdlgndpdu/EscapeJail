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
    protected float burstSpeed = 5f;
    protected float originSpeed = 3f;
    //단위 초 
    protected int immuneTime = 1;

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
        AddWeapon(new Bfe());
  
        UIUpdate();
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
            inventory.SetInventorySize(0);

        if (armorSystem == null)
        {
            armorSystem = new ArmorSystem(playerUi.inventoryUi, playerUi, playerUi.hpBar);
            //임시
            SetArmor(0, 0);
        }
    }



    public void GetBag(int value)
    {
        if (inventory != null)
        {
            inventory.SetInventorySize(value);
            hasBag = true;
        }
    }

    public bool HasBag()
    {
        return hasBag;
    }


    public void SetArmor(int level, int value)
    {
        if (armorSystem != null)
            armorSystem.SetArmor(level, value);
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
        SetBurstSpeed(true);
        StartCoroutine(FindItemRoutine());
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


        HandleNowWeapon();

#if UNITY_ANDROID
        MoveInMobie();
#endif
#if UNITY_EDITOR
        InputOnPc();
#endif

    }
    protected void InputOnPc()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GetDamage(1);
        }
        MoveInPc();
    }

    public virtual void FireWeapon()
    {

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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireWeapon();

        }

#endif


        RotateWeapon();

    }

    public void ChangeWeapon()
    {
        weaponHandler.ChangeWeapon(inventory.GetWeapon());
    }

    public void ChangeSpeceficWeapon(WeaponType weaponType)
    {
        weaponHandler.ChangeWeapon(inventory.GetSpeceficWeapon(weaponType));
    }

    protected void RotateWeapon(Vector3 enemyPos)
    {
    

            Vector3 nearestEnemyPos = enemyPos;
        weaponAngle = MyUtils.GetAngle(nearestEnemyPos, this.transform.position);

        if (weaponPosit != null&& weaponHandler.CanRotateWeapon()==true)
        {          
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, weaponAngle);
        }

        //flip
        if ((weaponAngle >= 0f && weaponAngle <= 90) ||
              weaponAngle >= 270f && weaponAngle <= 360)
        {
            if (weaponHandler != null)
            {
                weaponHandler.FlipWeapon(false);
                FlipCharacter(true);
            }
        }
        else
        {
            if (weaponHandler != null)
            {
                weaponHandler.FlipWeapon(true);
                FlipCharacter(false);
            }
        }

    }

    protected void MoveInPc()
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

    protected void MoveInMobie()
    {
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

            UIUpdate();

            if (hp <= 0)
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
            playerUi.SetHpBar(hp, hpMax);
    }

    protected void DieAction()
    {
        Debug.Log("CharacterDie");
    }

    public void GetBulletItem()
    {
        if (weaponHandler != null)
        {
            weaponHandler.GetBulletItem();

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

    public bool isInventoryFull()
    {
        if (inventory == null) return true;

        return inventory.isInventoryFull();
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
                ItemInfoBar.Instance.SetItemBar(dropItem.itemBase, dropItem.ClickAction);
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
                ItemInfoBar.Instance.SetItemBar(dropItem.itemBase, dropItem.ClickAction);
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

    public virtual void UseCharacterSkill()
    {
        //자식에서 구현띠
    }


    public bool CanHeal()
    {
        return hp < hpMax;
    }

    public void GetHp(int value)
    {
        Debug.Log("Heal" + value.ToString());
        hp += value;
        if (hp > hpMax)
        {
            hp = hpMax;
        }

        UIUpdate();
    }

    public bool CanUseStimulant()
    {
        //체력이 풀피이거나 이미 사용중이면 사용 불가
        return !(hp >= hpMax) && !nowUseStimulant;
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

}
