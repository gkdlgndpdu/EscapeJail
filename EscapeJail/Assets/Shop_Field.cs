using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Field : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> tableList;

    private void Start()
    {
        if (tableList == null) return;

        for(int i = 0; i < tableList.Count; i++)
        {
            ItemSpawner.Instance.SpawnWeapon(tableList[i].transform.position, true);
        }
    }


}
