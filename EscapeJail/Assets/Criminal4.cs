using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal4 : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal4;
        SetHp(10);
        nearestAcessDistance = 7f;
        attackDelay = 1f;

    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }


    private void FireGranade()
    {
        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            float reBoundValue = 5f;
            Vector3 firePos = this.transform.position;
            Vector3 fireDir = GamePlayerManager.Instance.player.transform.position - this.transform.position;
            fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
            bullet.Initialize(firePos, fireDir.normalized, 2, BulletType.EnemyBullet, 1, 1, 2f);
            bullet.InitializeImage("white", false);
            bullet.SetEffectName("CriminalGrandeExplosion", 1.5f);
            bullet.SetExplosion(1.5f);


        }
    }



    private new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    private void Update()
    {
        ActionCheck();
        if (isActionStart == false) return;

        MoveToTarget();
        NearAttackLogic();


    }



    protected override IEnumerator AttackRoutine()
    {
        nowAttack = true;
        SetAnimation(MonsterState.Attack);
        yield return null;
        while (true)
        {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")==false)
            {
                FireGranade();
                break;
            }
            yield return null;
        }


        yield return new WaitForSeconds(3f);
        nowAttack = false;
    }






}
