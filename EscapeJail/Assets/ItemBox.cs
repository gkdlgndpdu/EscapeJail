using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class ItemBox : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.speed = 0f;
        }

        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
        }
    }

    private void OpenBox()
    {
        if (animator != null)        
            animator.speed = 1f;

        if (boxCollider != null)
            boxCollider.enabled = false;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OpenBox();
        }


        return;
    }
  
}
