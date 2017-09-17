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
    private float delay = 1f;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (animator != null)
        {
            originAnimSpeed = animator.speed;   
        }

        HandOff();
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
        Invoke("HandOn", delay);
    }

    public void OnDisable()
    {
        HandOff();
    }

    public void HandAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }
 

}
