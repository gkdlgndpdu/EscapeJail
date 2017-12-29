using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossFirePosition : MonoBehaviour
{
    private Transform target;
    private void Start()
    {
        target = GamePlayerManager.Instance.player.transform;
    }
    public void FireStart()
    {
        StartCoroutine("fireRoutine");
    }
    public void EndFire()
    {
        StopCoroutine("fireRoutine");
    }
    private IEnumerator fireRoutine()
    {
        while (true)
        {
            FireBullet();
          
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void FireBullet()
    {
        SpecialBullet bullet = ObjectManager.Instance.specialBulletPool.GetItem();
        float rebound = 20f;
        float bulletSpeed =5f;
        SoundManager.Instance.PlaySoundEffect("Laserpistol");

        if (bullet != null)
        {
            Vector3 fireDir = target.position - this.transform.position;
            fireDir = Quaternion.Euler(0f, 0f, Random.Range(-rebound, rebound)) * fireDir;
            bullet.Initialize(this.transform.position, fireDir.normalized, bulletSpeed, BulletType.EnemyBullet, SpecialBulletType.LaserBullet, 2f, 1,10f);
            bullet.SetBloom(true, Color.red);
            
        }
    }
    


}
