using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTextTest : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        iTween.FadeTo(this.gameObject, iTween.Hash( "loopType", "pingPong", "alpha",0f));



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
