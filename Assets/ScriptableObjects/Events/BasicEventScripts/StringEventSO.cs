using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soStringEvent", menuName = "ScriptableObject/StringEvent")]
public class StringEventSO : ScriptableObject
{
    private List<StringEventListener> listeners =
    new List<StringEventListener>();

    public bool debug;

    public void Raise(string inString)
    {
        if (debug)
        {
            Debug.Log("TestEvent Raised.");
        }
        
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(inString);
    }

    public void RegisterListener(StringEventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(StringEventListener listener)
    { listeners.Remove(listener); }
}