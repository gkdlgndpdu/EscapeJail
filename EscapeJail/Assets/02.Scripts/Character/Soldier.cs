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

    protected override void ResetAbility()
    {
        

    }
    protected override void DieAction()
    {
        base.DieAction();
        if (isDodge == true)
            DodgeOff();
    }
    // Use this for initialization
    private new void Start()
    {
        base.Start();
        SetWeapon();

    }

    // Update is called once per frame
    private new void Update()
    {   
        if (isDodge == true) return;
        base.Update();

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
        SoundManager.Instance.PlaySoundEffect("dodge");


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
        float count = 0f;
        while (true)
        {
            count += Time.deltaTime;

            playerUi.SetSkillButtonProgress(count, dodgeCoolTime);
            if (count > dodgeCoolTime)
            {
                isDodgeCoolTime = false;

                yield break;
            }
            yield return null;
        }

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
