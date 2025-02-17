﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BossEventQueue))]
public class BossBase : CharacterInfo
{
    protected bool isPatternStart = false;
 
    protected BossModule bossModule;

    protected BossEventQueue bossEventQueue;

    protected bool isBossDie = false;

    protected float moveSpeed = 1f;


    //컴포넌트
    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    [SerializeField]
    protected BossHpBar bosshpBar;

    //나머지는 자식에서 구현

        /// <summary>
        /// 하는일 : 보스 UI켜주기, 몬스터매니저에 추가
        /// </summary>
    public virtual void StartBossPattern()
    {
        SetUiOnOff(true);
        isPatternStart = true;
        //이동속도 정상화
        GamePlayerManager.Instance.player.SetBurstSpeed(false);
        AddToList();
        MiniMap.Instance.MinimapOnOff(false);


    }

    protected void AddToList()
    {
        MonsterManager.Instance.AddToList(this.gameObject);
    }

    protected void DeleteInList()
    {
        MonsterManager.Instance.DeleteInList(this.gameObject);
    }

    //protected void OnDisable()
    //{
    //    DeleteInList();
    //}



    public void SetUiOnOff(bool OnOff)
    {
        if (bosshpBar == null)
            bosshpBar = BossHpBar.Instance;

        if (bosshpBar != null)
        {
            bosshpBar.gameObject.SetActive(OnOff);
            bosshpBar.UpdateBar(1f, 1f);
        }
    }

    protected void Awake()
    {
        bossModule = GetComponentInParent<BossModule>();
        bossEventQueue = GetComponent<BossEventQueue>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.mass = 100f;

        SetUiOnOff(false);
    }


    public override void SetPush(Vector3 pushPoint, float pushPower, int damage)
    {
        GetDamage(damage);
    }

    public override void GetDamage(int damage)
    {
        if (isBossDie == true|| isPatternStart==false) return;

        if (NowSelectPassive.Instance.HasPassive(PassiveType.CrossCounter) == true)
        {
            damage *= 2;
        }

        VampiricGunEffect();
        hp -= damage;
        SoundManager.Instance.PlaySoundEffect("monsterdamage");

        if (bosshpBar != null)
            bosshpBar.UpdateBar(hp, hpMax);
        if (hp <= 0)
        {
            isBossDie = true;
            BossDie();
        }

       

    }


    protected virtual void BossDie()
    {
        if (bossEventQueue != null)
            bossEventQueue.Stop();

        if (boxCollider != null)
            boxCollider.enabled = false;
   
        if (animator != null)
            animator.SetTrigger("DeadTrigger");
        if (rb != null)
            rb.velocity = Vector3.zero;


        //할꺼 해주고
        //이동속도 정상화
        GamePlayerManager.Instance.player.SetBurstSpeed(true);
        //할꺼 해주고
        GamePlayerManager.Instance.scoreCounter.KillBoss();
        GamePlayerManager.Instance.scoreCounter.ClearStage();

        GamePlayerManager.Instance.player.GetCoin(100);

        MiniMap.Instance.MinimapOnOff(true);

        if (bossModule != null)
        {
            bossModule.ClearRoom();
            bossModule.WhenBossDie();

        }
        SetUiOnOff(false);

        DeleteInList();

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.8f);

        //아머채워줌
        if (GamePlayerManager.Instance != null)
        {
            if (GamePlayerManager.Instance.player != null)
                GamePlayerManager.Instance.player.SetArmorFull();
        }

        //총알삭제
        ObjectManager.Instance.AllEnemyBulletDestroy();
    }




}
