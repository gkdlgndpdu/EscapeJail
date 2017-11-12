using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBullet : ShapeBulletBase
{ 

    private int bulletLength = 30;
    private float rotationSpeed = 50f;
    private float zAngle = 0f;
    private bool nowRotateBullet = false;

    private void Start()
    {
        MakeBulletPool(bulletLength * 4);

    }

    private enum BulletDirection
    {
        Up,
        Down,
        Left,
        Right
    }



    public void RotationOnOff(bool OnOff)
    {
        if (OnOff == true)
        {
            StartCoroutine(bulletRotationRoutine());
        }
        else if (OnOff == false)
        {
            DestroyAllBullet();
            StopAllCoroutines();
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

            yield return null;
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
