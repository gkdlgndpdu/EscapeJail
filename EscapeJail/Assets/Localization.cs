using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class Localization : MonoBehaviour
{
    private Text text;

    [SerializeField]
    private string key;

    private int originTextSize;
    private Vector3 originPosition;
    [SerializeField]
    private bool SetTextBigger = true;
    [SerializeField]
    private bool SetTextSmaller = false;
    private bool FixPosition = false;

    private void Awake()
    {
        text = GetComponent<Text>();
        originTextSize = text.fontSize;
        originPosition = this.transform.localPosition;
        SetTextByLangugage();
    }

    public void SetTextByLangugage()
    {
        if (text == null) return;
        LocalizationDB data = DatabaseLoader.Instance.GetLanguage(key);
        if (data == null) return;


        int languageKey = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);

        //한글
        if (languageKey == 0)
        {
            text.font = Language.Instance.KoreanFont;
            text.text = data.Korean;
            if (SetTextBigger == true)
                text.fontSize = originTextSize + 15;

            if (FixPosition == false)
                this.transform.localPosition = originPosition + Vector3.up * 10f;
        }
        //영어
        else
        {

            text.font = Language.Instance.EnglishFont;
            text.text = data.English;

            if (SetTextSmaller == true)
                text.fontSize = originTextSize-15;
            else
                text.fontSize = originTextSize;



            this.transform.localPosition = originPosition;
        }
    }

}
