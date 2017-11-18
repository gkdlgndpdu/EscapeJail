using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지 넘길때 오브젝트풀 말고 임시적으로 생성한 것들 지워주는용
public class TemporaryObjects : MonoBehaviour
{
    public static TemporaryObjects Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyAllChildrenObject()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);
    }



}
