using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class MiniMap_MapIcon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

	public void SetClear()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.red;
    }

}
