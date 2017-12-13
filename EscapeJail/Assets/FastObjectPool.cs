using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastObjectPool<T> where T : Component
{
    private Stack<T> objectPool;
    private GameObject prefab;
    private Transform objectParent;

    public FastObjectPool(Transform objectParent, GameObject prefab, int objectNum)
    {
        this.objectPool = new Stack<T>();
        this.prefab = prefab;
        this.objectParent = objectParent;

        for (int i = 0; i < objectNum; i++)
        {
            MakePoolObject();
        }
    }

    public void PushUseEndObject(T data)
    {
        if (objectPool != null)
            objectPool.Push(data);
    }

    private void MakePoolObject()
    {
        GameObject item = GameObject.Instantiate(prefab, objectParent);
        if (item != null)
        {
            item.SetActive(false);
            T obj = item.GetComponent<T>();
            if (obj != null)
            {
                objectPool.Push(obj);
            }
            else if (obj == null)
            {
                GameObject.Destroy(item);
            }
        }
    }

    public void ReleasePool()
    {
        if (objectPool == null) return;

        while (objectPool.Count > 0)
        {
            GameObject.Destroy(objectPool.Pop());
        }

        objectPool.Clear();
        objectPool = null;
    }

    /// <summary>
    /// 리턴값 null체크 필요,SetActive(true)로 반환
    /// </summary>
    /// <returns></returns>
    public T GetItem()
    {
        if (objectPool == null) return null;
        if (objectPool.Count ==0)
        {
            MakePoolObject();
        }

        T returnItem = objectPool.Pop();
        returnItem.gameObject.SetActive(true);
        return returnItem;  
       
        }

}


