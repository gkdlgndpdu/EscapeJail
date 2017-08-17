using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



}
