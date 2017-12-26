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
    private LanguageType nowLanguage;
    public LanguageType NowLanguage
    {
        get
        {
            return nowLanguage;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            int languageKey = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);
            if (languageKey == 0)
            {
                nowLanguage = LanguageType.Korean;
            }
            else if(languageKey == 1)
            {
                nowLanguage = LanguageType.English;
            }
        }
    }
    public Font KoreanFont;
    public Font EnglishFont;


    public void ChangeAllTexts(LanguageType type)
    {
        nowLanguage = type;

        Localization[] components = Resources.FindObjectsOfTypeAll<Localization>();
        if (components == null) return;

        for(int i = 0; i < components.Length; i++)
        {
            components[i].SetTextByLangugage();
        }
    }
    

}
