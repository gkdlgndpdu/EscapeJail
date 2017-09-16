using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MouseHand : MonoBehaviour
{
    private Animator animator;
    public void HandAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (animator == null) return;

            
    }

}
