using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Soldier,
    Scientist,
    Defender,
    Sniper,
    Engineer,
    Trader,
    CharacterEnd
}

/// <summary>
/// 플레이어 정보,점수,콤보 등등 저장
/// </summary>
public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager Instance;
    private CharacterType playerName = CharacterType.Scientist;
    public CharacterType PlayerName
    {
        get
        {
            return playerName;
        }
    }

    [HideInInspector]
    public CharacterBase player;

    public ScoreCounter scoreCounter;

    private void Awake()
    {
        if (Instance == null)
        {           
            Instance = this;        
        }
        else if (Instance != null)
        {
            Instance = null;
            Instance = this;
        }


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
            playerObj.transform.localPosition = Vector3.zero;
            CharacterBase playerScript;
            playerScript = playerObj.GetComponent<CharacterBase>();

            if (playerScript != null)
            {
                player = playerScript;
                scoreCounter = new ScoreCounter();
            }
        }
    }

    public void ResetPlayerPosit()
    {
        if (player != null)
            player.transform.position = Vector3.zero;
    }



}
