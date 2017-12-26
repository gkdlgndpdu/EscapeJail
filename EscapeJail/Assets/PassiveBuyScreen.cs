using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PassiveBuyScreen : MonoBehaviour
{
    [SerializeField]
    private Text description;
    [SerializeField]
    private Image icon;

    private PassiveType passiveType;

    private int originTextSize;

    private int price;
    private void Awake()
    {
        if(description!=null)
        originTextSize = description.fontSize;
    }

    public void Initialize(PassiveType passiveType, PassiveDB passiveDb)
    {
        this.passiveType = passiveType;
        //아이콘세팅

        //텍스트 세팅
        this.price = passiveDb.price;
        SetIcon(passiveType.ToString());
    
            SetText(passiveDb);
     
    }
    private void SetText(PassiveDB passiveDb)
    {
        if (description == null) return;
        if (passiveDb == null) return;




        int languageKey = PlayerPrefs.GetInt(PlayerPrefKeys.LanguageKey, (int)LanguageType.Korean);

        //한글
        if (languageKey == 0)
        {
            description.text = string.Format("{0} 메달 입니다 구매하시겠습니까?", passiveDb.price);
            description.font = Language.Instance.KoreanFont;
            description.fontSize = originTextSize + 15;
        }
        else
        {
            description.text = string.Format("price is {0} medals\n Would you like to purchase it?", passiveDb.price);
            description.font = Language.Instance.EnglishFont;
            description.fontSize = originTextSize;
        }
    }
    private void SetIcon(string passiveName)
    {
        if (icon == null) return;

        Sprite loadSprite = Resources.Load<Sprite>("Sprites/icon/Passive/" + passiveName);
        if (loadSprite != null)
            icon.sprite = loadSprite;
    }


}
