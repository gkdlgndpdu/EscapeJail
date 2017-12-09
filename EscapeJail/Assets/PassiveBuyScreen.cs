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
    
    private int price;
    public void Initialize(PassiveType passiveType,PassiveDB passiveDb)
    {
        this.passiveType = passiveType;
        //아이콘세팅

        //텍스트 세팅
        this.price = passiveDb.price;
        SetIcon(passiveType.ToString());
        SetText(string.Format("{0} 메달 입니다 구매하시겠습니까?", passiveDb.price));
    }
    private void SetText(string text)
    {
        if (description == null) return;

        description.text = text;

    }
    private void SetIcon(string passiveName)
    {
        if (icon == null) return;

        Sprite loadSprite = Resources.Load<Sprite>("Sprites/Icons/Passive/" + passiveName);
        if (loadSprite != null)
            icon.sprite = loadSprite;
    }

  
}
