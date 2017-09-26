using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;
    private GameObject dropItemPrefab;

    private List<WeaponType> spawnedWeaponList;
    private List<GameObject> spawnedObjectList;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        LoadPrefab();

        spawnedWeaponList = new List<WeaponType>();
        spawnedObjectList = new List<GameObject>();
    }
    private void LoadPrefab()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/Objects/DropItem");
        if (obj != null)
            dropItemPrefab = obj;
    }

    //무기 중복생성 방지
    private bool isWeaponSpawned(WeaponType weaponType)
    {
        if (spawnedWeaponList == null) return true;

        for (int i = 0; i < spawnedWeaponList.Count; i++)
        {
            if (spawnedWeaponList[i] == weaponType)
                return true;
        }
        spawnedWeaponList.Add(weaponType);
        return false;

    }
    public void SpawnWeapon(Vector3 posit)
    {
        WeaponType RandomWeapon = (WeaponType)Random.Range((int)(WeaponType.PlayerWeaponStart + 1), (int)WeaponType.PlayerWeaponEnd);

        //중복체크 및 중복아니면 리스트에 추가
        if (isWeaponSpawned(RandomWeapon) == true) return;  
 
        DropItem item = MakeItemPrefab(posit);
        if (item == null) return;

        item.SetItemToWeapon(RandomWeapon);

        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);
    }

    public void SpawnArmor(Vector3 posit)
    {
        DropItem item = MakeItemPrefab(posit);
        if (item == null) return;

        item.SetItemToArmor(1);

        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);
    }

    public DropItem MakeItemPrefab(Vector3 posit)
    {
        GameObject obj = GameObject.Instantiate(dropItemPrefab, posit, Quaternion.identity, this.transform);
        DropItem item =null;
        if (obj != null)
        {
            item = obj.GetComponent<DropItem>();
        }

        return item;
    }

    public void DestroyAllItems()
    {
        if (spawnedObjectList == null) return;

        for(int i=0;i< spawnedObjectList.Count; i++)
        {
            GameObject.Destroy(spawnedObjectList[i].gameObject);
        }
    }

 
}
