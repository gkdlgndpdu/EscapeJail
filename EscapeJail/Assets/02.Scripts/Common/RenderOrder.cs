using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RenderOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    } 

    private void OnEnable()
    {
        RenderController.Instance.AddToRenderList(this);
    }

    private void OnDisable()
    {
        RenderController.Instance.RemoveInRenderList(this);
    }

    public void SetOrder(int orderNum)
    {
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = orderNum;
    }

  
}
