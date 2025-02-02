using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckUpdateListener : MonoBehaviour
{
    public UpdateDeck DeckEvent;
    public UnityEvent<int, CardLocation, string[]> updateAll;

    public void fullUpdate(int playerNum, CardLocation currDeck, string[] newCards)
    {
        updateAll.Invoke( playerNum, currDeck, newCards);
    }

    private void OnEnable()
    { DeckEvent.RegisterListener(this); }

    private void OnDisable()
    { DeckEvent.UnregisterListener(this); }
}
