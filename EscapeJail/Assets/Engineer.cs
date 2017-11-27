using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : CharacterBase
{

    private new void Awake()
    {
        base.Awake();
        SetHp(10);  
    }
    private new void Start()
    {
        base.Start();
        SetWeapon();
    }
    private new void Update()
    {
        base.Update();
   
    }
}
