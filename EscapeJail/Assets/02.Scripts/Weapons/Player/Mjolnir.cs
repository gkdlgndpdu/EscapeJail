using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class Mjolnir : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;
        private float explosionRadius = 10f;
        private int multiDamage = 3;
        public Mjolnir()
        {
            SetNearWeapon(CustomColor.Silver, Vector3.one * 8f);
            weapontype = WeaponType.Mjolnir;
            SetWeaponKind(WeaponKind.Special);
            fireDelay = 0.5f;
            SetAmmo(30);
            needBulletToFire = 1;
            damage = 1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();


            int layerMask = MyUtils.GetLayerMaskByString("Enemy");
            SoundManager.Instance.PlaySoundEffect("thunder");

            Collider2D[] colls = Physics2D.OverlapCircleAll(firePos, explosionRadius, layerMask);
            if (colls == null) return;

            for (int i = 0; i < colls.Length; i++)
            {
                CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
                if (characterInfo != null)
                {
                    ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
                    if(effect != null)
                        effect.Initilaize(characterInfo.transform.position+Vector3.up, "Thunder", 0.5f, 3f);

                    characterInfo.GetDamage(multiDamage);
                }
            }


            //이펙트 호출
            ExplosionEffect effect2 = ObjectManager.Instance.effectPool.GetItem();
            if (effect2 != null)
                effect2.Initilaize(firePos + Vector3.down * 0.5f, "PowerGauntletEffect", 0.5f, 2f);


        }
    }
}