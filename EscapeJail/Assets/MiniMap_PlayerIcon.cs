using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_PlayerIcon : MonoBehaviour
{
    Transform target;
    public void LinkPlayer(Transform transform)
    {
        target = transform;
    }

    public void Update()
    {
        if (target != null)
            this.transform.localPosition = target.transform.localPosition;
    }
}
