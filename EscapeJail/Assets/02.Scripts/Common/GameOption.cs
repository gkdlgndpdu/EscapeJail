using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//모든 클래스중 가장먼저 호출(awake)
public class GameOption : MonoBehaviour
{
    public static GameOption Instance;
    //몬스터,타일 정보가 들어있어연
  
    private StageData stageData;
    public StageData StageData
    {
        get
        {
            return stageData;
        }
    }
    private int nowStageLevel = 1;

    private void Awake()
    {
        if(Instance==null)
        Instance = this;

        Application.targetFrameRate = 60;

    
    } 

    public void LoadstageData()
    {
      Object obj = Resources.Load("StageData/Stage" + nowStageLevel.ToString());
        if (obj != null)
            stageData = (StageData)obj;

        nowStageLevel++;
    }
  


}
