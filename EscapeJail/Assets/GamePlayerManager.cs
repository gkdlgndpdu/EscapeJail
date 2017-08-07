using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{
    public static GamePlayerManager Instance;

    public GameObject player;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        player = FindPlayer();
    }

    private GameObject FindPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }



}
