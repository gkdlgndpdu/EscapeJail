﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapManager : MonoBehaviour
{
 
    private List<MapModuleBase> moduleList;
    [SerializeField]
    private List<GameObject> tableList;
    [SerializeField]
    private GameObject weaponBox;
    public GameObject WeaponBoxPrefab
    {
        get
        {
            return weaponBox;
        }
    }

    private float mapMakeCount = 0;

    //맵 생성기
    private MapModuleGenerator mapModuleGenerator;
    
       
   
 

    public GameObject GetRandomArticle()
    {
        if (tableList == null) return null;
        if (tableList.Count == 0) return null;
        if (weaponBox == null) return null;

        if (MyUtils.GetPercentResult(Probability.weaponBoxProb) == true)
        {
            return weaponBox;
        }
        return tableList[(Random.Range(0, tableList.Count))];

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
        MakePortal();
        PositioningComplete();
        CreateObjects();  

        LoadingBoard.Instance.LoadingEnd();

    }

    public void DestroyEveryMapModule()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);

        //타일 재활용
        for(int i = 0; i < moduleList.Count; i++)
        {
            MapModule normalModule = moduleList[i].GetComponent<MapModule>();
            if (normalModule != null)
                normalModule.PushAllTileToPool();
        }

        if (moduleList != null)
        {
            moduleList.Clear();         
        }
        mapModuleGenerator.PullBackGroundTiles();

        mapModuleGenerator = null;
    

    }

    private void MakeWall()
    {
        //벽 생성
        if (mapModuleGenerator != null)
            mapModuleGenerator.MakeWall(this.transform);       
    }

    private void MakePortal()
    {
        if (mapModuleGenerator != null)
            mapModuleGenerator.MakePortal();

   
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

        System.GC.Collect();
        

    }


}
