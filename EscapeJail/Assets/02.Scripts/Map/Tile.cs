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
[RequireComponent(typeof(BoxCollider2D))]
public class Tile : MonoBehaviour
{
    public TileType tileType;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BoxCollider2D boxCollider;
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
        SetMyParentPool();
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

    public void Initialize(TileType tileType, MapModuleBase parentModule, int layerOrder = 0)
    {
        SetLayerOrder(layerOrder);

        this.parentModule = parentModule;
        this.tileType = tileType;
        ChangeColor(Color.white);

        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();

        }

        if (tileType == TileType.Wall)
        {

            if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }

            if (spriteRenderer != null)
                spriteRenderer.sortingOrder = GameConstants.WallLayerMin;

            if (objectShadow != null)
                objectShadow.SetObjectShadow(spriteRenderer.sprite, GameConstants.WallLayerMin - 1);

            if (objectShadow != null)
                objectShadow.gameObject.SetActive(true);

            return;
        }


        if (tileType == TileType.Door)
        {         
            ChangeColor(Color.green);
            OpenDoor();
        }

        if (tileType == TileType.Normal)
        {
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }



        if (objectShadow != null)
            objectShadow.gameObject.SetActive(false);


    }

    private void SetLayerOrder(int layer)
    {
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = layer;
    }


    public void OpenDoor()
    {
        if (tileType != TileType.Door) return;


        if (boxCollider != null)
        {
            boxCollider.enabled = false;

            //열리는 애니메이션
            ChangeColor(Color.green);
        }

    }

    public void CloseDoor()
    {
        if (tileType != TileType.Door) return;


        if (boxCollider != null)
        {
            boxCollider.enabled = true;

            //닫히는 애니메이션 
            ChangeColor(Color.red);
        }

    }

    public void ChangeColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }
   
   

    public FastObjectPool<Tile> myPool;
    public void SetMyParentPool()
    {
        this.myPool = ObjectManager.Instance.tilePool;
    }

    public void PullToParentPool()
    {
        if (myPool == null)
            SetMyParentPool();
        if (myPool != null)
        {
            this.gameObject.SetActive(false);
            myPool.PushUseEndObject(this);
        }
    }

}
