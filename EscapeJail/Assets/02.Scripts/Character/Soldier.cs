using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : CharacterBase
{


    private new void Awake()
    {
        base.Awake();

        hp = 200;
        maxHp = 200;

    
    }
    // Use this for initialization
    private new void Start()
    {
        UIUpdate();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
      
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }
   
}
