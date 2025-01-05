using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StringListListener : MonoBehaviour
{
    public StringListEventSO Event;
    public UnityEvent<string[]> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(string[] inStrings)
    {
        Response.Invoke(inStrings);
    }
}