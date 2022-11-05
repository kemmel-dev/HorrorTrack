using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressionManager : MonoBehaviour
{

    public int currentEvent = 0;

    public List<UnityEvent> events;

    private void Start()
    {
        NextEvent();
    }

    public void NextEvent()
    {
        events[currentEvent++].Invoke();
    }
}
