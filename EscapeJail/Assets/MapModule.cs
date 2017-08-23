using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MapState
{
    Lock,
    Playing,    
}

public class MapModule : MonoBehaviour
{
    private MapState mapState;

    public List<GameObject> doorList = new List<GameObject>();
    public List<Tile> tileList = new List<Tile>();

    private void Start()
    {
        StartCoroutine(SpawnMonster());
    }

    private void Awake()
    {
        LinkTile();
    }

    private void LinkTile()
    {
        if (tileList == null) return;

        Tile[] tiles = GetComponentsInChildren<Tile>();
        for(int i=0;i< tiles.Length; i++)
        {
            if(tiles[i].tileType ==TileType.Normal)
            tileList.Add(tiles[i]);
        }
    }

    IEnumerator SpawnMonster()
    {
        if(tileList == null) yield break;

        while (true)
        {
            int RandomIndex = Random.Range(0, tileList.Count - 1);
            MonsterManager.Instance.SpawnMonster(MonsterName.Mouse1, tileList[RandomIndex].transform.position);
            yield return new WaitForSeconds(2.0f);
        }
    } 

	

}
