using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterCondition
{
    Normal,
    InFire
}
public class CharacterInfo : MonoBehaviour
{
    protected int hp=0;
    protected int hpMax=0;
    protected MonsterCondition monsterCondition = MonsterCondition.Normal;

    public virtual void GetDamage(int damage)
    {
        //자식에서 구현!
    }

    protected void SetHp(int hp)
    {
        this.hp = hp;
        hpMax = hp;        
    }

    public void SetFire()
    {
        if (monsterCondition == MonsterCondition.InFire == true) return;
        StartCoroutine(testFireRoutine());
    }

    protected virtual IEnumerator testFireRoutine()
    {
        monsterCondition = MonsterCondition.InFire;
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("불피해");
            this.hp -= 5;
            yield return new WaitForSeconds(1.0f);
        }

        monsterCondition = MonsterCondition.Normal;
    }
  

}
