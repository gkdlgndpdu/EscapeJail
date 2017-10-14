using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Guard2 : MonsterBase
{
    private float fireDelay = 1f;
    private float rageFireDelay = 0.3f;
    private float originFireDelay = 0f;
    private float originSpeed = 0f;
    private float rageSpeed = 3f;
    private bool isRageOn = false;
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Guard2;
        SetHp(10);
        nearestAcessDistance = 5f;
        SetWeapon();

    }
    public override void ResetMonster()
    {
        base.ResetMonster();
        StartCoroutine(RandomMovePattern());
        StartCoroutine(FireRoutine());
        RageOnOff(false);


    }

  

    private void RageOnOff(bool OnOff)
    {
        isRageOn = OnOff;

        if (OnOff == true)
        {
            GameObject target = spriteRenderer.gameObject;
            iTween.ColorTo(target, iTween.Hash("loopType", "pingPong", "Time", 0.1f, "Color", Color.red));

            this.moveSpeed = rageSpeed;
            this.fireDelay = rageFireDelay;       

        }
        else if (OnOff == false)
        {
            GameObject target = spriteRenderer.gameObject;
            iTween.ColorTo(target, Color.white, 0.1f);

            this.moveSpeed = originSpeed;
            this.fireDelay = originFireDelay;

        }


    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);

        if (hp <= hpMax / 2 && isRageOn == false)
            RageOnOff(true);

    }

    private void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new GuardRifle());

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
        SetOriginData();
    }

    private void SetOriginData()
    {
        originSpeed = moveSpeed;
        originFireDelay = fireDelay;

    }

    // Update is called once per frame
    private void Update()
    {
        RotateWeapon();
        if (canMove() == false) return;
        MoveToTarget();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            RageOnOff(true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RageOnOff(false);
        }

    }

    protected override IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
            Vector3 fireDIr = PlayerPos - this.transform.position;

            for (int i = 0; i < 5; i++)
            {
                FireWeapon(fireDIr);
                yield return new WaitForSeconds(0.08f);
            }


            yield return new WaitForSeconds(fireDelay);
        }
    }

    protected void RotateWeapon()
    {
        float angle = MyUtils.GetAngle(this.transform.position, target.position);
        if (weaponPosit != null)
            weaponPosit.rotation = Quaternion.Euler(0f, 0f, angle);

        //flip
        if ((angle >= 0f && angle <= 90) ||
              angle >= 270f && angle <= 360)
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(false);
        }
        else
        {
            if (nowWeapon != null)
                nowWeapon.FlipWeapon(true);
        }

    }

    private void FireWeapon(Vector3 fireDir)
    {
        if (nowWeapon != null)
            nowWeapon.FireBullet(this.transform.position, fireDir);
    }
}
