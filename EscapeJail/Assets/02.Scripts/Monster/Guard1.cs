using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard1 : MonsterBase
{

    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard1;
        SetHp(10);
        nearestAcessDistance = 5f;
        SetWeapon();
    }
    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
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
            FireWeapon();
            yield return new WaitForSeconds(1f);
        }
    }

  

 

}
