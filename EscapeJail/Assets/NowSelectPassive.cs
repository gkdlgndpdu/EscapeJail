using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    easy,
    hard
}

public class NowSelectPassive : MonoBehaviour
{
    public static NowSelectPassive Instance;
    private Difficulty difficulty;
    public Difficulty NowDifficulty
    {
        get
        {
            return difficulty;
        }
    }

    public void SetDifficulty(Difficulty diff)
    {
        difficulty = diff;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            myPassive = new List<PassiveType>();
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    private List<PassiveType> myPassive;

    public void AddPassive(PassiveType passiveType)
    {
        if (myPassive == null) return;
        if (myPassive.Count > 2) return;
        myPassive.Add(passiveType);
    }
    public void RemovePassive(PassiveType passiveType)
    {
        if (myPassive == null) return;
        myPassive.Remove(passiveType);
    }

    public bool HasPassive(PassiveType passiveType)
    {
        if (myPassive == null) return false;
        return myPassive.Contains(passiveType);
    }

    public void ClearPassives()
    {
        if (myPassive == null) return;
        myPassive.Clear();
    }

}
