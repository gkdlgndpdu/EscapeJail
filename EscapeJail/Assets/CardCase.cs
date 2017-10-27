using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace weapon
{
    public class CardCase : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 5f;

        public CardCase()
        {
            weapontype = WeaponType.CardCase;


            bulletSpeed = 10f;
            fireDelay = 0.3f;
            SetAmmo(100);   
            needBulletToFire = 1;

            //무기 크기
            weaponScale = Vector3.one *1;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            //첫줄에 이거 필요 
            if (canFire() == false) return;

            //총알달게
            useBullet();
            //딜레이 흐르게
            FireDelayOn();
            //애니메이션재생
            PlayFireAnim();


            Vector3 fireDir = fireDirection;
            for(int i = 0; i < 3; i++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                                                  //x,y,z, 회전 값을 넣고 * 원래 방향벡터     
                    Vector3 fd = Quaternion.Euler(0f, 0f, -15f  + ( 15f * i)) * fireDir;                              

                    //총알 기본속성 발사 위치 ,방향 ,피아식별 등등
                    bullet.Initialize(firePos, fd.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f);

                    //총알 이미지 설정 앞에인자는 파일이름 뒤에는 애니메이션 유무
                    bullet.InitializeImage("cardcase_4", false);

                    //총알 터졌을때 이펙트 설정
                    bullet.SetEffectName("revolver");

                    bullet.SetBloom(false, Color.white);
                }
            }

               
            

      

        }
    }

}