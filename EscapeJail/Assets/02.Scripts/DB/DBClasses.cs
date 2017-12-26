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
    public int level;

    public WeaponDB(int Probability, string Description, int level)
    {
        this.Probability = Probability;
        this.Description = Description;
        this.level = level;
    }
}


public class PassiveDB
{
  //  public PassiveType passiveType = PassiveType.None;
    public bool hasPassive = false;
    public int price;
    public string koreanDescription;
    public string englishDescription;
    public string koreanName;
    public string englishName;

    public PassiveDB(bool hasPassive, int price,string koreanDescription, string englishDescription,string koreanName, string englishName)
    {
        this.hasPassive = hasPassive;
        this.price = price;
        this.koreanDescription = koreanDescription;
        this.englishDescription = englishDescription;
        this.koreanName = koreanName;
        this.englishName = englishName;

    }

}

public class CharacterDB
{
    public bool hasCharacter = false;
    public string skillNameEng;
    public string skillNameKor;
    public string descriptionEng;
    public string descriptionKor;
    public string howToGetEng;
    public string howToGetKor;

    public CharacterDB(bool hasCharacter,string skillNameEng,string skillNameKor,string descriptionEng,string descriptionKor,string howToGetEng,string howToGetKor)
    {
        this.hasCharacter = hasCharacter;
        this.skillNameEng = skillNameEng;
        this.skillNameKor = skillNameKor;
        this.descriptionEng = descriptionEng;
        this.descriptionKor= descriptionKor;
        this.howToGetEng = howToGetEng;
        this.howToGetKor = howToGetKor;
    }
}

public class LocalizationDB
{
    public string Korean;
    public string English;

    public LocalizationDB(string Korean,string English)
    {
        this.Korean = Korean;
        this.English = English;
    }
}
