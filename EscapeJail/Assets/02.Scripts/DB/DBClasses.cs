using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDB
{
    public int Probability;
    public int Hp;

    public MonsterDB( int Probability, int Hp)
    {   
        this.Probability = Probability;
        this.Hp = Hp;
    }
}