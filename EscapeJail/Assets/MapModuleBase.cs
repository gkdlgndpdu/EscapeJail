using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MapModuleBase : MonoBehaviour
{
    //관리받게될 매니저
    protected MapManager mapManager;

    //컴포넌트
    public BoxCollider2D boxcollider2D;

    //상태
    protected MapState mapState = MapState.UnLock;
    protected bool isClear = false;


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
    protected float eachModuleDistance = 1.28f;

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


    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (isStartModule == true) return;
        if (collision.CompareTag("MapModule"))
        {
            MapModule anotherModule = collision.gameObject.GetComponent<MapModule>();

            //겹치는거 예외처리
            if (eachModuleDistance <= widthDistance * 2f)
            {
                eachModuleDistance = widthDistance * 2f;
            }          

            if (anotherModule != null)
            {
                if (mapManager != null)
                    mapManager.ResetMakeCount();

                if (this.transform.position.x < collision.bounds.center.x)
                {
                    this.transform.position -= Vector3.right * eachModuleDistance;
                }
                else if (this.transform.position.x >= collision.bounds.center.x)
                {
                    this.transform.position += Vector3.right * eachModuleDistance;
                }

                if (this.transform.position.y < collision.bounds.center.y)
                {
                    this.transform.position -= Vector3.up * eachModuleDistance;
                }
                else if (this.transform.position.y >= collision.bounds.center.y)
                {
                    this.transform.position += Vector3.up * eachModuleDistance;
                }
            }
        }
    }
}
