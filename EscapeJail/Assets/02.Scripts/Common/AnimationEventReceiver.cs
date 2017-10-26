using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimationEventReceiver : MonoBehaviour
{
    public UnityEvent myEvent1;
    public UnityEvent myEvent2;
    public UnityEvent myEvent3;
    public UnityEvent myEvent4;
    public void EventAction1()
    {
        if (myEvent1 != null)
            myEvent1.Invoke();
    }

    public void EventAction2()
    {   
        if (myEvent2!= null)
            myEvent2.Invoke();
    }

    public void EventAction3()
    {
        if (myEvent3 != null)
            myEvent3.Invoke();
    }
    public void EventAction4()
    {
        if (myEvent4 != null)
            myEvent4.Invoke();
    }

}
