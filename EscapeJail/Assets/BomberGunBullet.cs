using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberGunBullet : LivingBullet
{
    protected new void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
    }

    private void Start()
    {
        StartCoroutine(AutoOffRoutine());
        StartCoroutine(FindTargetRoutine());
        StartCoroutine(PathFindRoutine());
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ItemTable") == true) return;


        int layerMask = MyUtils.GetLayerMaskByString("Enemy");

        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, 1.5f, layerMask);
        if (colls == null) return;

        for (int i = 0; i < colls.Length; i++)
        {
            CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
            if (characterInfo != null)
                characterInfo.GetDamage(damage);
        }
    
        //이펙트 호출
        ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
        if (effect != null)
            effect.Initilaize(this.transform.position, "bazooka", 0.5f, 3f);

        //삭제
        DestroyBullet();
    }

}
