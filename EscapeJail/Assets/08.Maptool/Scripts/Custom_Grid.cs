using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_Grid : MonoBehaviour
{
    public float width = 32.0f;
    public float height = 32.0f;

    public Color gridColor = Color.white;

    public Transform tilePrefab;

    public TileSet tileSet;

    private void OnDrawGizmos()
    {
        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = gridColor;

        for (float y = pos.y - 800.0f; y < pos.y + 800.0f; y += this.height)
        {
            Gizmos.DrawLine(new Vector3(-1000000.0f, Mathf.Floor(y / this.height) * height, 0f),
                new Vector3(1000000.0f, Mathf.Floor(y / this.height) * height, 0f));

        }

        for(float x = pos.x -1200.0f;x<pos.x + 1200.0f; x += this.width)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Floor(x / this.width) * this.width, -1000000.0f, 0.0f),
                new Vector3(Mathf.Floor(x / this.width) * this.width, 1000000.0f, 0.0f));
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
