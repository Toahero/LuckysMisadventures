using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneIntListener : MonoBehaviour
{
    public OneIntEvent Event;
    public UnityEvent<int> Response;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised(int inputInt)
    { Response.Invoke(inputInt); }
}
