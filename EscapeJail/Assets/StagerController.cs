using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagerController : MonoBehaviour
{
    [SerializeField]
    private MapManager mapManager;
    [SerializeField]
    private ItemSpawner itemSpawner;

    //생성정보
    private StageData stageData;

    private void Awake()
    {
        stageData = GameOption.Instance.StageData;
    }

    //맵,오브젝트,몬스터풀정도 삭제
    private void DestroyThisStage()
    {
        if (mapManager != null)
            mapManager.DestroyEveryMapModule();

        if (itemSpawner != null)
            itemSpawner.DestroyAllItems();
    }

    private void CreateNextStage()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            mapManager.MakeMap(stageData);
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
