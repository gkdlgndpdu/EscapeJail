using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundOption
{
    public static float BgmVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("?");
        }
        set
        {

        }
    }

   
}
