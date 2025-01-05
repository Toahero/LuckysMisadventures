using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneIntEvent : ScriptableObject
{
    private List<OneIntListener> listeners =
    new List<OneIntListener>();

    public bool debug;

    public void Raise(int inputInt)
    {
        if (debug)
        {
            Debug.Log(this.name + " raised with value: " + inputInt);
        }

        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(inputInt);
    }

    public void RegisterListener(OneIntListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(OneIntListener listener)
    { listeners.Remove(listener); }
}
