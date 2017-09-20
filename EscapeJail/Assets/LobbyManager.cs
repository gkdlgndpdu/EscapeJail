using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public void GameStart()
    {
        Application.LoadLevelAsync(1);
    }

}
