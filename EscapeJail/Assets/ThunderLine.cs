using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderLine : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer thunderSprite;

    [SerializeField]
    private SpriteRenderer thunderBloomSprite;

    float eachPointDistance = 0f;

    private Transform startTarget;
    private Transform endTarget;
    private float scale = 1f;
    private void Update()
    {
        UpdateThunder();
    }

    private void UpdateThunder()
    {

        if (startTarget == null || endTarget == null) return;      

        eachPointDistance = Vector3.Distance(startTarget.position, endTarget.position);
        this.transform.position = startTarget.position + (endTarget.position - startTarget.position).normalized * eachPointDistance * 0.5f;
        this.transform.rotation = Quaternion.Euler(0f, 0f, MyUtils.GetAngle(startTarget.position, endTarget.position));
        this.transform.localScale = new Vector3(1f, 1f, scale);
        SetThundertLength();
    }

    public void Initialize(Transform startTarget, Transform endTarget, Color color = default(Color), float lifeTime =1f,float scale =0.5f)
    {
        eachPointDistance = Vector3.Distance(startTarget.position, endTarget.position);
        this.transform.position = startTarget.position + (endTarget.position - startTarget.position).normalized * eachPointDistance * 0.5f;
        this.transform.rotation = Quaternion.Euler(0f, 0f, MyUtils.GetAngle(startTarget.position, endTarget.position));
        this.transform.localScale = new Vector3(1f, 1f, scale);
        SetThundertLength();

        this.startTarget = startTarget;
        this.endTarget = endTarget;
        this.scale = scale;
        StartCoroutine(StopRoutine(lifeTime));

        if (color != default(Color))
            thunderSprite.color = color;
        else
            thunderSprite.color = Color.white;

        SetBloom(true);
    }
    public void SetBloom(bool OnOff,Color color = default(Color))
    {
        if (OnOff == true)
        {
            if (thunderBloomSprite != null)
            {
                thunderBloomSprite.gameObject.SetActive(true);

                if (color != default(Color))                
                    thunderBloomSprite.color = color;                
                else
                    thunderBloomSprite.color = Color.white;
            }
        }
        else if(OnOff==false)
        {
            if (thunderBloomSprite != null)
                thunderBloomSprite.gameObject.SetActive(false);
        }
    }

    private void SetThundertLength()
    {
        if (thunderSprite == null || thunderBloomSprite == null) return;

        thunderSprite.size = new Vector2( eachPointDistance, 0.64f);
        thunderBloomSprite.size = new Vector2( eachPointDistance, 0.64f);        
    }

    private IEnumerator StopRoutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        OffEffect();

    }

    private void OffEffect()
    {
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

}
