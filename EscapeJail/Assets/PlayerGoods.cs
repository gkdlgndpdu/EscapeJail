using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoods : MonoBehaviour
{
    public int GetCurrentMedal()
    {
        return PlayerPrefs.GetInt(GoodsType.Medal.ToString(), 0);
    }

    public void UseMedals(int num)
    {
        int prefMedal = GetCurrentMedal();
        prefMedal -= num;
        prefMedal = Mathf.Clamp(prefMedal, 0, 10000);
        PlayerPrefs.SetInt(GoodsType.Medal.ToString(), prefMedal);

    } 

    

    
}
