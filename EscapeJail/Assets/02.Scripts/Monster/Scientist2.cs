using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Scientist2 : MonsterBase
{

    protected override void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Scientist2;
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
        StartCoroutine(FireRoutine2());
    }

    protected override void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new Scientist_GasGun());

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

        yield return new WaitForSeconds(Random.Range(3f, 3f));
        while (true)
        {
            //
            //발사
            SetAnimation(MonsterState.Attack);
           //
            yield return new WaitForSeconds(Random.Range(3f,6f));
        }
    }

    protected IEnumerator FireRoutine2()
    {
        while (true)
        {

            FireFireBullet();
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    public void FireFireBullet()
    {
        if (target == null) return;
        Vector3 fireDirection = target.position - this.transform.position;

        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            Vector3 fireDir = fireDirection;
            fireDir = Quaternion.Euler(0f, 0f, Random.Range(-2f, 2f)) * fireDir;
            bullet.Initialize(this.transform.position, fireDir.normalized, 6f, BulletType.EnemyBullet, 1.2f);
            bullet.InitializeImage("White", false);
            bullet.SetBloom(true, Color.red);
            bullet.SetPollute(CharacterCondition.InFire);

            SoundManager.Instance.PlaySoundEffect("poisongun");
        }
    }


    public void FireGasGun()
    {
        FireWeapon();
        Debug.Log("Bang");

    }




}
