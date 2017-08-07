using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : MonoBehaviour
{
    //컴포넌트    
    private Rigidbody2D rb;
    [SerializeField]
    Animator animController;

    //값변수
    [SerializeField]
    private float moveSpeed=10f;



    private void Awake()
    {
        Initialize();
    }
    void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveInPc();

    }

    protected void MoveInPc()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = Vector3.right * h + Vector3.up * v;
       // moveDir.Normalize();
         
        //이동
        if(rb!=null)
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;

        //애니메이션
        AnimControl(moveDir);
    }

    protected void AnimControl(Vector3 MoveDir)
    {
        if (animController == null) return;

        float SpeedValue = Mathf.Abs(MoveDir.x) + Mathf.Abs(MoveDir.y);
        animController.SetFloat("Speed", SpeedValue);
        if (SpeedValue > 0.2f)
        {
            animController.speed = 1f;
            animController.SetFloat("DirectionX", MoveDir.x);
            animController.SetFloat("DirectionY", MoveDir.y);
        }
        else
        {
            animController.Play("Walk", -1,0f);
            animController.speed = 0f;
        }
  
    }
}
