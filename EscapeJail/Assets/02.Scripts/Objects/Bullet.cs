using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//레이어이름과같음 이걸로 충돌 레이어 구분
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

public enum BulletDestroyAction
{
    none,
    aroundFire
}


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    protected int power = 0;
    protected BulletType bulletType;
    public BulletType BulletType
    {
        get
        {
            return bulletType;
        }
    }
    protected Rigidbody2D rb;
    protected string effectName = "revolver";
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Sprite defaultSprite;   
    protected float effectsize = 1f;
    protected float explosionRadius = 1f;
    protected ExplosionType explosionType;
    protected BulletDestroyAction bulletDestroyAction = BulletDestroyAction.none;
    private float bulletSpeed = 0f;
    //충돌에 의해 파괴 가능? 왠만하면 true, 수류탄 다이나마이트같이 시간 기다려주는것들 제외
    private bool canDestroyByCollision = true;
    private bool stopWhenCollision = false; 

    private bool hasPollute = false;
    private CharacterCondition polluteType;

    //폭발시 실행할 함수
    public Action<Vector3> explostionEndFunc;

    public void SetExplosionEndFunc(Action<Vector3> linkFunc)
    {
        explostionEndFunc = linkFunc;
    }

    public void SetMoveLifetime(float time)
    {
        StartCoroutine(bulletMoveLifeCount(time));
    }

    public void SetPollute(CharacterCondition polluteType)
    {
        hasPollute = true;
        this.polluteType = polluteType;
    }

    [SerializeField]
    private SpriteRenderer bloomSprite;
  

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            defaultSprite = spriteRenderer.sprite;


    }


    /// <summary>
    /// 총알 초기화함수
    /// </summary>
    /// <param name="startPos">처음위치</param>
    /// <param name="moveDir">발사방향</param>
    /// <param name="moveSpeed">총알속도</param>
    /// <param name="bulletType"></param>
    /// <param name="bulletScale"></param>
    /// <param name="power"></param>
    /// <param name="lifeTime"></param>
    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, float bulletScale = 1f, int power = 1, float lifeTime = 5f)
    {
        //위치
        this.transform.position = new Vector3(startPos.x, startPos.y, 0f);

        //이동
        if (rb != null)
        {
            bulletSpeed = moveSpeed;
            rb.velocity = moveDir.normalized * moveSpeed;
        }

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
                    SetBloom(true, Color.yellow);
                }
                break;
        }

        StartCoroutine(bulletDestroyRoutine(lifeTime));

        bulletDestroyAction = BulletDestroyAction.none;
        canDestroyByCollision = true;      
        stopWhenCollision = false;
        explostionEndFunc = null;
        hasPollute = false;
    }
    public void SetDestroyByCollision(bool canDestroyByCollision, bool stopWhenCollision = true)
    {
        this.canDestroyByCollision = canDestroyByCollision;
           this.stopWhenCollision = stopWhenCollision;
    }

    private void OnDisable()
    {
      StopAllCoroutines();
    }

    public void SetBulletDestroyAction(BulletDestroyAction action)
    {
        bulletDestroyAction = action;
     
    }

    IEnumerator bulletDestroyRoutine(float time)
    { 
        yield return new WaitForSeconds(time);
        BulletDestroy();
    }

    IEnumerator bulletMoveLifeCount(float time)
    {
        yield return new WaitForSeconds(time);
        StopBullet();
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
            RuntimeAnimatorController animController = Resources.Load<RuntimeAnimatorController>(string.Format("Animators/Bullet/{0}", bulletImageName));
            if (animController != null)
            {
                animator.runtimeAnimatorController = animController;
            }
        }
        else if (isAnimBullet == false && spriteRenderer != null)
        {      
            if (animator != null)
                animator.runtimeAnimatorController = null;

            Sprite sprite = Resources.Load<Sprite>(string.Format("Sprites/Bullet/{0}", bulletImageName));
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

            if (hasPollute == false)
            {
                if (characterInfo != null)
                    characterInfo.GetDamage(this.power);
            }
            else if(hasPollute == true)
            {
                switch (polluteType)
                {
                    case CharacterCondition.InFire:
                        {
                            if (characterInfo != null)
                                characterInfo.SetFire();
                        }
                        break;
                    case CharacterCondition.InPoison:
                        {
                            if (characterInfo != null)
                                characterInfo.SetPoison();
                        }
                        break;
                }
      
            }

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
        //이펙트 호출
        if (canDestroyByCollision == true)
        {
            BulletDestroy();
        }
        else if (canDestroyByCollision == false&&stopWhenCollision==true)
        {
            if (collision.gameObject.CompareTag("Tile"))
                StopBullet();          
        }

        if (explosionType == ExplosionType.single)
            SingleTargetDamage(collision);


        if (collision.gameObject.CompareTag("ItemTable"))
        {
            DamegeToItemTable(collision);
        }


    }

    private void StopBullet()
    {
        if (rb != null)
            rb.velocity = Vector3.zero;
    }


    public void BulletDestroy()
    {
        if (explosionType == ExplosionType.multiple)
            MultiTargetDamage();

        if (explostionEndFunc != null)
        {
            explostionEndFunc(this.transform.position);
            explostionEndFunc = null;
        }

        ShowEffect();
        BulletDestroyActionExcute();
        this.gameObject.SetActive(false);
    }

    protected void BulletDestroyActionExcute()
    {
        switch (bulletDestroyAction)
        {
            case BulletDestroyAction.none: { } break;
            case BulletDestroyAction.aroundFire:
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                        if (bullet != null)
                        {
                            Vector3 fireDIr = Quaternion.Euler(new Vector3(0f, 0f, i * 30)) * Vector3.right;
                            bullet.gameObject.SetActive(true);
                            bullet.Initialize(this.transform.position + fireDIr * 0.1f, fireDIr.normalized, bulletSpeed, bulletType);
                            bullet.InitializeImage("white", false);
                            bullet.SetEffectName("revolver");
                        }
                    }
                }
                break;
        }
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
    //꼼수
    public void SetBloom(bool OnOff)
    {
        SetBloom(OnOff, Color.white);
    }
}
