using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;
    private GameObject dropItemPrefab;

    [SerializeField]
    private MapManager mapManager;
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

        spawnedObjectList = new List<GameObject>();


        SetRandomGenerator();

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

    public DropItem SpawnRandomItem(Vector3 spawnPosit, Transform parent = null)
    {
        if (randomGenerator == null) return null;
        if (mapManager == null) return null;

        ItemType randomType = randomGenerator.GetRandomData();
        if (parent == null)
        {
          return SpawnItem(randomType, spawnPosit, mapManager.transform);
        }
        else
        {
           return SpawnItem(randomType, spawnPosit, parent);
        }

    }



    private void LoadPrefab()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/Objects/DropItem");
        if (obj != null)
            dropItemPrefab = obj;
    }


    //중복체크 X 아이템 삭제할때 땅에 똑같은거 버려주는 용도로 사용
    public void SpawnWeapon(Vector3 posit, WeaponType weaponType, Transform parent = null)
    {
        if (mapManager == null) return;
        WeaponType RandomWeapon = weaponType;


        DropItem item = MakeItemPrefab(posit);
        if (item == null) return;

        if (parent == null)
            item.transform.parent = mapManager.transform;
        else
            item.transform.parent = parent;

        item.SetItemToWeapon(RandomWeapon);

        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);
    }

    ///////////////////임시코드




    public void SpawnWeapon(Vector3 posit, bool isSalesItem = false)
    {
        if (mapManager == null) return;
        WeaponType RandomWeapon = DatabaseLoader.Instance.GetRandomWeaponTypeByProbability();

        DropItem item = MakeItemPrefab(posit);
        if (item == null) return;
        item.transform.parent = mapManager.transform;
        item.SetItemToWeapon(RandomWeapon);

        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);

        if (isSalesItem == true)
            item.SetItemToSales();
    }


    public DropItem SpawnItem(ItemType itemType, Vector3 posit, Transform parent, bool isSalesItem = false, int level = 999)
    {
        DropItem item = MakeItemPrefab(posit);
        if (item == null) return null;
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
                    if (level == 999)
                        item.SetItemToStimulant();
                    else
                        item.SetItemToStimulant(level);
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
            case ItemType.Turret:
                {
                    item.SetItemToTurret();
                }
                break;
            case ItemType.FlashBang:
                {
                    item.SetItemToFlashBang();
                }
                break;
        }


        if (spawnedObjectList != null)
            spawnedObjectList.Add(item.gameObject);

        if (isSalesItem == true)
            item.SetItemToSales();

        return item;
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
