using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected WeaponBase nowWeapon;

    //무기 장착 위치
    [SerializeField]
    protected Transform weaponPosit;

    protected List<Weapon> nowHaveWeapons = new List<Weapon>();

    [SerializeField]
    protected Transform FirePos;

    //UI
    [SerializeField]
    protected PlayerUI playerUI;




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
        rb = GetComponent<Rigidbody2D>();

        //임시로 총기 넣어줌
        nowHaveWeapons.Add(new Revolver());
        nowHaveWeapons.Add(new ShotGun());
        nowHaveWeapons.Add(new WaterGun());
        nowHaveWeapons.Add(new AssaultRifle());
        nowWeapon.SetWeapon(nowHaveWeapons[0]);


        SetUpComponent();

    }

    private void OnDestroy()
    {
        for (int i = 0; i < nowHaveWeapons.Count; i++)
        {
            nowHaveWeapons[i] = null;
        }
        nowHaveWeapons = null;
    }

    protected void SetUpComponent()
    {
        //컴포넌트
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            if (nearEnemy != null && nowWeapon != null)
            {
                Vector3 fireDir = nearEnemy.transform.position - this.transform.position;
                nowWeapon.FireBullet(this.FirePos.position, fireDir);

                //회전
            }
            else if (nearEnemy == null)
            {
                Vector3 fireDir = Vector3.right;
                nowWeapon.FireBullet(this.FirePos.position, fireDir);
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nowWeapon.SetWeapon(nowHaveWeapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nowWeapon.SetWeapon(nowHaveWeapons[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nowWeapon.SetWeapon(nowHaveWeapons[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            nowWeapon.SetWeapon(nowHaveWeapons[3]);
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
            if (nowWeapon != null)
            {
                nowWeapon.FlipWeapon(false);
                FlipCharacter(true);
            }
        }
        else
        {
            if (nowWeapon != null)
            {
                nowWeapon.FlipWeapon(true);
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
}
