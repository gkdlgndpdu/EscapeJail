using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MinimapBackGround : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetScale(Vector3 scale)
    {
        if (spriteRenderer == null) return;
        spriteRenderer.size = scale;
    }

}
