using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Debug.Log("world : " + this.transform.position);
        Debug.Log("local : " + this.transform.localPosition);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
