using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    private int playTime = 0;
    public bool nowUsingScientistSkill = false;
    public float slowRatio = 0f;
    
    public int PlayTime
    {
        get { return playTime; }
    }
    private IEnumerator CalculatePlayTime()
    {
        WaitForSeconds ws = new WaitForSeconds(1.0f);
        while (true)
        {
            playTime++;
            yield return ws;
        }
        
    }

    private void Start()
    {
        StartCoroutine(CalculatePlayTime());
    }

    private void Awake()
    {
        Instance = this;
    }

    public void BulletTimeOn(float slowRatio)
    {
        Time.timeScale = slowRatio;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        nowUsingScientistSkill = true;
        this.slowRatio = slowRatio;
    }

    public void BulletTimeOff()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        nowUsingScientistSkill = false;
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        if (nowUsingScientistSkill == true)
        {
            BulletTimeOn(slowRatio);
        }
        else if (nowUsingScientistSkill == false)
        {
            Time.timeScale = 1f;
        }
  
    }






}
