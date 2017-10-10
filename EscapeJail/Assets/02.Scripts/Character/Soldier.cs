using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Soldier : CharacterBase
{
    
    private new void Awake()
    {
        base.Awake();

        hp = 200;
        hpMax = 200;

    
    }
    // Use this for initialization
    private new void Start()
    {
      //  AddWeapon(new Bazooka());
        AddWeapon(new AssaultRifle());

        UIUpdate();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
      
    }

   
}
