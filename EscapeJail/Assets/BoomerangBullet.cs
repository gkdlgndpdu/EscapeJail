using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : PlayerSpecialBullet
{
    private enum State
    {
        Throw,
        Receive
    }

    private State state = State.Throw;
    private Vector3 moveDir = Vector3.zero;
    private float ThrowTime = 1f;
    private Transform playerTr;
    private float originSpeed;
    private new void Awake()
    {
        base.Awake();
        moveSpeed = 5f;
        originSpeed = moveSpeed;
    }

    void Start()
    {
 
        playerTr = GamePlayerManager.Instance.player.transform;
    }

    public void Initialize(Vector3 firePos,Vector3 fireDir,int damage)
    {
        this.damage = damage;
        this.transform.position = firePos;
        this.moveDir = fireDir.normalized;
        this.moveSpeed = originSpeed;
        StartCoroutine(ThrowRoutine());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo chr = collision.gameObject.GetComponent<CharacterInfo>();
        if (chr != null)
        {
            chr.GetDamage(damage);
        }
    }

    IEnumerator ThrowRoutine()
    {
        state = State.Throw;
        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

        yield return new WaitForSeconds(ThrowTime);
        state = State.Receive;
    }

    private void MoveBoomerang()
    {
        if (state == State.Throw) return;
        if (playerTr == null) return;

        moveDir = playerTr.position - this.transform.position;

        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;

        moveSpeed += Time.deltaTime*1.3f;

        float dist = Vector3.Distance(this.transform.position, playerTr.transform.position);
        if (dist < 0.1f)
        {
            DestroyBoomerang();
        }
    }

    private void DestroyBoomerang()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    void Update()
    {
        MoveBoomerang();
    }
}
