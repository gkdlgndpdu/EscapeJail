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

    public static int GetLayerMaskExcludeName(string maskName)
    {
        int layerMask = (-1) - ((1 << LayerMask.NameToLayer(maskName)));
        return layerMask;
    }


    public static bool GetPercentResult(int percent)
    {
        percent = Mathf.Clamp(percent, 0, 100);

        int randNum = Random.Range(0, 101);

        return randNum <= percent;
    }

    public static PassiveType GetNowPassive()
    {
        return (PassiveType)PlayerPrefs.GetInt(GameConstants.PassiveKeyValue);
    }



    //public static Vector3 AbsVector(Vector3 value)
    //{
    //    float x = 0f;
    //    float y = 0f;
    //    float z = 0f;

    //    if (value.x > 0)
    //        x = value.x;
    //    else
    //        x = -value.x;

    //    if (value.y > 0)
    //        y = value.y;
    //    else
    //        y = -value.y;

    //    if (value.z > 0)
    //        z = value.z;
    //    else
    //        z = -value.z;

    //    return new Vector3(x, y, z);

    //} 



}
