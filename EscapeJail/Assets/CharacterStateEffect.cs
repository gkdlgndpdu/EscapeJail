using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterStateEffect : MonoBehaviour
{
    
    private float lifeTime = 0f;
    private float count = 0f;
    private Transform originParent;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originParent = this.transform.parent;
    }

    public void Initialize(float lifeTime, float size,Transform target, SpecialBulletType specialbulletType)
    {
        this.transform.parent = target;
        this.lifeTime = lifeTime;
        this.transform.localScale = Vector3.one * size;

        SetEffect(specialbulletType);
    }

    private void SetEffect(SpecialBulletType specialbulletType)
    {
        switch (specialbulletType)
        {
            case SpecialBulletType.Fire:
                {
                    if (spriteRenderer != null)
                        spriteRenderer.color = new Color(1f, 1f, 1f, 0.7f);
                } break;
            case SpecialBulletType.Poision:
                {
                    if (spriteRenderer != null)
                        spriteRenderer.color = new Color(0f, 1f, 0f, 0.7f);
                } break;
        }

  
            
    }

    public void CountReset()
    {
        count = 0f;
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count >= lifeTime)
        {
            EffectOff();
        }
    }

    private void EffectOff()
    {
        this.transform.parent = originParent;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CountReset();


    }


}
