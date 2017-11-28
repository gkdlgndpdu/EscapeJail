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
    private Transform target;
    private float moveSpeed = 10f;  

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
   
    }

    public void SetTarget(Transform target)
    {
        this.target = target;

    }

    private IEnumerator fireRoutine()
    {
        while (true)
        {
            GameObject monster = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
            if (monster != null)
            {
                if (animator != null)
                    animator.SetTrigger("FireTrigger");

                if (monster.transform.position.x > this.transform.position.x)
                    FlipDrone(true);
                else
                    FlipDrone(false);

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

    private void FlipDrone(bool OnOff)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = OnOff;
    }

    // Use this for initialization
    void Start()
    {
        ShowDrone();

    }

    private void Update()
    {
        MoveDrone();
    }

    private void MoveDrone()
    {
        if (target == null) return;

        this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position, Time.deltaTime * moveSpeed);
    }

    public void HideDrone()
    {
        StopCoroutine("fireRoutine");

        if (animator != null)
            animator.SetTrigger("HideDrone");
    }

    public void ShowDrone()
    {
        Invoke("StartFire", 1f);

        if (animator != null)
            animator.SetTrigger("ShowDrone");
    }

    private void StartFire()
    {
        StartCoroutine("fireRoutine");
    }

    private void ChangeAnim()
    {

    }

}
