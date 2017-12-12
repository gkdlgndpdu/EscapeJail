using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Loading,
    Start,
    Pause
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    void Awake()
    {    
      Instance = this;
    }    



    public void ChangeStage()
    {
        StartCoroutine(StageChangeRoutine());
    }

    private IEnumerator StageChangeRoutine()
    {
        LoadingBoard.Instance.LoadingStart();
        yield return new WaitForSeconds(1.0f);
        TemporaryObjects.Instance.DestroyAllChildrenObject();
        MiniMap.Instance.ResetMiniMap();
        MonsterManager.Instance.ClearMonsterList();
       // yield return new WaitForSeconds(0.5f);
        StagerController.Instance.DestroyThisStage();
       // yield return new WaitForSeconds(0.5f);
        StagerController.Instance.CreateNextStage();
       // yield return new WaitForSeconds(0.5f);
        GamePlayerManager.Instance.ResetPlayerPosit();
        System.GC.Collect();

    }
    
}   
