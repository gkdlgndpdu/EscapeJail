using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal3 : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal3;
        SetHp(30);
        nearestAcessDistance = 1f;
        weaponPosit.gameObject.SetActive(false);
        attackDelay = 1f;
        moveSpeed = 2f;

    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }



    private new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead == true) return;

        ActionCheck();
        if (isActionStart == false) return;

        MoveToTarget();
        NearAttackLogic();
        RotateWeapon();

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
