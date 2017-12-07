using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomItween : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetSize = new Vector3(0.8f, 0.8f, 0f);
    [SerializeField]
    private float speed = 0.5f;
    // Use this for initialization
    void Start ()
    {
       Hashtable hash = new Hashtable();
        hash.Add("scale", targetSize);
        hash.Add("time", speed);
        hash.Add("loopType", "pingpong");
        hash.Add("easetype", "linear");

        iTween.ScaleTo(this.gameObject, hash);     

    }


}
