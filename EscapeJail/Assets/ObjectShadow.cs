using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class ObjectShadow : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }

    public void SetObjectShadow(Sprite sprite,int sortingOrder)
    {
        if(spriteRenderer==null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.color = new Color(0f, 0f, 0f, 0.5f);
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.sprite = sprite;
        }
    }

    public void ShadowOff()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }

}
