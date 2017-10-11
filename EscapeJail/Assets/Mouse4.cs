using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Mouse4 : MonsterBase
{
    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Mouse4;
        SetHp(10);
        nearestAcessDistance = 6f;
        SetWeapon();
    }

    private void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new AroundGun());

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

    private void FireWeapon()
    {
        if (nowWeapon != null)
            nowWeapon.FireBullet(this.transform.position, Vector3.zero);
    }

}
