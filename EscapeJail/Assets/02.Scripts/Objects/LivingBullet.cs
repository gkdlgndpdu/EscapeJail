using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBullet : PlayerSpecialBullet
{
    protected Vector3 moveDir;
    protected float LifeTime = 10f;
    protected bool hasWall = false;

    protected void CalculateMoveDir()
    {
        if (hasWall == true) return;
        GameObject targetMonster = MonsterManager.Instance.GetNearestMonsterPos(this.transform.position);
        if (targetMonster != null)
        {
            moveDir = targetMonster.transform.position - this.transform.position;

            if (this.transform.position.x > targetMonster.transform.position.x)
                FlipSprite(false);
            else
                FlipSprite(true);
        }
        else if (targetMonster == null)
        {
            moveDir = Vector3.zero;
        }

    }

    protected void FlipSprite(bool OnOff)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = OnOff;
    }

    public void Initialize(Vector3 startPos, int damage)
    {
        this.transform.position = startPos;
        this.damage = damage;
    }

    protected new void Awake()
    {
        base.Awake();
 
    }
    protected void MoveToTarget()
    {
        CalculateMoveDir();

        if (rb != null)
            rb.velocity = moveDir.normalized * moveSpeed;
    }

    protected IEnumerator AutoOffRoutine()
    {
        yield return new WaitForSeconds(LifeTime);
        DestroyBullet();
    }

    protected IEnumerator FindTargetRoutine()
    {
        while (true)
        {
            MoveToTarget();
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }
    protected IEnumerator PathFindRoutine()
    {
        int layerMask = MyUtils.GetLayerMaskByString("ItemTable");
        float rayDistance = 1.5f;
        //찾은길로 이동하는 시간
        float findMoveTime = 1f;

        while (true)
        {
            Vector3 rayDir = moveDir.normalized;
            RaycastHit2D raycastHit = Physics2D.Raycast(this.transform.position, rayDir, rayDistance, layerMask);
            //벽이없음 -> 갈길간다
            if (raycastHit.collider == null)
            {
                Debug.Log("벽이없음");
                hasWall = false;
            }
            //벽이 탐지됨 ->길을 찾는다
            else
            {
                Debug.Log("벽이있어");
                hasWall = true;

                for (int i = 1; i < 5; i++)
                {
                    Vector3 nextRayDir1 = Quaternion.Euler(0f, 0f, i * 40f) * rayDir;
                    Vector3 nextRayDir2 = Quaternion.Euler(0f, 0f, i * -40f) * rayDir;
                    RaycastHit2D raycastHit1 = Physics2D.Raycast(this.transform.position, nextRayDir1, rayDistance, layerMask);
                    RaycastHit2D raycastHit2 = Physics2D.Raycast(this.transform.position, nextRayDir2, rayDistance, layerMask);

                    bool findPath = false;
                    float pointdistance = 99f;

                    if (raycastHit1.collider == null)
                    {
                        findPath = true;
                        pointdistance = raycastHit1.distance;
                        moveDir = nextRayDir1.normalized;
                    }
                    if (raycastHit2.collider == null)
                    {
                        findPath = true;

                        //두번째 경로가 더 짧으면
                        if (raycastHit2.distance < pointdistance)
                        {
                            moveDir = nextRayDir2.normalized;

                        }
                    }

                    if (findPath == true)
                    {
                        if (rb != null)
                            rb.velocity = moveDir * moveSpeed * 1.5f;

                        yield return new WaitForSeconds(findMoveTime);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

    }

  

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ItemTable") == true) return;

        CharacterInfo chr = collision.gameObject.GetComponent<CharacterInfo>();
        if (chr != null)
        {
            chr.GetDamage(damage);
            DestroyBullet();
        }
    }

    protected void DestroyBullet()
    {
        GameObject.Destroy(this.gameObject);
    }

}
