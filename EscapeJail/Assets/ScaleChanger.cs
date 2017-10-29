using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    [SerializeField]
    private Transform parentTr;

    private void Awake()
    {

        if (parentTr != null)
        {
            Vector3 parentScale = parentTr.localScale;

            if (parentTr != null)
                this.transform.localScale = new Vector3(1f / parentScale.x, 1f / parentScale.y, 1f / parentScale.z);

        }
    }

 
}
