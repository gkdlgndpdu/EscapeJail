using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class CharacterSlot_Ui : MonoBehaviour
{
    private CharacterSelector parent;
    private Image characterImage;
    private CharacterDB characterDB;
    private Button button;

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
        button = GetComponent<Button>();


    }
    public void Initialize(CharacterType characterType, Action passiveUiOnOffFunc, CharacterSelector parent)
    {
        this.characterType = characterType;
        characterDB = DatabaseLoader.Instance.GetCharacterDB(CharacterType);

        if(characterDB!=null)
            SetButton(characterDB.hasCharacter);

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
    public void SetButton(bool OnOff)
    {
        if (button == null || characterImage == null) return;
        if (OnOff == false)
        {
         //   button.enabled = false;
            characterImage.color = Color.black;
        }
        else if (OnOff == true)
        {
          //  button.enabled = true;
            characterImage.color = Color.white;
        }
    }

    public void SelectCharacter()
    {
        if (characterDB == null) return;

        if (characterDB.hasCharacter == true)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.CharacterKeyValue, (int)characterType);
            if (parent != null)
                parent.RegistSelectSlot(this);
        }
        else if (characterDB.hasCharacter == false)
        {
            if (parent != null)
                parent.ShowHowToGet(this);
        }


        //if (passiveWindowOnFunc != null)
        //    passiveWindowOnFunc();   
    }


}
