﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCam : MonoBehaviour
{

    private Transform target;
    [SerializeField]
    private float followSpeed =2;


    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (target != null)
            this.transform.position = Vector3.Lerp(this.transform.position,target.position,Time.fixedDeltaTime* followSpeed);
    }

}
