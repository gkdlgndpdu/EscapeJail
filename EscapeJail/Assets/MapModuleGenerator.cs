using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModuleGenerator : MonoBehaviour
{
    //임시
    [SerializeField]
    private GameObject normalTile;
    [SerializeField]
    private GameObject wallTile;

    private int widthNum = 30;
    private int heightNum = 30;
    private float widthDistance =0.64f;
    private float heightDistance = 0.64f;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {



      for (int x = 0; x < widthNum; x++)
        {
            for(int y = 0; y < heightNum; y++)
            {
                Vector3 posit = new Vector3((float)(-widthNum / 2 + x) * widthDistance,
                                            (float)(-heightNum / 2 + y) * heightDistance, 0f);
                GameObject obj = Instantiate(normalTile, this.transform);
                obj.transform.position = posit;
            }  
        }

    }

  

    

}
