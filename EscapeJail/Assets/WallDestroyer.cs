using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {    
        Tile tile = collision.gameObject.GetComponent<Tile>();
        if (tile != null)
        {
            if (tile.tileType == TileType.Wall)
            {            
                Destroy(collision.gameObject);
            }
        }
    }

}
