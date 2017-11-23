
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

    private int tileSize = 30;

    public float gridMaxSize = 100f;

    public bool isRenderGrid = true;

    public int nowLayerNumber=0;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
         
        if (isRenderGrid == false)
        {
            return;
        }


        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = gridColor;

        // Gizmos.DrawLine(new Vector3(-25f,0f,0f),new Vector3(25f,0f,0f));
      //  Gizmos.DrawLine(new Vector3(0f, -25f, 0f), new Vector3(0f, 25f, 0f));

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

       
    }
#endif

}
