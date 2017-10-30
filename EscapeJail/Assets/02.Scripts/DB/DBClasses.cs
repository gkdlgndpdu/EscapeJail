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


//대분류
public class ItemProbabilityDB
{
    public int Probability;
    public string Discription;
    public ItemProbabilityDB(int Probability,string Discription)
    {
        this.Probability = Probability;
        this.Discription = Discription;
    }
}


//중분류
public class ItemDB
{
    public int Value;
    public string Discription;

    public ItemDB(int Value,string Discription)
    {     
        this.Value = Value;
        this.Discription = Discription;
    }
}

