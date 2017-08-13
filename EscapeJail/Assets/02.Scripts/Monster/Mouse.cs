using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonsterBase
{
    //임시
    [SerializeField]
    SpriteRenderer spriteRenderer;
    
	// Use this for initialization
	private new void Start ()
    {
        base.Start();
	}

    private new void Awake()
    {
        base.Awake();
    }
	
	// Update is called once per frame
	private new void Update ()
    {
        base.Update();
        SetMoveAnimation();

    }

    private void SetMoveAnimation()
    {
        if (animator == null) return;
        animator.SetFloat("DirectionX", moveDir.x);
        //animator.SetFloat("Speed",)

        //임시
        if (spriteRenderer == null) return;
        if (moveDir.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        
            
    }
}
