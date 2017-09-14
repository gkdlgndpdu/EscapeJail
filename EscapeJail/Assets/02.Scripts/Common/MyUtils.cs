using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtils
{
    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        float returnvalue = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        if (returnvalue < 0f)
        {
            returnvalue =returnvalue + 360f;
        }
        return returnvalue;
    }

    public static int GetLayerMaskByString(string maskName)
    {
        int layerMask = 1 << LayerMask.NameToLayer(maskName);

        return layerMask;
    }
 


}
