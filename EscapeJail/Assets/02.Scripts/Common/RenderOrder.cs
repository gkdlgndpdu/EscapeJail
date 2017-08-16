using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RenderOrder : MonoBehaviour
{
    private SpriteRenderer renderer;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        RenderController.Instance.AddToRenderList(this);
    }

    private void OnDisable()
    {
        RenderController.Instance.RemoveInRenderList(this);
    }

    public void SetOrder(int orderNum)
    {
        if (renderer != null)
            renderer.sortingOrder = orderNum;
    }

  
}
