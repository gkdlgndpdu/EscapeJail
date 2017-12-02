using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PassiveSlot_Ui : MonoBehaviour
{
    private PassiveType passiveType;
    private bool hasItem = false;
    private bool isSelected = false;
    private PassiveDB data;

    [SerializeField]
    private Button button;
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

    private void Awake()
    {       
        button = GetComponent<Button>();

        if (selectIcon != null)
            selectIcon.gameObject.SetActive(false);
    }

    public void Initialize(PassiveType passiveType, PassiveDB data, PassiveSelectScreen parent)
    {
        if (data == null) return;

        this.data = data;
        this.passiveType = passiveType;
        this.hasItem = data.hasPassive;
        this.parent = parent;

        SetButton(hasItem);
        SetIcon();
        SetText(data.howToGet, data.description);
        SetPrice(data.price);
    }
    private void SetPrice(int price)
    {
        if (priceText == null) return;
        priceText.text = string.Format("{0} 훈장", price.ToString());
    }

    private void SetIcon()
    {
        if (iconImage == null) return;

        Sprite loadSprite = Resources.Load<Sprite>("Sprites/Icons/Passive/" + passiveType.ToString());
        if (loadSprite != null)
            iconImage.sprite = loadSprite;
    }

    private void SetText(string howToGet,string description)
    {
        if (text == null) return;
        text.text = string.Format("{0} \n {1}", description, howToGet);
    }

    private void SetPrice()
    {

    }

    private void SetButton(bool OnOff)
    {
        if (button != null)
        button.enabled = OnOff;

        if (mask != null)
            mask.gameObject.SetActive(!OnOff);


    }

    public void SetSelect(bool OnOff)
    {
        if (selectIcon != null) 
            selectIcon.gameObject.SetActive(OnOff);

        if (OnOff == true)
        {
            PlayerPrefs.SetInt(GameConstants.PassiveKeyValue, (int)passiveType);
        }

    }

    //선택되면 들어옴
    public void OnClick()
    {
        if (data == null) return;
        if (data.hasPassive == false) return;
        if (parent != null)
            parent.RegistSelectSlot(this);
    }
}
