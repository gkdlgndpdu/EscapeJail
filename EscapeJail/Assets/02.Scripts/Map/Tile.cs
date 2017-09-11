using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Normal,
    Wall,
    Door,
    Object
}

public enum DoorDirection
{
    Default,
    Up,
    Down,
    Left,
    Right
}

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public TileType tileType;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private MapModule parentModule=null;

    public int x;
    public int y;

    public bool canSpawned = true;
        
 
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();      
    }

    public void SetSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = sprite;

       
    }

    public void Initialize(TileType tileType, Sprite sprite=null,MapModule parentModule =null)
    {
        if (tileType == TileType.Wall)
        {
            collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.64f, 0.64f);


        }

        if (sprite != null)
            SetSprite(sprite);

        if (tileType == TileType.Door)
        {
            this.parentModule = parentModule;
            collider = this.gameObject.AddComponent<BoxCollider2D>();
           
            OpenDoor();
        }    
        this.tileType = tileType;

    }

    public void OpenDoor()
    {
        if (collider == null) return; 
        collider.enabled = false;

        //열리는 애니메이션
        ChangeColor(Color.green);
    }

    public void CloseDoor()
    {
        if (collider == null) return;
        collider.enabled = true;

        //닫히는 애니메이션 
        ChangeColor(Color.red);
 
    }

    public void ChangeColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }

    public void ChangeSprite(string spriteName)
    {

    }
}
