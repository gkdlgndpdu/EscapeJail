using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist3 : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Scientist3;
        SetHp(10);
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
        NearAttackRotate();
        if (canMove() == false) return;


        MoveToTarget();
        NearAttackLogic();

    }


    protected override void SetDie()
    {
        base.SetDie();

        //독지대
        PollutedArea pollutedArea = ObjectManager.Instance.pollutedAreaPool.GetItem();
        if (pollutedArea != null)
            pollutedArea.Initialize(this.transform.position, 5f, 2f, CharacterCondition.InPoison,BulletType.EnemyBullet);


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



}
