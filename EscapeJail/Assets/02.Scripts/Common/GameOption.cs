using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//모든 클래스중 가장먼저 호출(awake)
public static class GameOption 
{
    
    static GameOption()
    {
        Debug.Log("게임옵션 생성자 들어옴");
    }

}
