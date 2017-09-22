using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 10; i++)
        {
            GameObject obj = new GameObject();
            obj.name = i.ToString();
            obj.transform.parent = this.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Delete))
        {

            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);


        }
    }
}
