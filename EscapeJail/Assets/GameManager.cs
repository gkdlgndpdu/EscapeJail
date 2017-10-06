using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

	// Use this for initialization

	public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
    
}   
