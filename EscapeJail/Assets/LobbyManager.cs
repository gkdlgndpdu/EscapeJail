using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
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
        Application.LoadLevelAsync(1);


     
           

    }

}
