using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Coin : MonoBehaviour
{
    private Rigidbody2D rb;
    private int value = 0;
    private CharacterBase player;
    private float moveSpeed = 5f;

    private void Awake()
    {
        player = GamePlayerManager.Instance.player;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initiatlize(Vector3 spawnPosit,int value) 
    {
        this.transform.position = spawnPosit;
        this.value = value;
    }

    public void Update()
    {
        if (player != null)
        {
            if (rb != null)
            {
                Vector3 moveDir = player.transform.position - this.transform.position;
                rb.velocity = moveDir.normalized * moveSpeed;
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(player!=null)
        player.GetCoin(value);
        this.gameObject.SetActive(false);
    }
}
