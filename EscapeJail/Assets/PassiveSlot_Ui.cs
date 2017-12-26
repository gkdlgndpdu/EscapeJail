using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PassiveSlot_Ui : MonoBehaviour
{
    public PassiveType passiveType;    
    private bool hasItem = false;
    private bool isSelected = false;
    private PassiveDB passiveDB;
 
    private PassiveSelectScreen parent;

    [SerializeField]
    private Text text;

    [SerializeField]
    private Image selectIcon;
    [SerializeField]
    private Image mask;

    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Text priceText;

    private int originSize;

    private void Awake()
    {       
     

        if (selectIcon != null)
            selectIcon.gameObject.SetActive(false);

        if(text!=null)
        originSize = text.fontSize;
    }

    public void Initialize(PassiveType passiveType, PassiveDB data, PassiveSelectScreen parent)
    {
        if (data == null) return;

        this.passiveDB = data;
        this.passiveType = passiveType;
        this.hasItem = data.hasPassive;
        this.parent = parent;

        SetCanUse(hasItem);
        SetIcon();
        if (Language.Instance.NowLanguage == LanguageType.Korean)
        {
            SetText(data.koreanDescription);
            SetPrice(data.koreanName, data.price);


            text.font = Language.Instance.KoreanFont;
            //text.fontSize += 15;
            priceText.font = Language.Instance.KoreanFont;
            priceText.fontSize = originSize + 15;
        }
        else
        {
            SetText(data.englishDescription);
            SetPrice(data.englishName, data.price);
            priceText.fontSize = originSize+ 7;
        }
    
   
    }
    private void SetPrice(string name,int price)
    {
        if (priceText == null) return;

        if (Language.Instance.NowLanguage == LanguageType.Korean)
        {
            priceText.text = string.Format("{0} {1} 메달", name, price.ToString());
        }
        else
        {
            priceText.text = string.Format("{0} Price : {1} Medals", name, price.ToString());
        }
    }

    private void SetIcon()
    {
        if (iconImage == null) return;

        Sprite loadSprite = Resources.Load<Sprite>("Sprites/icon/Passive/" + passiveType.ToString());
        if (loadSprite != null)
            iconImage.sprite = loadSprite;
    }

    private void SetText(string description)
    {
        if (text == null) return;
        text.text = string.Format("{0}", description);
    }

   

    private void SetCanUse(bool OnOff)
    {        
        if (mask != null)
            mask.gameObject.SetActive(!OnOff);
    }

    public void SetSelect(bool OnOff)
    {
        if (selectIcon != null) 
            selectIcon.gameObject.SetActive(OnOff);

        if (OnOff == true)
        {
            //   PlayerPrefs.SetInt(PlayerPrefKeys.PassiveKeyValue, (int)passiveType);
            NowSelectPassive.Instance.AddPassive(this.passiveType);
        }
        else if (OnOff == false)
        {
            NowSelectPassive.Instance.RemovePassive(this.passiveType);
        }

    }

    //선택되면 들어옴
    public void OnClick()
    {
        if (passiveDB == null) return;
        if (passiveDB.hasPassive == false)
        {
            //구매창 보여줌
            if (parent != null)
                parent.OpenPassiveBuyScreen(this.passiveType, passiveDB);
            return;
        }
        if (parent != null)
            parent.RegistSelectSlot(this);
    }

    private void BuyPassiveItem()
    {

    }
}
