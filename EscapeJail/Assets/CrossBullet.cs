using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBullet : MonoBehaviour
{
    public Bullet bulletPrefab;
    private int bulletLength = 30;
    private float rotationSpeed = 80f;
    private float zAngle = 0f;
    private ObjectPool<Bullet> bulletPool;
    private List<Bullet> allBulletList = new List<Bullet>();
    private bool nowRotateBullet = false;

    private void Start()
    {
        MakeBulletPool();
   
    }

    private enum BulletDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    private void MakeBulletPool()
    {
        ObjectManager.Instance.MakePool<Bullet>(ref bulletPool, "Prefabs/Objects/Bullet", this.transform, bulletLength * 4);
    }

    public void RotationOnOff(bool OnOff)
    {
        nowRotateBullet = OnOff;
        if (OnOff == true)
        {
            StartCoroutine(bulletRotationRoutine());
        }
    }

    private IEnumerator bulletRotationRoutine()
    {
        this.transform.rotation = Quaternion.identity;
        zAngle = 0f;
        SetBullet();
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            zAngle += Time.deltaTime * rotationSpeed;
            this.transform.rotation = Quaternion.Euler(0f, 0f, zAngle);

            if (nowRotateBullet == false)
            {
                DestroyAllBullet();
                yield break;
            }

            yield return null;
        }
    }

    private void DestroyAllBullet()
    {
        if (allBulletList == null) return;
        for(int i=0;i< allBulletList.Count; i++)
        {
            allBulletList[i].BulletDestroy();
        }
    }



    private void MakeBullet(BulletDirection bulletDirection, int bulletNum)
    {
        for (int i = 0; i < bulletNum; i++)
        {
            Bullet bullet = bulletPool.GetItem();
            Vector3 dir = Vector3.one;
            switch (bulletDirection)
            {
                case BulletDirection.Up:
                    dir = Vector3.up;
                    break;
                case BulletDirection.Down:
                    dir = Vector3.down;
                    break;
                case BulletDirection.Left:
                    dir = Vector3.left;
                    break;
                case BulletDirection.Right:
                    dir = Vector3.right;
                    break;
            }
            bullet.Initialize(this.transform.position, dir, i * 0.3f, BulletType.EnemyBullet, 1f, 1, 999f);
            bullet.SetMoveLifetime(1f);
            bullet.SetDestroyByCollision(false);
            allBulletList.Add(bullet);

        }
    }

    private void SetBullet()
    {
        MakeBullet(BulletDirection.Up, bulletLength);
        MakeBullet(BulletDirection.Down, bulletLength);
        MakeBullet(BulletDirection.Left, bulletLength);
        MakeBullet(BulletDirection.Right, bulletLength);
    }




   

}
