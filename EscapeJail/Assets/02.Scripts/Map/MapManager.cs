using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Image maskImage;
    float alphaValue = 255f;

    public static MapManager Instance;
    public List<MapModule> moduleList = new List<MapModule>();
    private float mapMakeCount = 0;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        StartCoroutine(MapPositioningRoutine());
    }

    //맵이 아직 생성중일때
    public void ResetMakeCount()
    {
        mapMakeCount = 0f;
    }

    IEnumerator MapPositioningRoutine()
    {
        while (true)
        {
            mapMakeCount += Time.unscaledDeltaTime;
            if (mapMakeCount > 1.0f)
            {
                Debug.Log("Positioning Complete");
                break;
            }
            yield return null;
        }
        d
        

        PositioningComplete();
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
