using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private MapModuleBase parentModule;

    public void Initialize(MapModuleBase parentModule)
    {
        this.parentModule = parentModule;
        this.transform.parent = parentModule.transform;
        this.transform.localPosition = Vector3.zero;
    }
}
