
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
{
    public class BasicSniper : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;
        private float fireDistance = 50f;

        public BasicSniper()
        {
            weapontype = WeaponType.BasicSniper;
            bulletSpeed = 10f;
            fireDelay = 1f;
            maxAmmo = 100;
            nowAmmo = 100;
            needBulletToFire = 1;
            weaponScale = Vector3.one * 0.7f;
            relativePosition = new Vector3(-0.3f, 0f, 0f);
            damage = 10;



        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            useBullet();
            FireDelayOn();
            PlayFireAnim();

            //   int layerMask = MyUtils.GetLayerMaskByString("Enemy");
    

            int layerMask = (1 << LayerMask.NameToLayer("Enemy") | (1 << LayerMask.NameToLayer("Tile")) | (1 << LayerMask.NameToLayer("ItemTable")));



            Ray2D ray = new Ray2D(firePos, fireDirection);

            RaycastHit2D hit = Physics2D.Raycast(firePos, fireDirection, fireDistance, layerMask);

            if (hit == true)
            {
                CharacterInfo characterInfo = hit.transform.gameObject.GetComponent<CharacterInfo>();

                if (characterInfo != null)
                    characterInfo.GetDamage(damage);

                //라인 그려주기
                DrawLiner line = ObjectManager.Instance.linePool.GetItem();
                if (line != null)
                {
                    line.Initialize(firePos, hit.point);
                }

            }

        

           


        }
    }

}