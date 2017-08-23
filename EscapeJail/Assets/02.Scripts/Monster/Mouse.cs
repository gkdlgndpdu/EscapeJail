using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        SetHp(10);
        nearestAcessDistance = 0.5f;
        weaponPosit.gameObject.SetActive(false);
    }

    // Use this for initialization
    private new void Start ()
    {
        base.Start();  
        SetUpMonsterAttribute();
    }

    private void AttackOn()
    {
        if(weaponPosit!=null)
            weaponPosit.gameObject.SetActive(true);
    }

    private void AttackOff()
    {
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(false);
    }





    private new void Awake()
    {
        base.Awake();      
    }
	
	// Update is called once per frame
	private void Update ()
    {
        ActionCheck();
        if (isActionStart == false) return;

        MoveToTarget();
        NearAttackLogic();
        RotateWeapon();

    }

    private void NearAttackLogic()
    {
        if (IsInAcessArea(nearestAcessDistance) == true&& nowAttack==false)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        nowAttack = true;
        SetAnimation(MonsterState.Attack);
        AttackOn();
        yield return new WaitForSeconds(0.5f);
        nowAttack = false;
        AttackOff();
    }


    protected void RotateWeapon()
    {
        if (weaponPosit.gameObject.activeSelf == false) return;
        float angle = MyUtils.GetAngle(target.position,this.transform.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);   

    }


}
