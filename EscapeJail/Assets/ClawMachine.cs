using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClawMachine : MonoBehaviour
{
    private bool usedMachine = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (usedMachine == true) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("도박기계 작동");
            GamePlayerManager.Instance.player.playerUi.clawMachinePopup.LinkFuncAndOpenPopup(()=> 
            {
                usedMachine = true;
                ItemSpawner.Instance.SpawnWeapon(this.transform.position+Vector3.down*0.5f);
                if (animator != null)
                    animator.SetTrigger("Complete");

                SoundManager.Instance.PlaySoundEffect("vendingMachine");
            });

            
        }
    }

}
