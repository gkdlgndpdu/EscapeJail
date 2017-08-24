using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Normal,
    Wall,
    Object
}

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{    
    public TileType tileType;
    private SpriteRenderer spriteRenderer;
    public int x;
    public int y;

    public void SetIndex(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ChangeColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }
}
