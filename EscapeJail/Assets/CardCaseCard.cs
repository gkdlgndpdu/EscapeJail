using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Spade,
    Heart,
    Clover,
    Diamond
}

[RequireComponent(typeof(SpriteRenderer))]
public class CardCaseCard : MonoBehaviour
{
    [SerializeField]
    private Sprite spade;
    [SerializeField]
    private Sprite heart;
    [SerializeField]
    private Sprite clover;
    [SerializeField]
    private Sprite diamond;

    private Dictionary<CardType, Sprite> cardList=  new Dictionary<CardType, Sprite>();

    private float ShuffleDelay = 0.5f;

    private CardType nowCardType;
    public CardType NowCardType
    {
        get
        {
            return nowCardType;
        }
    }

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        cardList.Add(CardType.Spade, spade);
        cardList.Add(CardType.Heart, heart);
        cardList.Add(CardType.Clover, clover);
        cardList.Add(CardType.Diamond, diamond);
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable()
    {
        StartCoroutine(ShuffleRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }



    IEnumerator ShuffleRoutine()
    {
        while (true)
        {
            if (cardList.Count < 3) yield return null;

            foreach(KeyValuePair<CardType,Sprite> data in cardList)
            {
                if (spriteRenderer != null)
                    spriteRenderer.sprite = data.Value;
                nowCardType = data.Key;
                yield return new WaitForSeconds(ShuffleDelay);
            }
       
        }
    }  
}
