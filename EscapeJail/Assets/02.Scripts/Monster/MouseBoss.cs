using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBoss : BossBase
{
    private GameObject mouseHandPrefab;
    private ObjectPool<MouseHand> mouseHandPool;


    //패턴 시간
    private float digPatternLastTime = 5f;
    private float digPatternAttackSpeed = 0.5f;
    private float idleLastTime = 3f;

    //인스펙터에서 할당
    public List<Transform> moveList;
    private enum Actions
    {
        Dig,
        DigOut,
        Walk,
        PatternEnd,
        Die
    }

    public enum Pattern
    {
        Idle,
        DigPattern, 
        FirePattern
    }
   

    private void Action(Actions action)
    {
        switch (action)
        {
            case Actions.Dig:
                {
                    if (animator != null)
                        animator.SetTrigger("Dig");
                }
                break;
            case Actions.DigOut:
                {
                    if (animator != null)
                        animator.SetTrigger("Out");
                }
                break;
            case Actions.Walk:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 1.0f);
                }
                break;
            case Actions.Die:
                {
                    if (animator != null)
                        animator.SetTrigger("Die");
                }
                break;
        }
    }

    private new void Awake()
    {
        base.Awake();

        LoadPrefab(); 
        SetHp(30);

        RegistPatternToQueue();
    }

    private void RegistPatternToQueue()
    {

        bossEventQueue.Initialize(this, EventOrder.InOrder);
     
        bossEventQueue.AddEvent("DigPattern");
        bossEventQueue.AddEvent("FirePattern");

    }

    private void LoadPrefab()
    {

        mouseHandPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Boss/Mouse/MouseHand");
        if (mouseHandPrefab != null)
        {
            mouseHandPool = new ObjectPool<MouseHand>(bossModule.transform, mouseHandPrefab, 10);
            // Debug.Log("풀만들어짐");
        }
    }

    public override void StartBossPattern()
    {
        base.StartBossPattern();
        if (bossEventQueue != null)
            bossEventQueue.StartEventQueue();
    }





    public void HideOn()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

    }
    public void HideOff()
    {
        if (boxCollider != null)
            boxCollider.enabled = true;

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        TeleportToRandomPosit();
    }

    private void TeleportToRandomPosit()
    {
        if (moveList == null) return;
        if (moveList.Count == 0) return;
        this.transform.position = moveList[Random.Range(0, moveList.Count)].position;
    }

    public IEnumerator DigPattern()
    {
        float patternCount = 0f;
        Action(Actions.Dig);
        yield return new WaitForSeconds(3f);

        while (true)
        {
            if (bossModule.NormalTileList != null)
            {
                Tile RandomTile = bossModule.NormalTileList[Random.Range(0, bossModule.NormalTileList.Count)];

                if (mouseHandPool != null)
                {
                    MouseHand mouseHand = mouseHandPool.GetItem();
                    mouseHand.transform.position = RandomTile.transform.position + Vector3.up * 0.35f;
                }

                if (mouseHandPool != null)
                {
                    MouseHand mouseHand = mouseHandPool.GetItem();
                    Vector3 playerPosit = GamePlayerManager.Instance.player.transform.position;
                    mouseHand.transform.position = new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 0f) + playerPosit + Vector3.up * 0.35f;
                }
            }
            patternCount += digPatternAttackSpeed;

            if (patternCount >= digPatternLastTime)
            {
                HideOff();
                Action(Actions.DigOut);
                break;
            }

            yield return new WaitForSeconds(digPatternAttackSpeed);
        }
    }

    public IEnumerator FirePattern()
    {
        //잠시 딜레이
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 36; j++)
            {
                Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                if (bullet != null)
                {
                    bullet.gameObject.SetActive(true);
                    Vector3 fireDIr;

                    if (i % 2 == 0)
                        fireDIr = Vector3.right;
                    else
                        fireDIr = Quaternion.Euler(0f, 0f, 5f) * Vector3.right;

                    fireDIr = Quaternion.Euler(0f, 0f, j * 10f) * fireDIr;
                    bullet.Initialize(this.transform.position, fireDIr.normalized, 3f, BulletType.EnemyBullet);
                    bullet.InitializeImage("white", false);
                    bullet.SetEffectName("revolver");
                }
            }
            yield return new WaitForSeconds(0.7f);
        }    
    }
   
}
