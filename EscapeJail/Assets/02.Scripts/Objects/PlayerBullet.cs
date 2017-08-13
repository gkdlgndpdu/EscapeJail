using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector3 moveDir = Vector3.zero;
    // Use this for initialization
    private float moveSpeed = 0f;

    public void Initialize(Vector3 startPos,Vector3 moveDir, float moveSpeed)
    {
        //Debug.Log("start pos : " + startPos);
        //Debug.Log("moveDir : " + moveDir);
        //Debug.Log("moveSpeed : " + moveSpeed);

        this.transform.position =new Vector3(startPos.x, startPos.y, 0f);
        this.moveDir = moveDir;    
        this.moveSpeed = moveSpeed;
    }

    void Move()
    {        
        Vector3 moveValue = moveDir * moveSpeed * Time.deltaTime;  
        this.transform.position += moveValue;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
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
