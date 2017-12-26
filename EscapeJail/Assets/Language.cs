using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanguageType
{
    Korean,
    English
}

public class Language : MonoBehaviour
{
    public static Language Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public Font KoreanFont;
    public Font EnglishFont;


    public void ChangeAllTexts(LanguageType type)
    {
        Localization[] components = Resources.FindObjectsOfTypeAll<Localization>();
        if (components == null) return;

        for(int i = 0; i < components.Length; i++)
        {
            components[i].SetTextByLangugage();
        }
    }
    

}
