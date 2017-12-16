using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MiniMap_PlayerIcon : MonoBehaviour
{
    private Transform target;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void LinkPlayer(Transform transform)
    {
        target = transform;
        SetIcon();
    }

    private void SetIcon()
    {
        CharacterType playerName = (CharacterType)PlayerPrefs.GetInt(PlayerPrefKeys.CharacterKeyValue, (int)CharacterType.Soldier);
        string ItemPath = string.Format("Sprites/icon/{0}", playerName.ToString());
        Sprite loadSprite  = Resources.Load<Sprite>(ItemPath);
        if(loadSprite!=null)
            spriteRenderer.sprite = loadSprite;
    }

    public void Update()
    {
        if (target != null)
            this.transform.localPosition = target.transform.localPosition;
    }
}
