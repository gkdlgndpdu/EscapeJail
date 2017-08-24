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
    private float widthDistance = 0.64f;
    private float heightDistance = 0.64f;

    public List<List<Tile>> grid;

    public List<Color> RandomColor;

    private void Start()
    {
        grid = new List<List<Tile>>();

        for (int x = 0; x < widthNum; x++)
        {
            grid.Add(new List<Tile>());
        }

        GenerateMap();
    }

    private void GenerateMap()
    {



        for (int x = 0; x < widthNum; x++)
        {
            for (int y = 0; y < heightNum; y++)
            {
                Vector3 posit = new Vector3((float)(-widthNum / 2 + x) * widthDistance,
                                            (float)(-heightNum / 2 + y) * heightDistance, 0f);
                GameObject obj;
                //테두리
                if (y == 0 || y == heightNum - 1 || x == 0 || x == widthNum - 1)
                {
                    obj = Instantiate(wallTile, this.transform);

                    Tile tile = obj.GetComponent<Tile>();


                  
                }
                else
                {
                    obj = Instantiate(normalTile, this.transform);

                    Tile tile = obj.GetComponent<Tile>();
                    tile.ChangeColor(RandomColor[Random.Range(0, RandomColor.Count - 1)]);


                }

                obj.transform.position = posit;

           
            }
        }



    }





}
