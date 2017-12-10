using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter
{
    //남은골드 점수로 환산
    int nowScore=0;
    public int NowScore
    {
        get { return nowScore; }
    }
    int monsterKillNum=0;
    public int MonsterKillNum
    {
        get { return monsterKillNum; }
    }
    int bossKillNum=0;
    public int BossKillNum
    {
        get { return bossKillNum; }
    }
    int lastStage =1;
    public int LastStage
    {
        get { return lastStage; }
    }
    int clearRoomNum = 0;
    public int ClearRoomNum
    {
        get { return clearRoomNum; }
    }
    int lostHeartNum = 0;
    public int LostHeartNum
    {
        get { return lostHeartNum; }
    }
    int totalDamage = 0;
    public int TotalDamage
    {
        get { return totalDamage; }
    }
    int remainCoin = 0;
    public int RemainCoin
    {
        get { return remainCoin; }
        set { remainCoin = value; }
    }

    int earningMedals = 0;
    public int EarningMedals
    {
        get { return earningMedals; }
        set { earningMedals = value; }
    }


    

    public void KillMonster()
    {
        monsterKillNum++;
    }
    public void KillBoss()
    {
        bossKillNum++;
    }
    public void ClearStage()
    {
        lastStage++;
    }
    public void ClearRoom()
    {
        clearRoomNum++;
    }
    public void GetDamage()
    {
        lostHeartNum++;
    }

    public void HitDamage(int damage)
    {
        totalDamage += damage;  
    }
    
}
