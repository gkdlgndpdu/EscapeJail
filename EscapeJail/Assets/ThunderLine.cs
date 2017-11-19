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


    public void Initialize(Vector3 startPos,Vector3 endPos,Color color = default(Color), float lifeTime =1f,float scale =1f)
    {
        eachPointDistance = Vector3.Distance(startPos, endPos);
        this.transform.position = startPos + (endPos - startPos).normalized * eachPointDistance * 0.5f;
        this.transform.rotation = Quaternion.Euler(0f, 0f, MyUtils.GetAngle(startPos, endPos));
        this.transform.localScale = new Vector3(1f, 1f, scale);
        SetThundertLength();
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
