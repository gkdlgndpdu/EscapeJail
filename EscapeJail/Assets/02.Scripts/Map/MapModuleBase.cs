using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MapModuleType
{
    NormalModule,
    BossModule,
    ShopModule
}

[RequireComponent(typeof(BoxCollider2D))]
public class MapModuleBase : MonoBehaviour
{
    //관리받게될 매니저
    protected MapManager mapManager;
    protected MapModuleType moduleType;
    //컴포넌트
    public BoxCollider2D boxcollider2D;

    //상태
    protected MapState mapState = MapState.UnLock;
    protected bool isClear = false;
    protected bool isPositioningComplete = false;

    [SerializeField]
    protected int widthNum;
    [SerializeField]
    protected int heightNum;

    protected bool hasPortal = false;
    public bool HasPortal
    {
        get { return hasPortal; }
    }

    //저장소
    public List<Tile> normalTileList;
    public List<Tile> NormalTileList
    {
        get
        {
            return normalTileList;
        }
    }
    protected List<Tile> wallTileList;
    protected List<Tile> doorTileList;

    protected bool isStartModule = false;
    protected float widthDistance;
    protected float heightDistance;
  //  protected float eachModuleDistance = 1.28f;

    protected void OpenDoor()
    {
        if (doorTileList == null) return;
        for (int i = 0; i < doorTileList.Count; i++)
        {
            doorTileList[i].OpenDoor();
        }
    }

    protected void CloseDoor()
    {
        if (doorTileList == null) return;
        for (int i = 0; i < doorTileList.Count; i++)
        {
            doorTileList[i].CloseDoor();
        }
    }

    public virtual void PositioningComplete()
    {
        isPositioningComplete = true;    
    }

    public virtual void MakeObjects()
    {
    
    }

    public void MakePortal()
    {
        if (moduleType != MapModuleType.NormalModule) return;

        hasPortal = true;

        GameObject portalPrefab = Resources.Load<GameObject>("Prefabs/Articles/Portal");
        if (portalPrefab != null)
        {
            GameObject makeObj = Instantiate(portalPrefab);
            if (makeObj != null)
            {
                Portal portal = makeObj.GetComponent<Portal>();
                if (portal != null)
                {
                    portal.Initialize(this);
                }
            }
        }
    }


    protected void OnTriggerStay2D(Collider2D collision)
    {  
        if (isStartModule == true) return;
        if (collision.CompareTag("MapModule"))
        {
       //     MapModule anotherModule = collision.gameObject.GetComponent<MapModule>();

            //겹치는거 예외처리
            if (GameConstants.eachModuleDistance <= widthDistance * 2f)
            {
                GameConstants.eachModuleDistance = widthDistance * 2f;
            }          

            //if (anotherModule != null)
            //{
                if (mapManager != null)
                    mapManager.ResetMakeCount();
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                if (this.transform.position.x < collision.bounds.center.x)
                {
                    this.transform.position -= Vector3.right * GameConstants.eachModuleDistance;
                }
                else if (this.transform.position.x >= collision.bounds.center.x)
                {
                    this.transform.position += Vector3.right * GameConstants.eachModuleDistance;
                }
            }
            else
            {
                if (this.transform.position.y < collision.bounds.center.y)
                {
                    this.transform.position -= Vector3.up * GameConstants.eachModuleDistance;
                }
                else if (this.transform.position.y >= collision.bounds.center.y)
                {
                    this.transform.position += Vector3.up * GameConstants.eachModuleDistance;
                }

            }

       

              
            //}
        }
    }
}
