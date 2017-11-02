using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Soldier : CharacterBase
{
    private bool isDodge =false;
    private float dodgeSpeed = 5f;
    private float dodgeCoolTime = 0.3f;
    private bool isDodgeCoolTime = false;
    private new void Awake()
    {
        base.Awake();

        SetHp(10);     
    
    }
    // Use this for initialization
    private new void Start()
    {
        SetWeapon();

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
        if (isDodgeCoolTime == true|| isDodge==true) return;

        isDodge = true;

        isDodgeCoolTime = true;

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

        StartCoroutine(DodgeCoolTimeRoutine());
    }

    IEnumerator DodgeCoolTimeRoutine()
    {
        yield return new WaitForSeconds(dodgeCoolTime);
        isDodgeCoolTime = false;
    }

    


    public override void UseCharacterSkill()
    {    
        DodgeOn();
    }

    public override void FireWeapon()
    {
        if (isDodge == true) return;
        base.FireWeapon();
    }


}
