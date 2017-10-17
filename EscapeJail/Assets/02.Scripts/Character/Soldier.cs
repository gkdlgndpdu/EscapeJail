using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Soldier : CharacterBase
{
    private bool isDodge =false;
    private float dodgeSpeed = 5f;
    private new void Awake()
    {
        base.Awake();

        hp = 10;
        hpMax = hp;
    
    }
    // Use this for initialization
    private new void Start()
    {
        //  AddWeapon(new Bazooka());
        AddWeapon(new Minigun());
        //AddWeapon(new Hammer());
        UIUpdate();

    }

    // Update is called once per frame
    private new void Update()
    {
   
        if (isDodge == true) return;

        base.Update();

#if UNITY_EDITOR
        //임시코드
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UseCharacterSkill();
        }
        //임시코드
#endif
    }

    private void DodgeOn()
    {
        isDodge = true;

        if (rb != null)
            rb.velocity = lastMoveDir* dodgeSpeed;

        if (animator != null)
            animator.SetTrigger("SoldierDodge");

        this.gameObject.layer = LayerMask.NameToLayer("SoldierDodge");



    }

    public void DodgeOff()
    {
        isDodge = false;

        if (rb != null)
            rb.velocity = Vector3.zero;

        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }


    public override void UseCharacterSkill()
    {
        if(isDodge!=true)
        DodgeOn();

    }

    public override void FireWeapon()
    {
        if (isDodge == true) return;
        base.FireWeapon();
    }


}
