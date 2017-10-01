using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 정보,점수,콤보 등등 저장
/// </summary>
public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager Instance;

    [HideInInspector]
    public CharacterBase player;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {

            CharacterBase playerScript;
            playerScript = playerObj.GetComponent<CharacterBase>();

            if (playerScript != null)
                player = playerScript;
        }


        return;
    }

    public void ResetPlayerPosit()
    {
        if (player != null)
            player.transform.position = Vector3.zero;
    }



}
