using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class PowerGauntlet : Weapon
    {

        private float reBoundValue = 0f;
        private float explosionRadius = 5f;
        private int multiDamage=3;
        
        public PowerGauntlet()
        {
            SetNearWeapon(Color.yellow, Vector3.one * 8f);

            weapontype = WeaponType.PowerGauntlet;   
            fireDelay = 0.4f;
            SetAmmo(100);
            needBulletToFire = 1;

        }
        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();

            //밀어내기

            int layerMask = MyUtils.GetLayerMaskByString("Enemy");
          

            Collider2D[] colls = Physics2D.OverlapCircleAll(firePos, explosionRadius, layerMask);
            if (colls == null) return;

            for (int i = 0; i < colls.Length; i++)
            {
                CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
                if (characterInfo != null)
                    characterInfo.SetPush(firePos,3f, multiDamage);
            }

            //이펙트
            //이펙트 호출
            ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
            if (effect != null)
                effect.Initilaize(firePos+Vector3.down*0.5f, "PowerGauntletEffect", 0.5f, 3f);

            


        }
    }
}