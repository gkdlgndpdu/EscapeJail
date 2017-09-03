using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : MonoBehaviour
{
    //컴포넌트    
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    //값변수
    [SerializeField]
    protected float moveSpeed = 10f;
    protected int hp = 0;
    protected int maxHp = 0;

    //무기
    [SerializeField]
    protected WeaponHandler weaponHandler;

    //무기 장착 위치
    [SerializeField]
    protected Transform weaponPosit;

    [SerializeField]
    protected Transform FirePos;

    //UI
    [SerializeField]
    protected PlayerUI playerUI;

    //인벤토리
    protected Inventory inventory;
    



    protected void Awake()
    {
        SetLayerAndTag();
        Initialize();
    }

    private void SetLayerAndTag()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        this.gameObject.tag = "Player";
    }
    protected void Initialize()
    {
        //컴포넌트
        SetupComponent();

        //스크립트
        inventory = new Inventory();


        //임시로 총기 넣어줌      
        AddWeapon(new Revolver());
        AddWeapon(new ShotGun());
        AddWeapon(new AssaultRifle());

        if (weaponHandler != null&& inventory!=null)
            weaponHandler.ChangeWeapon(inventory.GetWeapon());


  

    }

    public void AddWeapon(Weapon weapon)
    {
        if (inventory != null && weapon != null)
            inventory.AddWeapon(weapon);
    }



    protected void SetupComponent()
    {
        //컴포넌트
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        HandleNowWeapon();
    }

    protected void FixedUpdate()
    {
        MoveInPc();
    }

    protected void HandleNowWeapon()
    {
        MonsterBase nearEnemy = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
        //발사
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (nearEnemy != null && weaponHandler != null)
            {
                Vector3 fireDir = nearEnemy.transform.position - this.transform.position;
                weaponHandler.FireBullet(this.FirePos.position, fireDir);

                //회전
            }
            else if (nearEnemy == null)
            {
                Vector3 fireDir = Vector3.right;
                weaponHandler.FireBullet(this.FirePos.position, fireDir);
            }



        }
        if (nearEnemy != null)
            RotateWeapon(nearEnemy.transform.position);
        else
        {
            RotateWeapon(this.transform.position +Vector3.right);
        }

        //무기 변경
        ChangeWeapon();
    }

    protected void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponHandler.ChangeWeapon(inventory.GetWeapon());
        }
       

    }

    protected void RotateWeapon(Vector3 enemyPos)
    {
        Vector3 nearestEnemyPos = enemyPos;
        float angle = MyUtils.GetAngle(nearestEnemyPos, this.transform.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);

        //flip
        if ((angle >= 0f && angle <= 90) ||
              angle >= 270f && angle <= 360)
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
        rb.velocity = Vector2.zero;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = Vector3.right * h + Vector3.up * v;
        // moveDir.Normalize();

        //이동
        if (rb != null)
            rb.velocity = moveDir * moveSpeed * Time.deltaTime;

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

        // FlipCharacter(MoveDir);

    }

    protected void FlipCharacter(bool flip)
    {
        if (spriteRenderer == null) return;

        spriteRenderer.flipX = flip;



    }

    public void GetDamage(int damage)
    {
        hp -= damage;

        UIUpdate();

        if (hp <= 0)
        {
            DieAction();
        }
    }

    protected void UIUpdate()
    {
        if (playerUI != null)
            playerUI.SetHpBar(hp, maxHp);
    }

    protected void DieAction()
    {
        Debug.Log("CharacterDie");
    }

    public void HandleItem()
    {
        
    

    }
}
