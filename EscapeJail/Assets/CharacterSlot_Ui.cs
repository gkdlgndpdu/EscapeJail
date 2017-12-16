using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Image))]
public class CharacterSlot_Ui : MonoBehaviour
{
    private CharacterSelector parent;
    private Image characterImage;
    private CharacterType characterType;
    public CharacterType CharacterType
    {
        get
        {
            return characterType;
        }
    }
    private Action passiveWindowOnFunc;
    private void Awake()
    {
        characterImage = GetComponent<Image>();

    
    }
    public void Initialize(CharacterType characterType, Action passiveUiOnOffFunc, CharacterSelector parent)
    {
        this.characterType = characterType;
        this.parent = parent;
        passiveWindowOnFunc = passiveUiOnOffFunc;
        if (characterImage!=null)
        {
            Sprite loadSprite = Resources.Load<Sprite>(string.Format("Sprites/icon/{0}", characterType.ToString()));
            if (loadSprite != null)
            {
                if (characterImage != null)
                    characterImage.sprite = loadSprite;

            }
        }
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.CharacterKeyValue, (int)characterType);
        if (parent != null)
            parent.RegistSelectSlot(this);

        //if (passiveWindowOnFunc != null)
        //    passiveWindowOnFunc();   
    }


}
