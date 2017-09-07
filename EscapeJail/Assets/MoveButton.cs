using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool pressing = false;
    public UnityEvent myEvent;
    void Update()
    {
        if (pressing && myEvent != null)
        {
            myEvent.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pressing = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pressing = false;
    }
}
