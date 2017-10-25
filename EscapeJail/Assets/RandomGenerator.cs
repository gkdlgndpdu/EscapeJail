using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomGenerator <T> 
{
    private List<T> allItemList = new List<T>();


    public void RemoveInList(T data)
    {
        if (allItemList == null) return;
        //이건왜안될까
        // allItemList.RemoveAll((item) => (item == data));
        // allItemList.RemoveAll(x => x.Equals(data));
        allItemList.RemoveAll(x => EqualityComparer<T>.Default.Equals(x, data));


    }

    public void AddToList(T data,int num)
    {
        if (allItemList == null) return;
        for(int i = 0; i < num; i++)
        {
            allItemList.Add(data);
        }
    }

    public T GetRandomData()
    {
        if (allItemList == null) return default(T);
        return allItemList[UnityEngine.Random.Range(0, allItemList.Count)];
    }




}
