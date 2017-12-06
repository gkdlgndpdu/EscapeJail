using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G403Bullet : LivingBullet
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

        CharacterInfo chr = collision.gameObject.GetComponent<CharacterInfo>();
        if (chr != null)
        {
            chr.GetDamage(damage);
            DestroyBullet();

            ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
            if (effect != null)
                effect.Initilaize(this.transform.position, "revolver", 0.5f, 1f);
        }
    }

 

}
