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

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public TileType tileType;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private MapModule parentModule=null;

    //첫번째 모듈에서는 문이 안닫히도록
    private bool isStartModule = false;

    public int x;
    public int y;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(TileType tileType,MapModule parentModule =null, bool isStartModule = false)
    {
        if (tileType == TileType.Wall)
        {
            collider = this.gameObject.AddComponent<BoxCollider2D>();
        }

        if(tileType == TileType.Door)
        {
            this.parentModule = parentModule;
            collider = this.gameObject.AddComponent<BoxCollider2D>();
            OpenDoor();
        }

        this.isStartModule = isStartModule;
        this.tileType = tileType;
        this.x = x;
        this.y = y;
    }

    public void OpenDoor()
    {
        if (collider == null) return; 
        collider.isTrigger = true;

        //열리는 애니메이션
        ChangeColor(Color.green);
    }

    public void CloseDoor()
    {
        if (collider == null) return;
        collider.isTrigger = false;

        //닫히는 애니메이션 
        ChangeColor(Color.red);
 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (parentModule != null&& isStartModule == false)
                parentModule.StartWave();
        }
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
