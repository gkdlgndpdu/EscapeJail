using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    private int playTime = 0;
    public int PlayTime
    {
        get { return playTime; }
    }
    private IEnumerator CalculatePlayTime()
    {
        while(true)
        {
            playTime++;
            yield return new WaitForSeconds(1.0f);
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
    }

    public void BulletTimeOff()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }






}
