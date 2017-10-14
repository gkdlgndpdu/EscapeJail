using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard3 : MonsterBase
{

    private bool isSheildOn = false;
    private float shieldLastTime = 2.0f;
    private float originMoveSpeed = 1.0f;
    private float dashMoveSpeed = 2.5f;

    //해당 거리보다 멀리 있으면 실드킴 무조건
    private float shieldOffDistance = 3f;


    public override void ResetMonster()
    {
        base.ResetMonster();
       // ShieldOnOff(false);

    }

    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard3;
        SetHp(10);
        nearestAcessDistance = 1f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = 1f;

    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }


    private void ShieldOnOff(bool OnOff)
    {
        if (animator == null) return;

        isSheildOn = OnOff;

        if (OnOff == true)
        {
            animator.SetTrigger("ShieldOn");
            moveSpeed = originMoveSpeed;

        }
        else if (OnOff == false)
        {
            animator.SetTrigger("ShieldOff");
            moveSpeed = dashMoveSpeed;
        }
    }




    public override void GetDamage(int damage)
    {
        if (isSheildOn == true)
        {
            return;
            //파랑 빤작이
        } 

        this.hp -= damage;
        UpdateHud();
        if (hp <= 0)
        {
            SetDie();
        }
    }


    private new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    private void Update()
    {
        RotateWeapon();
        if (canMove() == false) return;
        ShieldRoutine();
        MoveToTarget();

        if(isSheildOn==false)
        NearAttackLogic();

    }

    //거리에 따라서 실드 껏다 켰다
    private void ShieldRoutine()
    {
        if (CanShieldOff() == true && isSheildOn == false)
        {
            ShieldOnOff(true);        
        }
        else if(CanShieldOff()==false&&isSheildOn==true)
        {
            ShieldOnOff(false);
        }
            
    }

    protected bool CanShieldOff()
    {
        return GetDistanceToPlayer()>=shieldOffDistance ;
    }

    protected override IEnumerator AttackRoutine()
    {
        nowAttack = true;
        SetAnimation(MonsterState.Attack);
        yield return new WaitForSeconds(attackDelay);
        nowAttack = false;
    }


    public void OnDisable()
    {
        AttackOff();
    }

    protected void RotateWeapon()
    {
        if (weaponPosit.gameObject.activeSelf == false) return;
        float angle = MyUtils.GetAngle(target.position, this.transform.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);

    }

}
