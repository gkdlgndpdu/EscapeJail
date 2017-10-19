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

	public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public void ChangeStage()
    {
        StartCoroutine(StageChangeRoutine());
    }

    private IEnumerator StageChangeRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        LoadingBoard.Instance.LoadingStart();
        yield return new WaitForSeconds(0.5f);
        StagerController.Instance.DestroyThisStage();
        yield return new WaitForSeconds(0.5f);
        StagerController.Instance.CreateNextStage();
        yield return new WaitForSeconds(0.5f);
        GamePlayerManager.Instance.ResetPlayerPosit();

    }
    
}   
