using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class CharacterSlot_Ui : MonoBehaviour
{
    private Image characterImage;
    private CharacterType characterType;

    private void Awake()
    {
        characterImage = GetComponent<Image>();
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

        //임시코드
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SceneName.GameScene);
        //임시코드
    }


}
