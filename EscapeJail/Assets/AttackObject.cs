using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private Animator animator;
    private int power = 1;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialize(int power)
    {
        this.power = power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterBase characterBase = collision.gameObject.GetComponent<CharacterBase>();
            if (characterBase != null)
            {
                characterBase.GetDamage(this.power);            
            }

        }
    }


   

}
