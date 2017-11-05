using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ResourceDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {

      
            


    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Texture2D[] sortedAll = Resources.FindObjectsOfTypeAll<Texture2D>();

            List<Texture2D> list = new List<Texture2D>(sortedAll);

            foreach (Texture2D data in list)
            {
                Debug.Log(data.name + " : " + Profiler.GetRuntimeMemorySize(data));
            }
        }
	}
}
