using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBoss : BossBase
{
    private GameObject mouseHandPrefab;
    private ObjectPool<MouseHand> mouseHandPool;


     private new void Awake()
    {
        base.Awake();
        LoadPrefab();

    }

    private void LoadPrefab()
    {
      
        mouseHandPrefab = Resources.Load("Prefabs/Monsters/Boss/Mouse/MouseHand") as GameObject;
        if (mouseHandPrefab != null)
        {
            mouseHandPool = new ObjectPool<MouseHand>(bossModule.transform, mouseHandPrefab, 10);
            Debug.Log("풀만들어짐");
        }
    }

    public override void StartBossPattern()
    {
        if (isPatternStart == true) return;
        isPatternStart = true;

        Debug.Log("BossPatternStart");
        StartCoroutine(BossPattern());
    }

    public IEnumerator BossPattern()
    {
        while (true)
        {
            if (bossModule.NormalTileList != null)
            {
                Tile RandomTile = bossModule.NormalTileList[Random.Range(0, bossModule.NormalTileList.Count)];

                if (mouseHandPool != null)
                {
                  MouseHand mouseHand = mouseHandPool.GetItem();
                    mouseHand.transform.position = RandomTile.transform.position;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
 
    }
}
