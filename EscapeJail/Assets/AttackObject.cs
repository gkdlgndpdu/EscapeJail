using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private int power = 1;

    private int Initialize(int power)
    {
        this.power = power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterBase characterBase = collision.gameObject.GetComponent<CharacterBase>();
            if (characterBase != null)
                characterBase.GetDamage(this.power);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
