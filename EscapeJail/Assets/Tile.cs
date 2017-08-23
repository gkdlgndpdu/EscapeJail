using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Normal,
    Wall,
    Object
}

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{    
    public TileType tileType;
}
