using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBulletBase : MonoBehaviour
{
    public Bullet bulletPrefab;
    protected ObjectPool<Bullet> bulletPool;
    protected List<Bullet> allBulletList = new List<Bullet>();

    protected void MakeBulletPool(int bulletNum)
    {
        ObjectManager.Instance.MakePool<Bullet>(ref bulletPool, "Prefabs/Objects/Bullet", this.transform, bulletNum);
    }

    protected void DestroyAllBullet()
    {
        if (allBulletList == null) return;
        for (int i = 0; i < allBulletList.Count; i++)
        {
            if(allBulletList[i].gameObject.activeSelf==true)
            allBulletList[i].BulletDestroy();
        }
        allBulletList.Clear();
    }

    protected void OnDisable()
    {
        if (bulletPool == null) return;
        bulletPool.ReleasePool();
        bulletPool = null;
    }
}
