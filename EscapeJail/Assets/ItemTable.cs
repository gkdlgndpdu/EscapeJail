using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class ItemTable : CharacterInfo
{  
    private int tableAnimFrameNum = 6;
    private BoxCollider2D boxCollider;
    private Animator animator;
    float animationFrameCount = 1f;

    [SerializeField]
    private List<Transform> SpawnPosit;

    public void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        if (animator != null)
            animator.speed = 0f;

        this.transform.localPosition = Vector3.zero;

        hp = 15;


    }

    private void BreakTable()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;
    }

    public override void GetDamage(int damage)
    {
        hp -= damage;

        //  체력 10, 5 , 0 일때 깨짐
        if (hp == 10 || hp == 5 || hp <= 0)
            DamageToTable();

    }

    private void DamageToTable()
    {
        animator.Play("TableAnim", 0, (1f / 6f) * animationFrameCount);
        animationFrameCount += 1f;

        //여기까지 오면 부서진거
        if (animationFrameCount > 3)
        {
            animator.speed = 1f;
            if (boxCollider != null)
                boxCollider.enabled = false;
        }
    }

    private void Start()
    {
        SpawnRamdomItem();
    }

    private void SpawnRamdomItem()
    {      
        //임시코드
        if (SpawnPosit != null)
        {
            ItemType itemType = (ItemType)Random.Range(0, (int)ItemType.Consumables);
            switch (itemType)
            {
                case ItemType.Weapon:
                    {
                        ItemSpawner.Instance.SpawnWeapon(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position, this.transform);
                    }
                    break;
                case ItemType.Armor:
                    {
                        ItemSpawner.Instance.SpawnArmor(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position, this.transform, Random.Range(1, 4));
                    }
                    break;

                case ItemType.Bullet:
                    {
                        ItemSpawner.Instance.SpawnBullet(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position, this.transform);
                    }
                    break;
                case ItemType.Bag:
                    {
                        ItemSpawner.Instance.SpawnBag(SpawnPosit[Random.Range(0, SpawnPosit.Count)].position, this.transform, Random.Range(1, 4));
                    }
                    break;
            }

        }
    }
   
}
