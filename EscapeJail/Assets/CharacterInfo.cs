using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterCondition
{
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


    //화상
    protected float fireSustainmentTime = 5f;
    protected float fireCount = 0f;

    //중독
    protected float poisonSustainmentTime = 5f;
    protected float poisonCount = 0f;

    protected List<CharacterCondition> conditionList = new List<CharacterCondition>();

    protected Dictionary<CharacterCondition, CharacterStateEffect> effectDic = new Dictionary<CharacterCondition, CharacterStateEffect>();

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
        if (effectDic.ContainsKey(CharacterCondition.InFire) == true)
        {
            effectDic[CharacterCondition.InFire].CountReset();
        }


        if (HasCondition(CharacterCondition.InFire) == true) return;


        //처음이다
        AddCondition(CharacterCondition.InFire);
        StartCoroutine(FireDamage());


        SetEffect(fireSustainmentTime, CharacterCondition.InFire);     

    }

    public void SetPoision()
    {
        //중복으로 들어왔을때 처리
        poisonCount = 0f;
        if (effectDic.ContainsKey(CharacterCondition.InPoison) == true)
        {
            effectDic[CharacterCondition.InPoison].CountReset();
        }

        if (HasCondition(CharacterCondition.InPoison) == true) return;

        //처음이다
        AddCondition(CharacterCondition.InPoison);
        StartCoroutine(PoisonDamage());

        SetEffect(fireSustainmentTime, CharacterCondition.InPoison);


    }

    private void SetEffect(float sustatinmentTime, CharacterCondition condition,float size =3.5f)
    {
        CharacterStateEffect effect = ObjectManager.Instance.characterStatePool.GetItem();
        if (effect != null)
        {
            effect.Initialize(sustatinmentTime, size, this.transform, condition);
            effect.transform.localPosition = Vector3.zero;

            effectDic.Add(condition, effect);

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
                effectDic.Remove(CharacterCondition.InFire);
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
                effectDic.Remove(CharacterCondition.InPoison);
                yield break;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
