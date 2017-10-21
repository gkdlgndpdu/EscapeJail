using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private List<T> objectPool;
    private GameObject prefab;
    private Transform objectParent;

    public ObjectPool(Transform objectParent, GameObject prefab, int objectNum)
    {
        this.objectPool = new List<T>();
        this.prefab = prefab;
        this.objectParent = objectParent;

        for (int i = 0; i < objectNum; i++)
        {
            MakePoolObject();
        }
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
                objectPool.Add(obj);
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

        for(int i=0;i< objectPool.Count; i++)
        {
            GameObject.Destroy(objectPool[i]);
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

        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].gameObject.activeSelf == true) continue;
            objectPool[i].gameObject.SetActive(true);
            return objectPool[i];
        }     

        //여기 내려오면 풀이 부족해서 못만들어준 경우.
        MakePoolObject();
        objectPool[objectPool.Count - 1].gameObject.SetActive(true);
        return objectPool[objectPool.Count - 1];
  
    }


}
