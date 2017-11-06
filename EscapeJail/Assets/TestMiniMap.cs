using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMiniMap : MonoBehaviour
{
    Transform target;
    // Use this for initialization
    void Start ()
    {
        if (target == null)
            target = GamePlayerManager.Instance.player.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
