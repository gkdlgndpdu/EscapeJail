using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : MonoBehaviour
{
    //컴포넌트    
    private Rigidbody2D rb;

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
        moveDir.Normalize();

        if(rb!=null)
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }
}
