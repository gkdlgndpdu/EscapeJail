using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomItween : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
       Hashtable hash = new Hashtable();
        hash.Add("scale", new Vector3(0.8f, 0.8f, 0f));
        hash.Add("time", 0.5f);
        hash.Add("loopType", "pingpong");
        hash.Add("easetype", "linear");

        iTween.ScaleTo(this.gameObject, hash);

     

    }


}
