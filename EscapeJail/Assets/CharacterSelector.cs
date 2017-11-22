using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{

    // Use this for initialization
    public void StartSoldier()
    {
        SetCharacter(CharacterType.Soldier);
        ChangeScene();
    }

    public void StartScientist()
    {
        SetCharacter(CharacterType.Scientist);
        ChangeScene();
    }

    private void SetCharacter(CharacterType characterType)
    {
        PlayerPrefs.SetInt(GameConstants.CharacterKeyValue, (int)characterType);
    }

    private void ChangeScene()
    {    
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SceneName.GameScene);

    }
}
