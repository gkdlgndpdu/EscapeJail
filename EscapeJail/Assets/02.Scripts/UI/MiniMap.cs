using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniMapState
{
    Origin,
    Center
}

public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance;
    [SerializeField]
    private MinimapBackGround backGround;

    private MiniMapState miniMapState = MiniMapState.Origin;
    private float moveSpeed = 3f;

    [SerializeField]
    private Transform iconsParent;

    [SerializeField]
    private Transform centerPosit;

    [SerializeField]
    private Transform originPosit;

    [SerializeField]
    private Transform maskTr;

    private Vector3 maskOriginSize;
    private Vector3 maskMaxSize = new Vector3(10f, 10f, 10f);

    private List<GameObject> objectList = new List<GameObject>();

    private GameObject mapIconPrefab;
    private GameObject playerIconPrefab;

    [SerializeField]
    private float realRatio;

    Transform target;

    Vector3 prefPosit;

    private MiniMap_PlayerIcon playerIcon;

    private int mapModuleLayerMask;

    [SerializeField]
    private Transform allParent;

    public void ChangeMiniMapMode()
    {
        count = 0f;
        if (miniMapState == MiniMapState.Origin)
        {
            iTween.ScaleTo(maskTr.gameObject, maskMaxSize, 0.5f);
            miniMapState = MiniMapState.Center;
      
        }
        else if(miniMapState == MiniMapState.Center)
        {
            iTween.ScaleTo(maskTr.gameObject, maskOriginSize, 0.5f);
            miniMapState = MiniMapState.Origin;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Instance = null;
            Instance = this;

        }
  
        LoadPrefab();

        iconsParent.transform.localScale = Vector3.one * realRatio;

        maskOriginSize = maskTr.localScale;

        mapModuleLayerMask = MyUtils.GetLayerMaskByString("MapModuleIcon");
    }

    public void ResetMiniMap()
    {
        if (objectList != null)
        {
            foreach (GameObject obj in objectList)
            {
                Destroy(obj);
            }
            objectList.Clear();
        }

        //  this.transform.localPosition = Vector3.zero;

    }

    public void SetBackGroundPosit(Vector3 posit)
    {
        if (backGround == null) return;
        backGround.gameObject.transform.localPosition = posit;
    }

    private void LoadPrefab()
    {
        mapIconPrefab = Resources.Load<GameObject>("Prefabs/Maps/MiniMap/MapModuleIcon");
        playerIconPrefab = Resources.Load<GameObject>("Prefabs/Maps/MiniMap/PlayerIcon");

    }

    public MiniMap_MapIcon MakeRoomIcon(Vector3 position, Vector3 scale, bool hasPortal = false,MapModuleBase linkModule = null)
    {
        GameObject obj = Instantiate(mapIconPrefab, iconsParent);
        if (obj != null)
        {
            obj.transform.localPosition = position;
            obj.transform.localScale = scale;
            MiniMap_MapIcon icon = obj.GetComponent<MiniMap_MapIcon>();
            icon.SetPortal(hasPortal, linkModule);
            objectList.Add(icon.gameObject);
            return icon;
        }

        return null;

    }

    public void MakePlayerIcon()
    {
        GameObject obj = Instantiate(playerIconPrefab, iconsParent);

        if (obj != null)
        {
            MiniMap_PlayerIcon icon = obj.GetComponent<MiniMap_PlayerIcon>();
            Transform playerTr = GamePlayerManager.Instance.player.transform;
            if (icon != null)
                icon.LinkPlayer(playerTr);

            target = playerTr;
            prefPosit = target.transform.localPosition;

            playerIcon = icon;
        }
    }

    public void SetBackGroundScale(Vector3 scale)
    {
        backGround.SetScale(scale);
        //   backGround.transform.localScale = scale;
    }

    // Use this for initialization
    void Start()
    {
        MakePlayerIcon();

    }

    float count = 0f;

    private void Update()
    {
 

        if (target == null) return;
        //if (count > 1f) return;

        if (miniMapState == MiniMapState.Origin)
        {
            count += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position,originPosit.transform.position , count);
            maskTr.localScale = Vector3.Lerp(maskTr.localScale, maskOriginSize, count);
            if (target.position != prefPosit)
            {
                float x = prefPosit.x - target.position.x;
                float y = prefPosit.y - target.position.y;

                //   iconsParent.localPosition += new Vector3(x*realRatio, y*realRatio, 0f);
                iconsParent.localPosition = new Vector3(-target.position.x * realRatio, -target.position.y * realRatio, 0f);         

           
            }
        }
        else if (miniMapState == MiniMapState.Center)
        {
            count += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, centerPosit.transform.position, count);
            maskTr.localScale = Vector3.Lerp(maskTr.localScale, maskMaxSize, count);
            iconsParent.localPosition = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
#if UNITY_EDITOR

                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D rayHit = Physics2D.Raycast(touchPos, Vector2.zero, 0.1f, mapModuleLayerMask);
                if (rayHit.collider != null)
                {
                    MiniMap_MapIcon target = rayHit.collider.gameObject.GetComponent<MiniMap_MapIcon>();

                    if (target != null)
                    {
                        if (target.HasProtal == true&&target.CanUsePortal==true)
                        {
                            if(target.LinkModule!=null)                        
                            GamePlayerManager.Instance.player.transform.position = target.LinkModule.transform.position;

                        }
                    }
                }
#else
      Touch[] touches = Input.touches;
            if (touches != null)
            {
                for (int i = 0; i < touches.Length; i++)
                {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touches[i].position);
                    RaycastHit2D rayHit = Physics2D.Raycast(touchPos, Vector2.zero, 0.1f, mapModuleLayerMask);
                    if (rayHit.collider != null)
                    {
                        MiniMap_MapIcon target = rayHit.collider.gameObject.GetComponent<MiniMap_MapIcon>();

                    if (target != null)
                    {
                        if (target.HasProtal == true&&target.CanUsePortal==true)
                        {
                            if(target.LinkModule!=null)                        
                            GamePlayerManager.Instance.player.transform.position = target.LinkModule.transform.position;
                            break;
                        }
                    }
                    }
                }
            }
#endif
            }

        }





    }
}




