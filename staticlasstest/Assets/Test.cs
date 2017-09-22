using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {


    public static Test Instance;
    int k = 1;
    private void Awake()
    {
        if(Instance==null)
        Instance = this;

        Debug.Log(k);
        k++;
    }  
}
