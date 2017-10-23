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
    //단위 초 
    protected int immuneTime = 1;

    //상태
    protected bool isImmune = false;
    //이동
    protected Vector3 moveDir = Vector3.zero;
    protected Vector3 lastMoveDir = Vector3.zero;

    //무기 회전 관련
    protected float weaponAngle = 0f;
    protected bool nowWeaponRotate = false;


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

    protected InventoryUi inventoryUi;
    [SerializeField]
    private Slider weaponSlider;

    //인벤토리
    protected Inventory inventory;

    //아머
    protected ArmorSystem armorSystem;



    protected void Awake()
    {
        SetLayerAndTag();

        Initialize();

 

        if (weaponHandler != null && slashObject != null)
        {
            weaponHandler.SetSlashObject(slashObject);
            slashObject.gameObject.SetActive(false);
        }

        if (weaponHandler != null && weaponSlider != null)
        {
            weaponHandler.SetSlider(weaponSlider);
        }

        if (weaponHandler != null && playerUi != null)
        {
            weaponHandler.SetWeaponUi(playerUi.weaponUi);
        }

        
    }

    protected void SetWeapon()
    {
        AddWeapon(new shortknife());

        UIUpdate();
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
            armorSystem = new ArmorSystem(inventoryUi, playerUi, playerUi.hpBar);
            //임시
            SetArmor(0);
        }
    }



    public void GetBag(int level)
    {
        if (inventory != null)
            inventory.SetInventorySize(level);
    }


    public void SetArmor(int level)
    {
        if (armorSystem != null)
            armorSystem.SetArmor(level);
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
        MoveInPc();
    }

    public virtual void FireWeapon()
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


    private void RotateWeapon()
    {  

        GameObject nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
        if (nearEnemy != null && weaponHandler.attackType ==AttackType.gun)
            RotateWeapon(nearEnemy.transform.position);
        else if(nearEnemy==null ||weaponHandler.attackType==AttackType.near)
        {
            RotateWeapon(this.transform.position + moveDir);
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

        if (moveDir != Vector3.zero)
            RotateWeapon();

    }

    public void ChangeWeapon()
    {
        weaponHandler.ChangeWeapon(inventory.GetWeapon());
    }

    protected void RotateWeapon(Vector3 enemyPos)
    {
        if (nowWeaponRotate == true) return;

        Vector3 nearestEnemyPos = enemyPos;
        weaponAngle = MyUtils.GetAngle(nearestEnemyPos, this.transform.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, weaponAngle);

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

    protected void NearWeaponRotate()
    {
        nowWeaponRotate = true;

    }

    protected void NearWeaponRotateEnd()
    {
        Debug.Log("들어오냐?");
        iTween.Stop(weaponPosit.gameObject);
        nowWeaponRotate = false;
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
            weaponHandler.ChangeWeapon(inventory.GetWeapon());
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
        }
    }

    public bool isInventoryFull()
    {
        if (inventory == null) return true;

        return inventory.isInventoryFull();
    }

    //반응키 눌렸을때
    public void ReactiveButtonClick()
    {

        int activeLayer = MyUtils.GetLayerMaskByString("DropItem");
        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, 0.5f, activeLayer);

        if (colls == null) return;
        if (colls.Length == 0) return;

        if (colls.Length == 1)
        {
            iReactiveAction action = colls[0].gameObject.GetComponent<iReactiveAction>();
            if (action != null)
                action.ClickAction();
        }
        else if (colls.Length > 1)
        {
            Array.Sort(colls, (a, b) =>
            {
                if (Vector3.Distance(this.transform.position, a.transform.position) >
                Vector3.Distance(this.transform.position, b.transform.position))
                    return 1;
                else return -1;

            });

            iReactiveAction action = colls[0].gameObject.GetComponent<iReactiveAction>();
            if (action != null)
                action.ClickAction();

        }



    }

    public virtual void UseCharacterSkill()
    {
        //자식에서 구현띠
    }


}
