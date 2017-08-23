using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private bool check = false;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (check == false)
        {
            Debug.Log("Enter");
            check = true;
        }
    }

    private void OnEnable()
    {
        check = false;
    }
}
