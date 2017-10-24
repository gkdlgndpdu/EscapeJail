using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class Scientist4 : MonsterBase
{

    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Scientist4;
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
        nowWeapon.ChangeWeapon(new Scientist_GasGun());

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
        Debug.Log("Bang");

    }


}
