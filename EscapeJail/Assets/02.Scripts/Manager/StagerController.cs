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

    public StageData stageData;
 
    private int nowStageLevel = 1;
  

    public int NowStageLevel
    {
        get
        {
            return nowStageLevel - 1;
        }
    }


    public void LoadstageData()
    {
        Object obj = Resources.Load("StageData/Stage" + nowStageLevel.ToString());
        if (obj != null)
            stageData = (StageData)obj;


        nowStageLevel++;
    }


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

       
    }

    public void CreateNextStage()
    {
        //stage data 갱신
        LoadstageData();
        //monsterPool갱신
        ObjectManager.Instance.MakeMonsterPool();      
        mapManager.MakeMap(stageData);
    }

}
