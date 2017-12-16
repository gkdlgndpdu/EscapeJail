using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Scientist4 : MonsterBase
{

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Scientist4;
        hasBullet = true;
        nearestAcessDistance = 5f;
        
    }
    public override void ResetMonster()
    {
        base.ResetMonster();
  

    }

    protected override void StartMyCoroutine()
    {
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new Scientist_PoisionGun());

    }


  

    // Update is called once per frame
    private void Update()
    {
        RotateWeapon();
        if (canMove() == false) return;
        MoveToTarget();

    }

    protected override void SetDie()
    {
        base.SetDie();

        //독지대
        PollutedArea pollutedArea = ObjectManager.Instance.pollutedAreaPool.GetItem();
        if (pollutedArea != null)
            pollutedArea.Initialize(this.transform.position, 2.5f, 2f, CharacterCondition.InPoison, BulletType.EnemyBullet);


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
            yield return new WaitForSeconds(3f);
        }
    }


    public void FireGasGun()
    {
        FireWeapon();
        //Debug.Log("Bang");

    }


}
