using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpecialBulletType
{
    FrameThrower,
}


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SpecialBullet : MonoBehaviour
{
    private int power = 0;
    private BulletType bulletType;
    private Rigidbody2D rb;
    private string effectName = "revolver";
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    private float lifeTime = 1.0f;
    private float effectsize = 0f;
    private float explosionRadius = 1f;
    private ExplosionType explosionType;
    private SpecialBulletType specialBulletType;
    float expireCount = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            defaultSprite = spriteRenderer.sprite;


    }
    private void SetLayer(BulletType bulletType)
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());
    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, SpecialBulletType specialBulletType, float bulletScale = 1f, int power = 1, float lifeTime = 5f)
    {
        //위치
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);

        //이동
        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

        float RotateAngle = MyUtils.GetAngle(Vector3.zero, moveDir) + 180f;

        //회전
        this.transform.rotation = Quaternion.Euler(0f, 0f, RotateAngle);


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

        this.specialBulletType = specialBulletType;
        SetBulletImage(this.specialBulletType);

    }

    private void SetBulletImage(SpecialBulletType specialBulletType)
    {
        switch (specialBulletType)
        {
            case SpecialBulletType.FrameThrower:
                {
                    InitializeImage(specialBulletType.ToString(), true);
                }
                break;
        }
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


    private void SingleTargetDamage(Collider2D collision)
    {
        //충돌여부는 layer collision matrix로 분리해놓음
        if (collision.gameObject.CompareTag("Enemy") == true || collision.gameObject.CompareTag("Player"))
        {
            CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();

            if (characterInfo != null)
                characterInfo.GetDamage(this.power);
        }
    }

    private void FrameThrowerDamage(Collider2D collision)
    {
        //충돌여부는 layer collision matrix로 분리해놓음
        if (collision.gameObject.CompareTag("Enemy") == true || collision.gameObject.CompareTag("Player"))
        {
            CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();

            if (characterInfo != null)
            {
                characterInfo.GetDamage(this.power);
                characterInfo.SetFire();
            }
        }
    }

    private void MultiTargetDamage()
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

    public void Update()
    {
        expireCount += Time.deltaTime;
        if (expireCount >= lifeTime)
        {
            BulletDestroy();
        }
    }

    private void DamegeToItemTable(Collider2D collision)
    {
        ItemTable table = collision.gameObject.GetComponent<ItemTable>();
        if (table != null)
        {
            if (specialBulletType == SpecialBulletType.FrameThrower)
                table.GetDamage(power * 5);
            else
                table.GetDamage(power);

        }
    }

    //다른 물체와의 충돌은 layer로 막아놓음
    private void OnTriggerEnter2D(Collider2D collision)
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


        if (specialBulletType == SpecialBulletType.FrameThrower)
        {
            FrameThrowerDamage(collision);
        }
        else
        {
            ////총알 삭제 및 이펙트 호ㅓ출
            BulletDestroy();
        }

    }






    private void BulletDestroy()
    {
        if (explosionType == ExplosionType.multiple)
            MultiTargetDamage();

        expireCount = 0f;
        ShowEffect();
        this.gameObject.SetActive(false);
    }

    private void ShowEffect()
    {
        //이펙트 호출
        ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
        if (effect != null)
            effect.Initilaize(this.transform.position, effectName, 0.5f, effectsize);
    }



}
