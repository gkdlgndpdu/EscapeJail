using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace weapon
{
    public class CardCase : Weapon
    {
        //리볼버 반동
        private float reBoundValue = 0f;

        private float spadeSpeed =15f;
        private int spadeDamage= 2;

        private float heartSpeed =10f;
        private int heartDamage =5;

        private float cloverSpeed =10f;
        private int cloverDamage = 2;

        private float diamondSpeed =15f;
        private int diamondDamage =2;

        public CardCase()
        {
            weapontype = WeaponType.CardCase;
            SetWeaponKind(WeaponKind.Special);
            fireDelay = 0.4f;
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

            SoundManager.Instance.PlaySoundEffect("cardThrow");

            CardCaseCard card = GamePlayerManager.Instance.player.NowCard;
            if (card == null) return;

            switch (card.NowCardType)
            {
                case CardType.Spade:
                    {
                        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                        if (bullet != null)
                        {

                            Vector3 fireDir = fireDirection;                  
                            fireDir.Normalize();
                            bullet.Initialize(firePos + fireDir * 0.1f, fireDir, spadeSpeed, BulletType.PlayerBullet, 1.5f, spadeDamage);
                            bullet.InitializeImage("Spade", false);
                            bullet.SetEffectName("revolver");
                            bullet.RotateBullet();
                            bullet.SetBloom(false);
                        }
                    }
                    break;
                case CardType.Heart:
                    {
                        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                        if (bullet != null)
                        {
                            Vector3 fireDir = fireDirection;                           
                            bullet.Initialize(firePos, fireDir.normalized, heartSpeed, BulletType.PlayerBullet, 1.5f, heartDamage);
                            bullet.InitializeImage("Heart", false);
                            bullet.SetEffectName("bazooka", 3f);
                            bullet.SetBloom(false);
                            bullet.RotateBullet();
                            bullet.SetExplosion(1.5f);

                        }
                    }
                    break;
                case CardType.Clover:
                    {
                        Vector3 fireDir = fireDirection;
                        for (int i = 0; i < 3; i++)
                        {
                            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                            if (bullet != null)
                            {                           
                                fireDir = Quaternion.Euler(0f, 0f, -10f + 10f * i) * fireDirection;
                                bullet.Initialize(firePos, fireDir.normalized, cloverSpeed, BulletType.PlayerBullet, 1.5f, cloverDamage, 1f);
                                bullet.InitializeImage("Clover", false);
                                bullet.SetEffectName("revolver");
                                bullet.SetBloom(false);
                                bullet.RotateBullet();

                            }
                        }
                    }
                    break;
                case CardType.Diamond:
                    {
                        Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                        if (bullet != null)
                        {

                            Vector3 fireDir = fireDirection;                      
                            fireDir.Normalize();
                            bullet.Initialize(firePos + fireDir * 0.1f, fireDir, diamondSpeed, BulletType.PlayerBullet, 1.5f, diamondDamage);
                            bullet.InitializeImage("Diamond", false);
                            bullet.SetEffectName("revolver");
                            bullet.SetBloom(false);
                            bullet.SetDestroyByCollision(false,false);
                            bullet.RotateBullet();


                        }

                    }
                    break;
            }
        }

    }
}