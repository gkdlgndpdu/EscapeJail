using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector3 moveDir = Vector3.one;
    // Use this for initialization
    private float moveSpeed = 0f;

    public void Initialize(Vector3 startPos,Vector3 moveDir, float moveSpeed)
    {
        this.transform.position = startPos;
        this.moveDir = moveDir;
        this.moveSpeed = moveSpeed;
    }

    void Move()
    {
        this.transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
            
    } 
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }
}
