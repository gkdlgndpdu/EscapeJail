using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;
    private GameObject dropItemPrefab;

    private List<WeaponType> spawnedWeaponList;
    //맵 변경될때 삭제하기 위함
    private List<GameObject> spawnedObjectList;


    private RandomGenerator<ItemType> randomGenerator;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        LoadPrefab();

        spawnedWeaponList = new List<WeaponType>();
        spawnedObjectList = new List<GameObject>();


        SetRandomGenerator();
        SetRandomWeapon();
    }

    private void SetRandomGenerator()
    {
        randomGenerator = new RandomGenerator<ItemType>();

        for (int i = 0; i < (int)ItemType.ItemTypeEnd; i++)
        {
            ItemType itemKey = (ItemType)i;

            ItemProbabilityDB itemDB = DatabaseLoader.Instance.GetItemProbabilityDB(itemKey);
            if (itemDB != null)
                randomGenerator.AddToList(itemKey, itemDB.Probability);
        }

    }

    public void SpawnRandomItem(Vector3 spawnPosit, Transform parent)
    {
        if (randomGenerator == null) return;

        ItemType randomType = randomGenerator.GetRandomData();
        SpawnItem(randomType, spawnPosit, parent);

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


    //중복체크 X 아이템 삭제할때 땅에 똑같은거 버려주는 용도로 사용
    public void SpawnWeapon(Vector3 posit, Transform parent, WeaponType weaponType)
    {
        WeaponType RandomWeapon = weaponType;


        DropItem item = MakeItemPrefab(posit);
        if (item == null) return;
        item.transform.parent = parent;
        item.SetItemToWeapon(RandomWeapon);

        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);
    }

    ///////////////////임시코드


    //임시코드 (아이템 확률이 생기면 삭제)
    private List<WeaponType> shuffledWeaponList = new List<WeaponType>();

    public void SpawnWeapon(Vector3 posit, Transform parent)
    {
        WeaponType RandomWeapon = WeaponType.Revolver;

        if (shuffledWeaponList.Count > 0)
        {
            RandomWeapon = shuffledWeaponList[0];
            shuffledWeaponList.RemoveAt(0);
        }
        else if (shuffledWeaponList.Count <= 0)
        {
            return;
        }


        DropItem item = MakeItemPrefab(posit);
        if (item == null) return;
        item.transform.parent = parent;
        item.SetItemToWeapon(RandomWeapon);

        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);
    }
    private void SetRandomWeapon()
    {
        for (int i = 0; i < (int)WeaponType.PlayerWeaponEnd; i++)
        {
            shuffledWeaponList.Add((WeaponType)i);
        }

        shuffledWeaponList.ListShuffle(100);
    }




    ////////////////////임시코드

    public void SpawnItem(ItemType itemType, Vector3 posit, Transform parent, int level = 999)
    {
        DropItem item = MakeItemPrefab(posit);
        if (item == null) return;
        item.transform.parent = parent;


        switch (itemType)
        {
            case ItemType.Armor:
                {
                    item.SetItemToArmor();
                }
                break;
            case ItemType.Bullet:
                {
                    item.SetItemToBullet();
                }
                break;
            case ItemType.Bag:
                {
                    item.SetItemToBag();
                }
                break;
            case ItemType.Stimulant:
                {

                }
                break;
            case ItemType.Medicine:
                {
                    if (level == 999)
                        item.SetItemToMedicine();
                    else
                        item.SetItemToMedicine(level);
                }
                break;
            case ItemType.EnergyDrink:
                {

                }
                break;
        }


        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);
    }

    public DropItem MakeItemPrefab(Vector3 posit)
    {
        GameObject obj = GameObject.Instantiate(dropItemPrefab, posit, Quaternion.identity, this.transform);
        DropItem item = null;
        if (obj != null)
        {
            item = obj.GetComponent<DropItem>();
        }

        return item;
    }

    public void DestroyAllItems()
    {
        if (spawnedObjectList == null) return;

        for (int i = 0; i < spawnedObjectList.Count; i++)
        {
            GameObject.Destroy(spawnedObjectList[i].gameObject);
        }
    }


}
