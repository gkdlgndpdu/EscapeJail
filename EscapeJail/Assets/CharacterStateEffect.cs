using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterStateEffect : MonoBehaviour
{

    private float lifeTime = 0f;
    private float count = 0f;
    private Transform target;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void Initialize(float lifeTime, float size, Transform target, SpecialBulletType specialbulletType)
    {
        this.target = target;
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
                }
                break;
            case SpecialBulletType.Poision:
                {
                    if (spriteRenderer != null)
                        spriteRenderer.color = new Color(0f, 1f, 0f, 0.7f);
                }
                break;
        }



    }

    public void CountReset()
    {
        count = 0f;
    }

    private void Update()
    {
        if (target != null)
        {
            if (target.gameObject.activeSelf == true)
                this.transform.position = target.transform.position;
            else if (target.gameObject.activeSelf == false)
            {
                target = null;
                EffectOff();
            }
        }

        count += Time.deltaTime;
        if (count >= lifeTime)
        {
            EffectOff();
        }
    }

    public void EffectOff()
    {
        CountReset();  
        gameObject.SetActive(false);
        target = null;
    }   


}
