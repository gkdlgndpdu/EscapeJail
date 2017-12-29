using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : ShapeBulletBase
{
    private int bulletNum = 24;
    private float rotationSpeed = 70f;
    private float zAngle = 0f;
    private bool nowAttack = false;
    public bool NowAttack
    {
        get
        {
            return nowAttack;
        }
    }

    private void Start()
    {
        MakeBulletPool(bulletNum);
    }
   
    public void StartAttack()
    {
        nowAttack = true;
        StartCoroutine(AttackRoutine());
    }

    public void StopAllAction()
    {
        StopAllCoroutines();
        DestroyAllBullet();
    }

    private IEnumerator AttackRoutine()
    {
        float bulletSpeed =10f;

        //총알 생성
        for (int i = 0; i < bulletNum; i++)
        {
            Bullet bullet = bulletPool.GetItem();
            Vector3 dir = Vector3.up;

            dir = Quaternion.Euler(0f, 0f, i * 15f) * dir;
            bullet.transform.parent = this.transform;
            bullet.Initialize(this.transform.position, dir.normalized, bulletSpeed, BulletType.EnemyBullet, 1f, 1, 999f);
            bullet.SetMoveLifetime(1f);
            bullet.SetDestroyByCollision(false, false);
            allBulletList.Add(bullet);

            yield return new WaitForSeconds(0.05f);
        }

        //총알 회전
        this.transform.rotation = Quaternion.identity;
        zAngle = 0f;
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(RotateBulletFire());
        while (true)
        {
            zAngle += Time.deltaTime * rotationSpeed;
            this.transform.rotation = Quaternion.Euler(0f, 0f, zAngle);
            yield return null;
        }
    }

    //돌면서 나가는 부분
    private IEnumerator RotateBulletFire()
    {
        Transform playerTr = GamePlayerManager.Instance.player.transform;
        allBulletList.ListShuffle(50);
        for (int i = 0; i < allBulletList.Count; i++)
        {
            Vector3 fireDir = playerTr.position - allBulletList[i].transform.position;
            fireDir.Normalize();
            allBulletList[i].transform.parent = null;
            allBulletList[i].Initialize(allBulletList[i].transform.position, fireDir, 10f, BulletType.EnemyBullet,1.5f);
            yield return new WaitForSeconds(0.3f);
        }

        //모든것을 끝낸다
        if(allBulletList!=null)
        allBulletList.Clear();
        this.transform.rotation = Quaternion.identity;
        StopAllAction();
        nowAttack = false;
    }



}
