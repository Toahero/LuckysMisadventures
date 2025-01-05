using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BanksEventsListener : MonoBehaviour
{
    public BankEventsSO Event;
    public UnityEvent Response;
    public UnityEvent<string[]> bUpdate;

    private void OnEnable()
    { Event.RegisterListener(this); }

    private void OnDisable()
    { Event.UnregisterListener(this); }

    public void OnEventRaised()
    { Response.Invoke(); }

	public void takeCard(int cardPos, int playerNum){
		
	}
	
	public void swapCards(int firstPos, int lastPos){
		
	}

    public void FullUpdate(string[] cardIDs)
    {
        bUpdate.Invoke(cardIDs);
    }
}
