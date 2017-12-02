using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDB
{
    public int Probability;
    public int Hp;

    public MonsterDB(int Probability, int Hp)
    {
        this.Probability = Probability;
        this.Hp = Hp;
    }
}


//대분류
public class ItemProbabilityDB
{
    public int Probability;
    public string Description;
    public ItemProbabilityDB(int Probability, string Description)
    {
        this.Probability = Probability;
        this.Description = Description;
    }
}


//중분류
public class ItemDB
{
    public int Value;
    public string Description;

    public ItemDB(int Value, string Description)
    {
        this.Value = Value;
        this.Description = Description;
    }
}

public class WeaponDB
{
    public int Probability;
    public string Description;

    public WeaponDB(int Probability, string Description)
    {
        this.Probability = Probability;
        this.Description = Description;
    }
}


public class PassiveDB
{
  //  public PassiveType passiveType = PassiveType.None;
    public bool hasPassive = false;
    public string description;
    public string howToGet;
    public int price;
    public PassiveDB(bool hasPassive, string description, string howToGet,int price)
    {
        this.hasPassive = hasPassive;
      //  this.passiveType = passiveType;
        this.description = description;
        this.howToGet = howToGet;
        this.price = price;

    }

}

