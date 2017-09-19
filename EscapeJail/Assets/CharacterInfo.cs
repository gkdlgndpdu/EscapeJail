using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    protected int hp=0;
    protected int hpMax=0;


    public virtual void GetDamage(int damage)
    {
        //자식에서 구현!
    }

    protected void SetHp(int hp)
    {
        this.hp = hp;
        hpMax = hp;
    }
  

}
