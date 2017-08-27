using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{

 
    private Transform target;


    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (target != null)
            this.transform.position = target.position;
    }
}
