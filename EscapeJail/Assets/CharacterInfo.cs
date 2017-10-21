using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterCondition
{
    Normal,
    InFire,
    InPoison
}
/// <summary>
/// 해당클래스가 달려있는 오브젝트의 체력,상태(불,독  등) 관리
/// </summary>
public class CharacterInfo : MonoBehaviour
{
    protected int hp = 0;
    protected int hpMax = 0;

    protected CharacterStateEffect nowStateEffect;

    //화상
    protected float fireSustainmentTime = 5f;
    protected float fireCount = 0f;

    //중독
    protected float poisonSustainmentTime = 5f;
    protected float poisonCount = 0f;

    protected List<CharacterCondition> conditionList = new List<CharacterCondition>();

    public virtual void GetDamage(int damage)
    {
        //자식에서 구현!
    }

    protected void AddCondition(CharacterCondition condition)
    {
        if (conditionList == null) return;
        conditionList.Add(condition);
    }

    protected void RemoveCondition(CharacterCondition condition)
    {
        if (conditionList == null) return;
        conditionList.Remove(condition);
    }

    protected bool HasCondition(CharacterCondition condtion)
    {
        if (conditionList == null) return false;  
        return conditionList.Contains(condtion);
    }

    protected bool IsConditionNormal()
    {
        if (conditionList == null) return true;
        if (conditionList.Count == 0) return true;
        return false;
    }

    protected void ResetCondition()
    {
        if (conditionList == null) return;
        conditionList.Clear();

    }

    protected void SetHp(int hp)
    {
        this.hp = hp;
        hpMax = hp;
    }

    public void SetFire()
    {
        //중복으로 들어왔을때 처리
        fireCount = 0f;
        if (nowStateEffect != null)
            nowStateEffect.CountReset();        
        if (HasCondition(CharacterCondition.InFire)==true) return;


        //처음이다
        AddCondition(CharacterCondition.InFire);
        StartCoroutine(FireDamage());
        //이펙트
        if (nowStateEffect == null)
        {
            CharacterStateEffect effect = ObjectManager.Instance.characterStatePool.GetItem();
            if (effect != null)
            {
                effect.Initialize(fireSustainmentTime, 3.5f, this.transform,SpecialBulletType.Fire);
                effect.transform.localPosition = Vector3.zero;
                effect.transform.parent = this.transform;
                nowStateEffect = effect;
            }
        }

    }

    public void SetPoision()
    {
        //중복으로 들어왔을때 처리
        poisonCount = 0f;
        if (nowStateEffect != null)
            nowStateEffect.CountReset();
        if (HasCondition(CharacterCondition.InPoison)==true) return;

        //처음이다
        AddCondition(CharacterCondition.InPoison);
        StartCoroutine(PoisonDamage());
        //이펙트
        if (nowStateEffect == null)
        {
            CharacterStateEffect effect = ObjectManager.Instance.characterStatePool.GetItem();
            if (effect != null)
            {
                //
                //독세팅
                //
                effect.Initialize(fireSustainmentTime, 3.5f, this.transform,SpecialBulletType.Poision);
                effect.transform.localPosition = Vector3.zero;
                effect.transform.parent = this.transform;
                nowStateEffect = effect;
            }
        }
    }


    protected virtual IEnumerator FireDamage()
    {
        while (true)
        {
            fireCount += 1f;
            GetDamage(GameConstants.fireTicDamage);
            if (fireCount >= fireSustainmentTime)
            {
                RemoveCondition(CharacterCondition.InFire);
                nowStateEffect = null;
                yield break;
            }

            yield return new WaitForSeconds(1.0f);
        }

    }

    protected virtual IEnumerator PoisonDamage()
    {
        while (true)
        {
            poisonCount += 1f;
            GetDamage(GameConstants.poisonTicDamage);
            if (poisonCount >= poisonSustainmentTime)
            {
                RemoveCondition(CharacterCondition.InPoison);
                nowStateEffect = null;
                yield break;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
