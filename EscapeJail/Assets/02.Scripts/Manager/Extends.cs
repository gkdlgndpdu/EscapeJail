using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 확장메소드는 여기에
public static class Extends
{
    public static void ListShuffle<T>(this List<T> list,int ShuffleNum =30)
    {
        if (list.Count < 2) return;

        for(int i = 0; i < ShuffleNum; i++)
        {
            int rand1 = Random.Range(0, list.Count);
            int rand2 = Random.Range(0, list.Count);

            T randValue = list[rand1];
            list[rand1] = list[rand2];
            list[rand2] = randValue;

        }         
    }
}
