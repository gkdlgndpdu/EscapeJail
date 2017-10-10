using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Criminal1 : MonsterBase
{
    private bool isDisable = false;


    public new void SetUpMonsterAttribute()
    {
        monsterName = MonsterName.Criminal1;
        SetHp(10);
        nearestAcessDistance = 5f;
        SetWeapon();
    }
    private void SetWeapon()
    {
        nowWeapon.ChangeWeapon(new CriminalPistol());

    }

    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetUpMonsterAttribute();
    }


    protected void OnDisable()
    {

        isDisable = true;


    }

    protected new void OnEnable()
    {
        base.OnEnable();
        if (weaponPosit != null)
            weaponPosit.gameObject.SetActive(true);
        StartCoroutine(TempFireRoutine());
        isDisable = false;
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
        RotateWeapon();

     
    }

    IEnumerator TempFireRoutine()
    {
        while (true)
        {
            if (isDisable == true) yield return null;
            for(int i = 0; i < 3; i++)
            {
            FireWeapon();
                yield return new WaitForSeconds(0.1f);
            }
       

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
