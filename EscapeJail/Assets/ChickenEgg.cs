using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class ChickenEgg : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float explosionRadius = 1.5f;
    private int power = 3;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.speed = 0f;

        SetLayer();
    }
    private void Start()
    {
        StartCoroutine(EggAutoDestroyRoutine());
    }

    private void SetLayer()
    {
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = GameConstants.ArticleLayerMin;
    }

    private IEnumerator EggAutoDestroyRoutine()
    {
        float animationFrameCount = -1f;
        for (int i = 0; i < 6; i++)
        {
            animationFrameCount += 1f;
            animator.Play("ChickenEggAnim", 0, (1f / 6f) * animationFrameCount);
            if (animationFrameCount >= 5) break;
            yield return new WaitForSeconds(1f);                 
        }

        ExplosionEgg();
    }

    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExplosionEgg();
    }

    private void ExplosionEgg()
    {
        int layerMask = MyUtils.GetLayerMaskByString("Enemy");


        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius, layerMask);
        if (colls == null) return;

        for (int i = 0; i < colls.Length; i++)
        {
            CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
            if (characterInfo != null)
                characterInfo.GetDamage(power);
        }

        //이펙트        
        ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
        if (effect != null)
            effect.Initilaize(this.transform.position, "bazooka", 0.5f, explosionRadius * 2f);

        Destroy(this.gameObject);
    }

}
