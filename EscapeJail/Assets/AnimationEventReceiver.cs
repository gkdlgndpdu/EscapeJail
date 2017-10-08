using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimationEventReceiver : MonoBehaviour
{
    public UnityEvent myEvent1;
    public UnityEvent myEvent2;




    public void EventAction1()
    {
        Debug.Log("EventReceiver호출");
        if (myEvent1 != null)
            myEvent1.Invoke();
    }

    public void EventAction2()
    {
        Debug.Log("EventReceiver호출");
        if (myEvent2!= null)
            myEvent2.Invoke();
    }

}
