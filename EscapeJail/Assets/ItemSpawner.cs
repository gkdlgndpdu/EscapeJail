using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;
    private GameObject dropItemPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        LoadPrefab();
    }
    private void LoadPrefab()
    {
        GameObject obj = Resources.Load("Prefabs/Objects/DropItem") as GameObject;
        if (obj != null)
            dropItemPrefab = obj;
    }
    public void SpawnWeapon(Vector3 posit)
    {

        if (dropItemPrefab == null) return;
        GameObject obj = GameObject.Instantiate(dropItemPrefab, posit, Quaternion.identity, this.transform);
        if (obj == null) return;
        DropItem item = obj.GetComponent<DropItem>();
        if (item == null) return;

        WeaponType RandomWeapon = (WeaponType)Random.Range((int)(WeaponType.PlayerWeaponStart + 1), (int)WeaponType.PlayerWeaponEnd);
        item.SetItemToWeapon(RandomWeapon);                   
    }

    public void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    SpawnWeapon(GamePlayerManager.Instance.player.transform.position, WeaponType.Revolver);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    SpawnWeapon(GamePlayerManager.Instance.player.transform.position, WeaponType.ShotGun);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SpawnWeapon(GamePlayerManager.Instance.player.transform.position, WeaponType.AssaultRifle);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    SpawnWeapon(GamePlayerManager.Instance.player.transform.position, WeaponType.WaterGun);
        //}
    }
}
