using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard4 : MonsterBase
{

    private bool isSheildOn = false;



    //해당 거리보다 멀리 있으면 실드킴 무조건
    private float shieldOffDistance = 3f;


    public override void ResetMonster()
    {
        base.ResetMonster();
        ShieldEffectOff();
        AttackOn();
        StartCoroutine("FireRoutine");

    }

    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard4;
        SetHp(10);
        nearestAcessDistance = 3f;

        attackDelay = 1f;
        moveSpeed = 1f;
        SetWeapon();

    }

    private void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new GuardPistol());

    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(true);
    }

    private void ShieldOnOff(bool OnOff)
    {
        if (animator == null) return;

        isSheildOn = OnOff;

        if (OnOff == true)
        {
            animator.SetTrigger("ShieldOn");
            StopCoroutine("FireRoutine");
            if (weaponPosit != null)
                weaponPosit.gameObject.SetActive(false);

        }
        else if (OnOff == false)
        {
            animator.SetTrigger("ShieldOff");
            StartCoroutine("FireRoutine");
            if (weaponPosit != null)
                weaponPosit.gameObject.SetActive(true);

        }
    }




    public override void GetDamage(int damage)
    {
        if (isSheildOn == true)
        {
            ShieldEffectOn();
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

        MoveAgainstTarget();
        //MoveToTarget();

   

    }

    //거리에 따라서 실드 껏다 켰다
    private void ShieldRoutine()
    {
        if (CanShieldOff() == true && isSheildOn == false)
        {
            ShieldOnOff(true);
        }
        else if (CanShieldOff() == false && isSheildOn == true)
        {
            ShieldOnOff(false);
        }

    }

    protected bool CanShieldOff()
    {
        return GetDistanceToPlayer() <= shieldOffDistance;
    }

    protected override IEnumerator FireRoutine()
    {
        while (true)
        {
            if(isSheildOn==false)
            FireWeapon();

            yield return new WaitForSeconds(1f);
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

    private void ShieldEffectOn()
    {
        GameObject target = spriteRenderer.gameObject;
        iTween.ColorTo(target, iTween.Hash("loopType", "pingPong", "Time", 0.05f, "Color", Color.blue));

        Invoke("ShieldEffectOff", 1f);

    }

    private void ShieldEffectOff()
    {
        GameObject target = spriteRenderer.gameObject;
        iTween.ColorTo(target, Color.white, 0.1f);
    }

}
