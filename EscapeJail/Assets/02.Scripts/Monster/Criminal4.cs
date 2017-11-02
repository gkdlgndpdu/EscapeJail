using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal4 : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal4;
        SetHp(5);     
        moveSpeed = 2f;
    }

    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(AttackRoutine());
    } 

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }


    public void FireGranade()
    {
        float bulletSpeed = 3f;
        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            float reBoundValue = 5f;
            Vector3 firePos = this.transform.position;
            Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
            fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
            bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet,1.3f, 2, 2f);
            bullet.InitializeImage("CriminalGrande", true);
            bullet.SetEffectName("GranadeExplosion", 2f);
            bullet.SetExplosion(1f);
            bullet.SetBloom(false);


        }
    }



    private new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove() == false) return;

        MoveToTarget();
        NearAttackLogic();


    }



    protected override IEnumerator AttackRoutine()
    {
        nowAttack = true;
        SetAnimation(MonsterState.Attack);   
        yield return new WaitForSeconds(4.5f);
        nowAttack = false;
    }






}
