using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpecialBulletType
{
    Fire,
    PoisionGranade
}



public class SpecialBullet : Bullet
{

    private SpecialBulletType specialBulletType;


    private new void Awake()
    {
        base.Awake();

    }

    public void Initialize(Vector3 startPos, Vector3 moveDir, float moveSpeed, BulletType bulletType, SpecialBulletType specialBulletType, float bulletScale = 1f, int power = 1, float lifeTime = 5f)
    {
        base.Initialize(startPos, moveDir, moveSpeed, bulletType, bulletScale, power, lifeTime);
                
        float RotateAngle = MyUtils.GetAngle(Vector3.zero, moveDir) + 180f;

        //회전
        this.transform.rotation = Quaternion.Euler(0f, 0f, RotateAngle);
        
        this.specialBulletType = specialBulletType;
        SetBulletImage(this.specialBulletType);

    }

    //총알 애니메이션 설정하는곳
    private void SetBulletImage(SpecialBulletType specialBulletType)
    {
        switch (specialBulletType)
        {
            case SpecialBulletType.Fire:
                {
                     InitializeImage(specialBulletType.ToString(), true);              
                }
                break;
            case SpecialBulletType.PoisionGranade:
                {
                    InitializeImage(specialBulletType.ToString(), true);
                } break;
        }
    }





    private void FireDamage(Collider2D collision)
    {
        //충돌여부는 layer collision matrix로 분리해놓음
        if (collision.gameObject.CompareTag("Enemy") == true || collision.gameObject.CompareTag("Player"))
        {
            CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();

            if (characterInfo != null)
            {
                characterInfo.GetDamage(this.power);
                characterInfo.SetFire();
            }
        }
    }

    private void PoisionDamage(Collider2D collision)
    {
        //충돌여부는 layer collision matrix로 분리해놓음
        if (collision.gameObject.CompareTag("Enemy") == true || collision.gameObject.CompareTag("Player"))
        {
            CharacterInfo characterInfo = collision.gameObject.GetComponent<CharacterInfo>();

            if (characterInfo != null)
            {
                characterInfo.GetDamage(this.power);
                characterInfo.SetPoison();
            }
        }
    }





    private new void DamegeToItemTable(Collider2D collision)
    {
        ItemTable table = collision.gameObject.GetComponent<ItemTable>();
        if (table != null)
        {
            if (specialBulletType == SpecialBulletType.Fire)
                table.GetDamage(power * 5);
            else
                table.GetDamage(power);

        }
    }
    protected new void MultiTargetDamage()
    {
        int layerMask;
        if (bulletType == BulletType.PlayerBullet)
            layerMask = MyUtils.GetLayerMaskByString("Enemy");
        else
            layerMask = MyUtils.GetLayerMaskByString("Player");


        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius, layerMask);
        if (colls == null) return;

        for (int i = 0; i < colls.Length; i++)
        {
            CharacterInfo characterInfo = colls[i].gameObject.GetComponent<CharacterInfo>();
            if (specialBulletType == SpecialBulletType.PoisionGranade)
            {
                if (characterInfo != null)
                    characterInfo.SetPoison();
            }
            else
            {
                if (characterInfo != null)
                    characterInfo.GetDamage(power);
            }
       
   
        }

    }


    //다른 물체와의 충돌은 layer로 막아놓음
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        switch (explosionType)
        {
            case ExplosionType.single:
                {
                    SingleTargetDamage(collision);
                }
                break;
            case ExplosionType.multiple:
                {
                    MultiTargetDamage();
                }
                break;
        }


        if (collision.gameObject.CompareTag("ItemTable"))
        {
            DamegeToItemTable(collision);
        }


        if (specialBulletType == SpecialBulletType.Fire)
        {
            if (string.Equals(collision.gameObject.name, "Wall") == false)
                FireDamage(collision);
            else
                BulletDestroy();
        }       
        else
        {
            ////총알 삭제 및 이펙트 호출
            BulletDestroy();
        }

    }



}
