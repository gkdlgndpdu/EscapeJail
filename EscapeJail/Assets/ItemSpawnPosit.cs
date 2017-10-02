using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSpawnPosit : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SpawnPosit;


    private void Start()
    {

                        ItemSpawner.Instance.SpawnWeapon(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position);

      //  ItemSpawner.Instance.SpawnBag(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position,Random.Range(1,4));

        return;

        if (SpawnPosit != null)
        {
            ItemType itemType = (ItemType)Random.Range(0, (int)ItemType.Consumables);
            switch (itemType)
            {
                case ItemType.Weapon:
                    {
                        ItemSpawner.Instance.SpawnWeapon(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position);
                    }
                    break;
                case ItemType.Armor:
                    {
                        ItemSpawner.Instance.SpawnArmor(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position);
                    }
                    break;

                case ItemType.Bullet:
                    {
                        ItemSpawner.Instance.SpawnBullet(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position);
                    } break;
                case ItemType.Bag:
                    {
                        ItemSpawner.Instance.SpawnBag(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position);
                    } break;
            }

        }
    }


}
