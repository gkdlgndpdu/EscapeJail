using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

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
