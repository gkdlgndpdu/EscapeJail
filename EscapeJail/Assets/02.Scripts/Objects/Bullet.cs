using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//레이어이름과같음
public enum BulletType
{
    PlayerBullet,
    EnemyBullet
}

public enum ExplosionType
{
    single,
    multiple
}


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    protected int power = 0;
    protected BulletType bulletType;
    protected Rigidbody2D rb;
    protected string effectName = "revolver";
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Sprite defaultSprite;
    protected float lifeTime = 1.0f;
    protected float effectsize = 1f;
    protected float explosionRadius = 1f;
    protected ExplosionType explosionType;

    [SerializeField]
    private SpriteRenderer bloomSprite;

    float expireCount = 0f;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            defaultSprite = spriteRenderer.sprite;


    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, float bulletScale = 1f, int power = 1, float lifeTime = 5f)
    {
        //위치
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);

        //이동
        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

        //피아식별
        this.bulletType = bulletType;
        //레이어
        SetLayer(bulletType);
        //크기
        this.transform.localScale = Vector3.one * bulletScale;
        //파워
        this.power = power;

        //애니메이션불렛 유무
        if (animator != null)
            animator.runtimeAnimatorController = null;

        this.lifeTime = lifeTime;

        //폭발 타입
        explosionType = ExplosionType.single;

        switch (bulletType)
        {
            case BulletType.EnemyBullet:
                {
                    //bloom
                    SetBloom(true, Color.red);
                }
                break;
            case BulletType.PlayerBullet:
                {
                    //bloom
                    SetBloom(true, Color.green);
                }
                break;
        }

    }
    private void OnDisable()
    {
        expireCount = 0f;
    }



    public void Update()
    {
        expireCount += Time.deltaTime;
        if (expireCount >= lifeTime)
        {
            BulletDestroy();
        }
    }

    public void SetExplosion(float radius)
    {
        explosionType = ExplosionType.multiple;
        explosionRadius = radius;
    }


    public void SetEffectName(string effectName, float effectsize = 1f)
    {
        this.effectName = effectName;
        this.effectsize = effectsize;
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

    protected void SetLayer(BulletType bulletType)
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());
    }



    protected void SingleTargetDamage(Collider2D collision)
    {
        //충돌여부는 layer collision matrix로 분리해놓음
        if (collision.gameObject.CompareTag("Enemy") == true || collision.gameObject.CompareTag("Player"))
        {
            CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();

            if (characterInfo != null)
                characterInfo.GetDamage(this.power);
        }
    }

    protected void MultiTargetDamage()
    {
        int layerMask;
        if (bulletType == BulletType.PlayerBullet)
            layerMask = MyUtils.GetLayerMaskByString("Enemy");
        else
            layerMask = MyUtils.GetLayerMaskByString("Player");


        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius, layerMask);
        if (colls == null) return;

        for (int i = 0; i < colls.Length; i++)
        {
            CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
            if (characterInfo != null)
                characterInfo.GetDamage(power);
        }

    }

    protected void DamegeToItemTable(Collider2D collision)
    {
        ItemTable table = collision.gameObject.GetComponent<ItemTable>();
        if (table != null)
            table.GetDamage(power);
    }

    //다른 물체와의 충돌은 layer로 막아놓음
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        switch (explosionType)
        {
            case ExplosionType.single:
                {
                    SingleTargetDamage(collision);

                }
                break;
            case ExplosionType.multiple:
                {
                    MultiTargetDamage();
                }
                break;
        }

        if (collision.gameObject.CompareTag("ItemTable"))
        {
            DamegeToItemTable(collision);
        }

        //이펙트 호출
        BulletDestroy();
    }



    protected void BulletDestroy()
    {
        if (explosionType == ExplosionType.multiple)
            MultiTargetDamage();

        expireCount = 0f;
        ShowEffect();
        this.gameObject.SetActive(false);
    }

    protected void ShowEffect()
    {
        //이펙트 호출
        ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
        if (effect != null)
            effect.Initilaize(this.transform.position, effectName, 0.5f, effectsize);
    }


    public void SetBloom(bool OnOff, Color color)
    {
        if (bloomSprite != null)
        {
            bloomSprite.gameObject.SetActive(OnOff);
            if (OnOff == true)
                bloomSprite.color = color;
        }
    }
}
