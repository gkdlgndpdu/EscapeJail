using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireStyle
{
    Auto,
    Manual

}
//모든 클래스중 가장먼저 호출(awake)
public static class GameOption 
{
    private static FireStyle fireStyle = FireStyle.Auto;

    public static FireStyle FireStyle
    {
        get
        {
            return fireStyle;
        }
    }

    public static void ChangeFireStype(FireStyle style)
    {
        fireStyle = style;
    }


}
