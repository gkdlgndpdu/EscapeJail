using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class SlashObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private int power = 0;
    private float slashTime = 1f;
    private float slashCount = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    } 

    public void Initialize(int power, Color color, Vector3 size)
    {
        //색상
        SetSlashColor(color);

        //크기
        this.transform.localScale = size;

        //공격력
        this.power = power;

    }

    private void SetSlashColor(Color color)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = color;
    }


    private bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void Update()
    {
        //애니메이션 끝나면 자동종료
        if (AnimatorIsPlaying() == false)
            SlashOff();

    }
    private void SlashOff()
    {
        this.gameObject.SetActive(false);
    }

    public void FlipObject(bool flip)
    {
        if (flip == false)
            this.transform.localRotation = Quaternion.identity;   
        else
            this.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
    }


    //충돌관련
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ItemTable"))
        {
            DamegeToItemTable(collision);
        }    
          
        SingleTargetDamage(collision);
    }

    private void DamegeToItemTable(Collider2D collision)
    {
        ItemTable table = collision.gameObject.GetComponent<ItemTable>();
        if (table != null)
            table.GetDamage(power);
    }

    private void SingleTargetDamage(Collider2D collision)
    {
        //충돌여부는 layer collision matrix로 분리해놓음
        if (collision.gameObject.CompareTag("Enemy") == true || collision.gameObject.CompareTag("Player"))
        {
            CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();

            if (characterInfo != null)
                characterInfo.GetDamage(this.power);
        }
    }


}
