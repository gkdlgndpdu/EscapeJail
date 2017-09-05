using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSpawnPosit : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SpawnPosit;


    private void Start()
    {
        if (SpawnPosit != null)
            ItemSpawner.Instance.SpawnWeapon(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position);
    }


}
