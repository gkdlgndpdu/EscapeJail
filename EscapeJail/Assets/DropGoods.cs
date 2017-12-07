using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsType
{
    Coin,
    Medal
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class DropGoods : MonoBehaviour
{
    private Rigidbody2D rb;
    private int value = 0;
    private CharacterBase player;
    private float moveSpeed = 10f;
    private float originSpeed = 10f;
    private float sleepTime = 1f;
    private bool isSleep = true;
    private GoodsType goodsType;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite coinSprite;
    [SerializeField]
    private Sprite medalSprite;

    private void Awake()
    {
        player = GamePlayerManager.Instance.player;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initiatlize(Vector3 spawnPosit, int value,GoodsType goodsType)
    {
        this.transform.position = spawnPosit;
        this.value = value;
        this.goodsType = goodsType;
        SetSprite();
        StartCoroutine(SleepRoutine());
    }
    private void SetSprite()
    {
        if (spriteRenderer == null) return;
        if (coinSprite == null || medalSprite == null) return;
        switch (goodsType)
        {
            case GoodsType.Coin:
                spriteRenderer.sprite = coinSprite;
                break;
            case GoodsType.Medal:
                spriteRenderer.sprite = medalSprite;
                break;
        }
  
    }
    IEnumerator SleepRoutine()
    {
        yield return new WaitForSeconds(sleepTime);
        isSleep = false;
    }

    public void Update()
    {
        if (isSleep == true) return;

        if (player != null)
        {
            if (rb != null)
            {
                Vector3 moveDir = player.transform.position - this.transform.position;
                rb.velocity = moveDir.normalized * moveSpeed;
                moveSpeed += Time.deltaTime * 5f;
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSleep == true) return;

        switch (goodsType)
        {
            case GoodsType.Coin:
                {
                    if (player != null)
                        player.GetCoin(value);
                }
                break;
            case GoodsType.Medal:
                {
                    if (player != null)
                        player.GetMedal(value);
                }
                break;
        }

        OffCoin();
    }

    private void OffCoin()
    {
        moveSpeed = originSpeed;
        isSleep = true;
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }
}
