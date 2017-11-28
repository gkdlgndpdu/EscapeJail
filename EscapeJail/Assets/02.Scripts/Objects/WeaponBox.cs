using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class WeaponBox : MonoBehaviour, iReactiveAction
{

    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;


    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator != null)
            animator.speed = 0f;

        SetLayerOrder();
    }



    private void SetLayerOrder()
    {
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = GameConstants.ArticleLayerMin;
    }

    public void ClickAction()
    {
        OpenBox();
    }

    private void OpenBox()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;

        if(animator!=null)
            animator.speed= 1f;

        //무기 생성
        ItemSpawner.Instance.SpawnWeapon(this.transform.position,this.transform);
        
    }
}
