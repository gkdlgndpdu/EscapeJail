using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance;
    [SerializeField]
    private SpriteRenderer backGround;


    private GameObject mapIconPrefab;
    private GameObject playerIconPrefab;

    [SerializeField]
    private float realRatio;

    Transform target;

    Vector3 prefPosit;

    private void Awake()
    {
        Instance = this;
        LoadPrefab();

        this.transform.localScale = Vector3.one * realRatio;
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

    public void MakeRoomIcon(Vector3 position,Vector3 scale)
    {
        GameObject obj = Instantiate(mapIconPrefab, this.transform);
        if (obj != null)
        {
            obj.transform.localPosition = position;
            obj.transform.localScale = scale;
            MiniMap_MapIcon icon = obj.GetComponent<MiniMap_MapIcon>();
        }     

    }

    public void MakePlayerIcon()
    {
        GameObject obj = Instantiate(playerIconPrefab, this.transform);

        if (obj != null)
        {
            MiniMap_PlayerIcon icon = obj.GetComponent<MiniMap_PlayerIcon>();
            Transform playerTr = GamePlayerManager.Instance.player.transform;
            if (icon!=null)
                icon.LinkPlayer(playerTr);

            target = playerTr;
            prefPosit = target.transform.localPosition;
        }
    }

    public void SetBackGroundScale(Vector3 scale)
    {
        backGround.transform.localScale = scale;
    }

    // Use this for initialization
    void Start ()
    {
        MakePlayerIcon();

    }

    private void Update()
    {
        if (target == null) return;

     

      //  float dist = Vector3.Distance(target.position, prefPosit);

        if (target.position != prefPosit)
        {
            float x = prefPosit.x - target.position.x;
            float y = prefPosit.y - target.position.y;

            this.transform.localPosition += new Vector3(x*realRatio, y*realRatio, 0f);
        }


        prefPosit = target.transform.position;


    }


}
