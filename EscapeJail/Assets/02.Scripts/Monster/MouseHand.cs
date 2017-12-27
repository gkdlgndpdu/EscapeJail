using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class MouseHand : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float originAnimSpeed;
    private float delay = 0.7f;

    private bool isFirstCreate = true;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (animator != null)
        {
            originAnimSpeed = animator.speed;   
        }
      
    }

    private void HandOn()
    {
        if (boxCollider != null)
            boxCollider.enabled = true;

        if (animator != null)
        {
            SoundManager.Instance.PlaySoundEffect("earthquake2");
            animator.speed = originAnimSpeed;
        }
    }
    private void HandOff()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;


        if (animator != null)
            animator.speed = 0f;
    }

    public void OnEnable()
    {
        if(isFirstCreate==false)
        Invoke("HandOn", delay);

        isFirstCreate = false;
    }

    public void OnDisable()
    {
        HandOff();
    }

    //애니메이션에 연결됨
    public void HandAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }
 
    private void FireBullet()
    {
        float bulletSpeed = 3f;
        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
        if (bullet != null)
        {
            Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
            Vector3 fireDIr = PlayerPos - this.transform.position;            
            bullet.Initialize(this.transform.position, fireDIr.normalized, bulletSpeed, BulletType.EnemyBullet, 1f);
            bullet.InitializeImage("StoneBullet", false);
            bullet.SetEffectName("stoneExplosion",2f);
            bullet.SetBloom(false);
          
        }
    }

}
