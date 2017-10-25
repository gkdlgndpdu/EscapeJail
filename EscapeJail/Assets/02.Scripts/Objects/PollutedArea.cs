using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PollutedArea : MonoBehaviour
{
    private float pollutedTime = 100f;
    private float count = 0f;
    private CharacterCondition polluteType;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<Animator> effectList = new List<Animator>();
       

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Vector3 position, float pollutedTime, float size, CharacterCondition polluteType, BulletType bulletType)
    {
        this.pollutedTime = pollutedTime;
        this.transform.localScale = Vector3.one * size;
        this.transform.position = position;
        this.polluteType = polluteType;
        SetPollute(polluteType);

        SetLayer(bulletType);
        ChangeEffectsAnimation(polluteType);
        RandomizeEffectsSpeed();
    }
    protected void SetLayer(BulletType bulletType)
    {
        this.gameObject.layer = LayerMask.NameToLayer(bulletType.ToString());
    }
    private void RandomizeEffectsSpeed()
    {
        if (effectList == null) return;
        for(int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i] != null)
                effectList[i].speed = Random.Range(1f, 2f);
        }

    }

    private void ChangeEffectsAnimation(CharacterCondition polluteType)
    {
        string path = string.Format("Animators/Effect/{0}", polluteType.ToString());
        RuntimeAnimatorController animController = Resources.Load<RuntimeAnimatorController>(path);

        if (effectList == null) return;
        for(int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i] != null)
                effectList[i].runtimeAnimatorController = animController;
        }

    }

    private void SetPollute(CharacterCondition polluteType)
    {
        switch (polluteType)
        {
            case CharacterCondition.InFire:
                {
                    SetColor(new Color(1f, 0f, 0f, 0.4f));
                }
                break;
            case CharacterCondition.InPoison:
                {
                    SetColor(new Color(0f, 1f, 0f, 0.4f));
                }
                break;
        }
    }

    private void SetColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }


    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count > pollutedTime)
        {
     
            AreaOff();
        }
    }

    void AreaOff()
    {
        gameObject.SetActive(false);
        count = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();
        if (characterInfo != null)
        {
            switch (polluteType)
            {
                case CharacterCondition.InFire:
                    {
                        characterInfo.SetFire();
                    }
                    break;
                case CharacterCondition.InPoison:
                    {
                        characterInfo.SetPoison();
                    }
                    break;
            }


     
        }
    }
}
