using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagerController : MonoBehaviour
{

    public static StagerController Instance;
    [SerializeField]
    private MapManager mapManager;
    [SerializeField]
    private ItemSpawner itemSpawner;

    //생성정보
    private StageData stageData;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        CreateNextStage();
    }

    //맵,오브젝트,몬스터풀정도 삭제
    public void DestroyThisStage()
    {
        if (mapManager != null)
            mapManager.DestroyEveryMapModule();

        if (itemSpawner != null)
            itemSpawner.DestroyAllItems();

        System.GC.Collect();
    }

    public void CreateNextStage()
    {
        //stage data 갱신
        GameOption.Instance.LoadstageData();

        //monsterPool갱신
        ObjectManager.Instance.MakeMonsterPool();

        stageData = GameOption.Instance.StageData;
        mapManager.MakeMap(stageData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            CreateNextStage();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DestroyThisStage();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            System.GC.Collect();
        }
    }
}
