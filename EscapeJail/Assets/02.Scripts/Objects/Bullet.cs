using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    PlayerBullet,
    EnemyBullet
}


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private int power = 0;
    private BulletType bulletType;
    private Rigidbody2D rb;
    private string effectName = "revolver";
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    private float lifeTime = 1.0f;
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            defaultSprite = spriteRenderer.sprite;


    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, float bulletScale = 1f, int power = 1,float lifeTime = 999f)
    {
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);

        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

        this.bulletType = bulletType;
        SetLayer(bulletType);

        this.transform.localScale = Vector3.one * bulletScale;

        this.power = power;


        if (animator != null)
            animator.runtimeAnimatorController = null;

        if (lifeTime != 999)
        {
            Invoke("BulletDestroy", lifeTime);
        }              
    }

    public void SetEffectName(string effectName)
    {
        this.effectName = effectName;
    }

    public void InitializeImage(string bulletImageName, bool isAnimBullet)
    {
        if (isAnimBullet == true && animator != null)
        {
            RuntimeAnimatorController animController = ObjectManager.LoadGameObject(string.Format("Animators/Bullet/{0}", bulletImageName)) as RuntimeAnimatorController;
            if (animController != null)
            {
                animator.runtimeAnimatorController = animController;
            }
        }
        else if (isAnimBullet == false && spriteRenderer != null)
        {
            Sprite sprite = ObjectManager.LoadGameObject(string.Format("Sprites/Bullet/{0}", bulletImageName)) as Sprite;
            if (sprite != null)
                spriteRenderer.sprite = sprite;
            else if (sprite == null)
                spriteRenderer.sprite = defaultSprite;

        }


    }

    public void SetBulletLifeTime(float lifeTime)
    {
        Invoke("BulletDestroy", lifeTime);
    }

    private void SetLayer(BulletType bulletType)
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());
    }

    //다른 물체와의 충돌은 layer로 막아놓음
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (bulletType)
        {
            case BulletType.PlayerBullet:
                {
                    if (collision.gameObject.CompareTag("Enemy"))
                    {
                        MonsterBase monsterBase = collision.gameObject.GetComponent<MonsterBase>();
                        if (monsterBase != null)
                            monsterBase.GetDamage(this.power);
                    }
                }
                break;
            case BulletType.EnemyBullet:
                {
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        CharacterBase characterBase = collision.gameObject.GetComponent<CharacterBase>();
                        if (characterBase != null)
                            characterBase.GetDamage(this.power);
                    }
                }
                break;
        }


   
        //이펙트 호출
        BulletDestroy();
    }

  

    private void BulletDestroy()
    {
        ShowEffect();
        this.gameObject.SetActive(false);
    }

    private void ShowEffect()
    {
        //이펙트 호출
        ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
        effect.Initilaize(this.transform.position, effectName, 0.5f);
    }

}
