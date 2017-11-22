using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
  

    [SerializeField]
    private List<Renderer> skyList;

    public float loopSpeed = 0.5f;

    void Update ()
    {

        LoopSky();

    }

    void LoopSky()
    {
        Vector2 offset = new Vector2(Time.time * loopSpeed, 0);
        for(int i = 0; i < skyList.Count; i++)
        {
            skyList[i].material.mainTextureOffset = -offset;
        }
        


    }
}
