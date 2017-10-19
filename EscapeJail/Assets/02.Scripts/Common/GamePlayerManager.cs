using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Soldier,
    Scientist
}

/// <summary>
/// 플레이어 정보,점수,콤보 등등 저장
/// </summary>
public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager Instance;
    private CharacterType playerName = CharacterType.Scientist;

    [HideInInspector]
    public CharacterBase player;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        

        MakePlayer();
    }

    private void MakePlayer()
    {

        playerName = (CharacterType)PlayerPrefs.GetInt(GameConstants.CharacterKeyValue, (int)CharacterType.Soldier);


        GameObject playerPrefab = null;

        playerPrefab = Resources.Load<GameObject>(string.Format("Prefabs/Characters/{0}", playerName.ToString()));

        if (playerPrefab != null)
        {
            GameObject playerObj = Instantiate(playerPrefab, null);

            CharacterBase playerScript;
            playerScript = playerObj.GetComponent<CharacterBase>();

            if (playerScript != null)
                player = playerScript;
        }


    }

    public void ResetPlayerPosit()
    {
        if (player != null)
            player.transform.position = Vector3.zero;
    }



}
