using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private int power = 1;
    //공격 2번 들어가는 예외사항 제외
    private bool isAttackFinished = false;

    private void Initialize(int power,string effectName)
    {
        this.power = power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&
            isAttackFinished==false)
        {
            CharacterBase characterBase = collision.gameObject.GetComponent<CharacterBase>();
            if (characterBase != null)
            {
                characterBase.GetDamage(this.power);
                isAttackFinished = true;
            }

        }
    }

    private void OnDisable()
    {
        isAttackFinished = false;
    }

   

}
