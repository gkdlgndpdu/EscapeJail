using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class MouseHand : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float originAnimSpeed;
    private float delay = 0.7f;

    private bool isFirstCreate = true;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (animator != null)
        {
            originAnimSpeed = animator.speed;   
        }
      
    }

    private void HandOn()
    {
        if (boxCollider != null)
            boxCollider.enabled = true;

        if (animator != null)
            animator.speed = originAnimSpeed;
    }
    private void HandOff()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;


        if (animator != null)
            animator.speed = 0f;
    }

    public void OnEnable()
    {
        if(isFirstCreate==false)
        Invoke("HandOn", delay);

        isFirstCreate = false;
    }

    public void OnDisable()
    {
        HandOff();
    }

    //애니메이션에 연결됨
    public void HandAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }
 

}
