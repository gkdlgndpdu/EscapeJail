using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class BfeBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private int ticDamage = 1;
    private float lifeTime = 3f;
    private float attackRadius = 3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, int ticDamage = 1)
    {
        this.transform.position = startPos;
        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;
        this.ticDamage = ticDamage;
        StartCoroutine(AutoOffRoutine());
        StartCoroutine(TicDamageRoutine());
    }

    private IEnumerator AutoOffRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    private IEnumerator TicDamageRoutine()
    {
        while (true)
        {
            int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, attackRadius, layerMask);
            if (colls != null)
            {
                for (int i = 0; i < colls.Length; i++)
                {
                    CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
                    if (characterInfo != null)
                        characterInfo.GetDamage(ticDamage);

                    //연출
                    ThunderLine thunderLine = ObjectManager.Instance.thunderLinePool.GetItem();
                    if (thunderLine != null)
                    {
                        thunderLine.Initialize(this.transform, characterInfo.transform,Color.green,0.5f);
                    }

                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

}
