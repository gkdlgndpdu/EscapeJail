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
    private MapModuleBase parentModule = null;
    public MapModuleBase ParentModule
    {
        get
        {
            return parentModule;
        }
    }
    private ObjectShadow objectShadow;

    public int x;
    public int y;

    public bool canSpawned = true;

    public void SetIndex(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectShadow = GetComponentInChildren<ObjectShadow>();    
        SetLayerAndTag();

    }

    private void Start()
    {
        OpenDoor();
    }

    private void SetLayerAndTag()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Tile");
        this.gameObject.tag = "Tile";
    }

    public void SetSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = sprite;


    }

    public void Initialize(TileType tileType, MapModuleBase parentModule,int layerOrder =0)
    {
        SetLayerOrder(layerOrder);

        this.parentModule = parentModule;

        if (tileType == TileType.Wall)
        {
            collider = this.gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(0.64f, 0.64f);

            if (spriteRenderer != null)
                spriteRenderer.sortingOrder = GameConstants.WallLayerMin;

            if (objectShadow != null)
                objectShadow.SetObjectShadow(spriteRenderer.sprite, GameConstants.WallLayerMin - 1);
        }


        if (tileType == TileType.Door)
        {
           
            collider = this.gameObject.AddComponent<BoxCollider2D>();

            OpenDoor();
        }

        this.tileType = tileType;



    }

    private void SetLayerOrder(int layer)
    {
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = layer;
    }


    public void OpenDoor()
    {
        if (tileType != TileType.Door) return;
        if (collider == null)
            collider = GetComponent<BoxCollider2D>();


        if (collider != null)
        {
            collider.enabled = false;

            //열리는 애니메이션
            ChangeColor(Color.green);
        }

    }

    public void CloseDoor()
    {
        if (tileType != TileType.Door) return;
        if (collider == null)
            collider = GetComponent<BoxCollider2D>();


        if (collider != null)
        {
            collider.enabled = true;

            //닫히는 애니메이션 
            ChangeColor(Color.red);
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
