using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Image))]
public class CharacterSlot_Ui : MonoBehaviour
{
    private Image characterImage;
    private CharacterType characterType;
    private Action passiveWindowOnFunc;
    private void Awake()
    {
        characterImage = GetComponent<Image>();

        GameObject passiveSelectObj = GameObject.Find("PassiveSelect");
        if (passiveSelectObj != null)
        {
            PassiveSelect passiveSelect = passiveSelectObj.GetComponent<PassiveSelect>();
            if (passiveSelect != null)
            {
                passiveWindowOnFunc = passiveSelect.PassiveUiOnOff;
            }
        }
    }
    public void Initialize(CharacterType characterType)
    {
        this.characterType = characterType;
        if(characterImage!=null)
        {
            Sprite loadSprite = Resources.Load<Sprite>(string.Format("Sprites/Icons/{0}", characterType.ToString()));
            if (loadSprite != null)
            {
                if (characterImage != null)
                    characterImage.sprite = loadSprite;

            }
        }
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt(GameConstants.CharacterKeyValue, (int)characterType);

        if (passiveWindowOnFunc != null)
            passiveWindowOnFunc();   
    }


}
