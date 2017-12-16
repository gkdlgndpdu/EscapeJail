using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterCondition
{
    InFire,
    InPoison,
    ConditionEnd

}
/// <summary>
/// 해당클래스가 달려있는 오브젝트의 체력,상태(불,독  등) 관리
/// </summary>
public class CharacterInfo : MonoBehaviour
{
    protected int hp = 0;
    protected int hpMax = 0;

    //상태이상 면역이신지요
    protected bool isImmuneAnyState = false;

    //화상
    protected float fireSustainmentTime = 5f;
    protected float fireCount = 0f;

    //중독
    protected float poisonSustainmentTime = 5f;
    protected float poisonCount = 0f;

    protected List<CharacterCondition> conditionList = new List<CharacterCondition>();

    protected Dictionary<CharacterCondition, CharacterStateEffect> effectDic = new Dictionary<CharacterCondition, CharacterStateEffect>();

    protected bool isStun = false;
    protected bool isPushed = false;
    protected bool isDead = false;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    public virtual void GetDamage(int damage)
    {


    }

    /// <summary>
    /// 스턴 설정
    /// </summary>
    /// <param name="OnOff">true = 스턴 o /false = 스턴 x </param>
    public virtual void SetStun(bool OnOff)
    {

    }

    public virtual void SetPush(Vector3 pushPoint, float pushPower, int damage)
    {

    }


    protected void AddCondition(CharacterCondition condition)
    {
        if (conditionList == null) return;
        if (conditionList.Contains(condition) == true) return;
        conditionList.Add(condition);
    }

    protected void RemoveCondition(CharacterCondition condition)
    {
        if (conditionList == null) return;
        if (conditionList.Contains(condition) == false) return;
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

    public void SetHp(int hp)
    {
        if (NowSelectPassive.Instance.HasPassive(PassiveType.Littlelove) == true)
        {
            hp += 2;
        }
        this.hp = hp;
        hpMax = hp;
    }

    public void SetFire()
    {
        if (isImmuneAnyState == true) return;

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

    public void SetPoison()
    {
        if (isImmuneAnyState == true) return;

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

    private void SetEffect(float sustatinmentTime, CharacterCondition condition, float size = 3.5f)
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
    protected void AllStateClear()
    {
        StopCoroutine("PoisonDamage");
        StopCoroutine("FireDamage");
        for(int i = 0; i < (int)CharacterCondition.ConditionEnd; i++)
        {
            RemoveCondition((CharacterCondition)i);
        }
        //이펙트 다꺼줌
        foreach (KeyValuePair<CharacterCondition,CharacterStateEffect> data in effectDic)
        {
            data.Value.gameObject.SetActive(false);
        }
        effectDic.Clear();

    }

    protected void VampiricGunEffect()
    {
        //흡혈의낫
        if (NowSelectPassive.Instance.HasPassive(PassiveType.VampiricGun) == true)
        {
            if (MyUtils.GetPercentResult(50) == true)
                GamePlayerManager.Instance.player.GetHp(1);

        }

    }
}
