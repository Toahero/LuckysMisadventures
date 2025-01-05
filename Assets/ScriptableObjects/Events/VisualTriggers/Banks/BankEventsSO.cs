using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BankDisplayTrigger", menuName = "ScriptableObject/BankDisplayTrigger")]

public class BankEventsSO : ScriptableObject
{
    private List<BanksEventsListener> listeners =
    new List<BanksEventsListener>();

    public bool debug = false;

    public void Raise()
    {
        if (debug)
        {
            Debug.Log("TestEvent Raised.");
        }

        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }
	
	public void cardTaken(int cardPos, int playerNum){
		
        if (debug)
        {
            Debug.Log("Player " + playerNum + " takes the card at position " + cardPos);
        }

		//command each listener to remove a card from the bank
		for(int i = listeners.Count - 1; i >= 0; i--){
			listeners[i].takeCard(cardPos, playerNum);
		}
	}
	
	public void cardSwitched(int firstPos, int lastPos){
		if(debug)
        {
            Debug.Log("Swapping the card at position " + firstPos + " with the card at position " + lastPos);
        }

		for(int i = listeners.Count - 1; i >= 0; i--){
			listeners[i].swapCards(firstPos, lastPos);
		}
	}
	
	public void BankUpdate(string[] cardIDs)
    {
        if (debug)
        {
            Debug.Log("Full Update Bank");
        }


        for(int i = listeners.Count - 1; i >= 0;i--)
        {
			//command each listener to fully update the bank
            listeners[i].FullUpdate(cardIDs);
        }
    }

    public void RegisterListener(BanksEventsListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(BanksEventsListener listener)
    { listeners.Remove(listener); }
}
