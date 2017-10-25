using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArticleType
{
 Table,
 WeaponBox
}


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Article : CharacterInfo
{
    public ArticleType articleType;

    protected void SetArticleType(ArticleType type)
    {
        this.articleType = type;
    }
}
