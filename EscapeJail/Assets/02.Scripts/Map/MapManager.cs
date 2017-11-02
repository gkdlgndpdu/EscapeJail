using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapManager : MonoBehaviour
{
    private List<MapModuleBase> moduleList;
    private List<GameObject> objectList;

    private float mapMakeCount = 0;

    //맵 생성기
    private MapModuleGenerator mapModuleGenerator;

    [SerializeField]
    private Transform backGroundParent;
    


    void Awake()
    {
        LoadObject();

    }

    private void LoadObject()
    {
        objectList = new List<GameObject>();

        if (objectList == null) return;
        GameObject[] objects = Resources.LoadAll<GameObject>("Prefabs/Articles/");

        if (objects != null)
        {
            if (objects.Length != 0)

            {
                for (int i = 0; i < objects.Length; i++)
                {
                    objectList.Add(objects[i]);
                }
            }
        }
    }

    public GameObject GetRandomArticle()
    {
        if (objectList == null) return null;
        if (objectList.Count == 0) return null;
        return objectList[(Random.Range(0, objectList.Count))];
    }


    public void MakeMap(StageData stageData)
    {
        mapModuleGenerator = new MapModuleGenerator(this.transform, this);

        moduleList = new List<MapModuleBase>();

        ResetMakeCount();

        if (mapModuleGenerator != null)
            mapModuleGenerator.MakeMap(stageData);

        StartCoroutine(MapPositioningRoutine());
    }

    //맵이 아직 생성중일때
    public void ResetMakeCount()
    {
        mapMakeCount = 0f;
    }

    IEnumerator MapPositioningRoutine()
    {
      

        ResetMakeCount();

        //맵 포지셔닝
        while (true)
        {
            mapMakeCount += Time.deltaTime;

            if (mapMakeCount > 1.0f)
            {
                Debug.Log("Positioning Complete");
                break;
            }
            yield return null;
        }

        MakeWall();
        PositioningComplete();
        CreateObjects();

        LoadingBoard.Instance.LoadingEnd();

    }

    public void DestroyEveryMapModule()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);

        if (moduleList != null)
        {
            moduleList.Clear();
         
        }   

        mapModuleGenerator = null;
    

    }

    private void MakeWall()
    {
        //벽 생성
        if (mapModuleGenerator != null)
            mapModuleGenerator.MakeWall(backGroundParent);       
    }
  

    //private void MakeBossModule()
    //{
    //    //보스 모듈 생성
    //    if (mapModuleGenerator != null)
    //        mapModuleGenerator.MakeBossModule(this.transform);
    //}


    private void CreateObjects()
    {
        if (moduleList == null) return;
        for (int i = 0; i < moduleList.Count; i++)
        {
            moduleList[i].MakeObjects();
        }
    }


    public void AddToModuleList(MapModuleBase module)
    {
        if (moduleList == null) return;
        if (module == null) return;

        moduleList.Add(module);
    }

    public void ModuleListClear()
    {
        if (moduleList == null) return;

        for (int i = 0; i < moduleList.Count; i++)
        {
            if (moduleList[i] != null) 
            Destroy(moduleList[i].gameObject);
        }

        moduleList.Clear();
    }

    private void PositioningComplete()
    {
        if (moduleList == null) return;
        for (int i = 0; i < moduleList.Count; i++)
        {
            if (moduleList[i] != null)
                moduleList[i].PositioningComplete();
        }

    }


}
