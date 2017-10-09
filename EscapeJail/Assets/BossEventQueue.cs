﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventOrder
{
    Random,
    InOrder
}


public class BossEventQueue : MonoBehaviour
{
    private List<string> eventList = new List<string>();
    private Queue<string> eventQueue = new Queue<string>();

    private MonoBehaviour targetGameObject =null;
    private EventOrder eventOrder;  

    public void Initialize(MonoBehaviour target, EventOrder eventOrder = EventOrder.Random)
    {
        targetGameObject = target;
        this.eventOrder = eventOrder;
    }

    public void StartEventQueue()
    {
        switch (eventOrder)
        {
            case EventOrder.InOrder:
                {
                    //미구현
                }
                break;
            case EventOrder.Random:
                {
                    StartCoroutine(EventRandomProcess());
                }
                break;
        }
    }

    public void AddEvent(string Name)
    {
        if (eventList == null) return;
        eventList.Add(Name);
    }   
    private void AddRandomEventToQueue()
    {
        if (eventList == null|| eventQueue==null) return;
        eventQueue.Enqueue(eventList[Random.Range(0, eventList.Count)]);
    } 
	
    public IEnumerator EventRandomProcess()
    {
        AddRandomEventToQueue();

        while (true)
        {
            if (eventQueue.Count > 0)
            {
                yield return targetGameObject.StartCoroutine(eventQueue.Dequeue());
                AddRandomEventToQueue();
            }
            else
            {            
                yield return null;
            }
        }
    }

    public void OnDisable()
    {
        eventList.Clear();
        eventList = null;
        eventQueue.Clear();
        eventQueue = null;
    }
}
