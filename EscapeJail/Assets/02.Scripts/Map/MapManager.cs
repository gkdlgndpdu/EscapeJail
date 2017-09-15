using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapManager : MonoBehaviour
{
    //싱글턴
    public static MapManager Instance;

    [SerializeField]
    private Image maskImage;
    float alphaValue = 255f;

    private List<MapModule> moduleList = new List<MapModule>();
    private List<GameObject> objectList = new List<GameObject>();


    private float mapMakeCount = 0;

    //맵 생성기
    private MapModuleGenerator mapModuleGenerator;

    public Transform wallParent;

    //생성정보
    private StageData stageData;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadObject();
        mapModuleGenerator = new MapModuleGenerator(this.transform);

        stageData = GameOption.Instance.StageData;
    }

    private void LoadObject()
    {
        if (objectList == null) return;
        GameObject[] objects = Resources.LoadAll<GameObject>("Prefabs/Articles/");
        
        if (objects != null)
        {
            if (objects.Length != 0)

            {
                for(int i = 0; i < objects.Length; i++)
                {
                    objectList.Add(objects[i]);
                }
            }
        }
    }

    public GameObject GetRandomObject()
    {
        if (objectList == null) return null;
        if (objectList.Count == 0) return null;
        return objectList[(Random.Range(0, objectList.Count))];
    }

    void Start()
    {
    
        MakeMap(stageData);

        StartCoroutine(MapPositioningRoutine());
    }

    public void MakeMap(StageData stageData)
    {
        if (mapModuleGenerator != null)
            mapModuleGenerator.MakeMap(stageData);
    }

    //맵이 아직 생성중일때
    public void ResetMakeCount()
    {
        mapMakeCount = 0f;   
    }

    IEnumerator MapPositioningRoutine()
    {
        //맵 포지셔닝
        while (true)
        {
            mapMakeCount += Time.deltaTime;
     
            if (mapMakeCount > 3.0f)
            {
                Debug.Log("Positioning Complete");
                break;
            }
            yield return null;
        }

        //벽 생성
        if (mapModuleGenerator != null)
            mapModuleGenerator.MakeWall(wallParent);

        PositioningComplete();
        CreateObjects();
    }


    private void CreateObjects()
    {
        if (moduleList == null) return;
        for(int i =0; i < moduleList.Count; i++)
        {
            moduleList[i].MakeObjects();
        }
    }


    public void AddToModuleList(MapModule module)
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
            if (moduleList[i] != null) ;
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
