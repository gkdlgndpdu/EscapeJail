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
    public GameObject player;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;  
    }

    private void Start()
    {
        player = FindPlayer();
    }

    private GameObject FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public void ResetPlayerPosit()
    {
        if (player != null)
            player.transform.position = Vector3.zero;
    }



}
