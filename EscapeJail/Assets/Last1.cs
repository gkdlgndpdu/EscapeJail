using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Last1 : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Last1;
        SetHp(10);
        nearestAcessDistance = 5f;
        SetWeapon();
    } 
    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
        AttackOff();
    }

    private void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new Last1Gun());

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

    private new void Awake()
    {
        base.Awake();

    }

    // Update is called once per frame
    private void Update()
    {
        RotateWeapon();
        if (canMove() == false) return;
        MoveToTarget();

    }

    protected override IEnumerator FireRoutine()
    {
        while (true)
        {
            //
            //발사
            SetAnimation(MonsterState.Attack);
            if (rb != null)
                rb.velocity = Vector3.zero;
            //
            yield return new WaitForSeconds(Random.Range(1f,5f));
        }
    }


    public void FireGun()
    {
        FireWeapon();
        Debug.Log("Bang");

    }


}
