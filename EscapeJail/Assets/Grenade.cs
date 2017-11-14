using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrenadeType
{
    Flashbang,
}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class Grenade : MonoBehaviour
{
    private GrenadeType grenadeType;

    public void Initialize(GrenadeType grenadeType)
    {
        this.grenadeType = grenadeType;
    }

}
