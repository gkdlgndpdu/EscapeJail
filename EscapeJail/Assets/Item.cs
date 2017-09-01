using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Weapon, //무기류
    Passive,
    Active //총알,구급상자 등
}

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
