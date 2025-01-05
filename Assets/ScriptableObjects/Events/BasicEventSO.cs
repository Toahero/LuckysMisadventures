using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "soBasicEvent", menuName = "ScriptableObject/BasicEvent")]

public class BasicEventSO : ScriptableObject
{
    private List<BasicListener> listeners =
    new List<BasicListener>();

    public void Raise()
    {
        //Debug.Log("TestEvent Raised.");
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    public void RegisterListener(BasicListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(BasicListener listener)
    { listeners.Remove(listener); }
}
