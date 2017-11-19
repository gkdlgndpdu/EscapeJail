using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class BfeBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 3f;
    private int ticDamage = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector3 startPos, Vector3 moveDir,int ticDamage = 1)
    {
        this.transform.position = startPos;
        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

        this.ticDamage = ticDamage;
    }

    public IEnumerator TicDamageRoutine()
    {
        yield return null;
    }
  
}
