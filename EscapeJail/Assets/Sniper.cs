using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : CharacterBase
{

    private float maxSaveTime = 5f;
    private float nowRemainTime = 0f;
    private bool nowUsingSkill = false;
    private float slowTimeRatio = 0.4f;
    private void SetSkillTime(float t)
    {
        nowRemainTime = t;
        maxSaveTime = t;
    }

    private new void Awake()
    {
        base.Awake();
        SetHp(10);
        SetSkillTime(5f);
    }

    private new void Start()
    {
        base.Start();
        SetWeapon();
        originSpeed = moveSpeed;
    }

    private new void Update()
    {
        base.Update();

    }

    public IEnumerator SkillRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                for (int i = 0; i < Input.touchCount; ++i)
                {
                    Vector2 test = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    int layerMask = MyUtils.GetLayerMaskByString("Enemy");
                    if (Input.GetTouch(i).phase == TouchPhase.Stationary)
                    {
                        test = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                        RaycastHit2D rayHit = Physics2D.Raycast(test, (Input.GetTouch(i).position),1f, layerMask);
                        if (rayHit.collider!=null)
                        {
                            //이펙트 호출
                            ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
                            if (effect != null)
                                effect.Initilaize(this.transform.position, "revolver", 0.5f, 2f);
                        }
                    }
                }
            }

            yield return null;
        }
    }

    public override void UseCharacterSkill()
    {
        if (nowUsingSkill == true)
        {
            SkillOff();
        }
        else if (nowUsingSkill == false)
        {
            SkillOn();
        }
    }

    private void SkillOn()
    {
        nowUsingSkill = true;
        CameraController.Instance.SniperAimEffectOnOff(true);

        if (weaponHandler != null)
            weaponHandler.gameObject.SetActive(false);

        if (animator != null)
        {
            animator.speed = 2f;
            animator.SetTrigger("FireReadyTrigger");
        }

        if (rb != null)
            rb.velocity = Vector3.zero;

        TimeManager.Instance.BulletTimeOn(slowTimeRatio);

        StartCoroutine("SkillRoutine");

    }
    private void SkillOff()
    {
        nowUsingSkill = false;
        CameraController.Instance.SniperAimEffectOnOff(false);

        if (weaponHandler != null)
            weaponHandler.gameObject.SetActive(true);

        if (animator != null)
        {
            animator.speed = 1f;
            animator.SetTrigger("SkillEnd");
        }

        TimeManager.Instance.BulletTimeOff();
        StopCoroutine("SkillRoutine");
    }

    public override void FireWeapon()
    {
        if (nowUsingSkill == true) return;
        base.FireWeapon();
    }

    protected override void MoveInMobie()
    {
        if (nowUsingSkill == true) return;
        base.MoveInMobie();
    }

    protected override void MoveInPc()
    {
        if (nowUsingSkill == true) return;
        base.MoveInPc();
    }
}
