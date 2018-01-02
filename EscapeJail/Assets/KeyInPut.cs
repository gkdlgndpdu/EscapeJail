using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KeyInPut : MonoBehaviour
{
    [SerializeField]
    private UnityEvent linkFunc;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (linkFunc != null)
                linkFunc.Invoke();
        }
	}
}
