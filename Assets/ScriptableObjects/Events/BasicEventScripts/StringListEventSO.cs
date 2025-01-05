using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringListEventSO : ScriptableObject
{
    private List<StringListListener> listeners =
    new List<StringListListener>();

    public bool debug;

    public void Raise(string[] inString)
    {
        if (debug)
        {
            Debug.Log("TestEvent Raised.");
        }

        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(inString);
    }

    public void RegisterListener(StringListListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(StringListListener listener)
    { listeners.Remove(listener); }
}
