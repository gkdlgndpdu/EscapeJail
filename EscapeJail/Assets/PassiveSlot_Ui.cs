using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PassiveSlot_Ui : MonoBehaviour
{
    private PassiveType passiveType;
    private bool hasItem = false;
    private bool isSelected = false;

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

    private void Awake()
    {       
        button = GetComponent<Button>();

        if (selectIcon != null)
            selectIcon.gameObject.SetActive(false);
    }

    public void Initialize(PassiveType passiveType,bool hasItem,string howToGet,string description,PassiveSelectScreen parent)
    {
        this.passiveType = passiveType;
        this.hasItem = hasItem;
        this.parent = parent;

        SetButton(hasItem);
        SetIcon();
        SetText(howToGet, description);
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
        if (parent != null)
            parent.RegistSelectSlot(this);
    }
}
