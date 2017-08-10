using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_Grid : MonoBehaviour
{
    [HideInInspector]
    public float width = 1f;
    [HideInInspector]
    public float height = 1f;

    public Color gridColor = Color.white;

    public Transform tilePrefab;

    public TileSet tileSet;

    private int tileSize = 20;

    public float gridMaxSize = 100f;

    public bool isRenderGrid = true;

    public int nowLayerNumber=0;

    private void OnDrawGizmos()
    {
         
        if (isRenderGrid == false)
        {
            return;
        }


        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = gridColor;

        // Gizmos.DrawLine(new Vector3(-25f,0f,0f),new Vector3(25f,0f,0f));
        Gizmos.DrawLine(new Vector3(0f, -25f, 0f), new Vector3(0f, 25f, 0f));

        for (int x = 0; x <= tileSize; x++)
        {
            Gizmos.DrawLine(new Vector3((float)width * (float)(x - tileSize / 2), -gridMaxSize, 0f),
                            new Vector3((float)width * (float)(x - tileSize / 2), gridMaxSize, 0f));
        }

        for (int y = 0; y <= tileSize; y++)
        {
            Gizmos.DrawLine(new Vector3(-gridMaxSize, (float)height * (float)(y - tileSize / 2), 0f)
                , new Vector3(gridMaxSize, (float)height * (float)(y - tileSize / 2), 0f));
        }

        //for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y += this.height)
        //{
        //    Gizmos.DrawLine(new Vector3(-10000f, Mathf.Floor(y / this.height) * height, 0f),
        //        new Vector3(10000f, Mathf.Floor(y / this.height) * height, 0f));
        //}

        //for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += this.width)
        //{
        //    Gizmos.DrawLine(new Vector3(Mathf.Floor(x / this.width) * this.width, -10000f, 0.0f),
        //        new Vector3(Mathf.Floor(x / this.width) * this.width, 10000f, 0.0f));
        //}
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
