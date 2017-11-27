using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Engineer_Drone : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float bulletSpeed = 10f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator fireRoutine()
    {
        while (true)
        {
            GameObject monster = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
            if (monster != null)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    Vector3 fireDir = monster.transform.position - this.transform.position;                    
                    bullet.Initialize(this.transform.position, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 1f, 1);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }

          
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(fireRoutine());
	}	

    private void ChangeAnim()
    {

    }

}
