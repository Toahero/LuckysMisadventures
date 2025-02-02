using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDeckUpdate", menuName = "ScriptableObject/PlayerDeckUpdate")]
public class UpdateDeck : ScriptableObject
{
    private List<DeckUpdateListener> listeners = new List<DeckUpdateListener>();
    public void fullUpdateDeck(int playerNum, CardLocation currDeck, string[] newCards)
    {
        //Debug.Log("FullUpdateDeck: Player:" + playerNum + " Deck:" + currDeck);
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].fullUpdate(playerNum, currDeck, newCards);
        }
    }

    public void RegisterListener(DeckUpdateListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(DeckUpdateListener listener)
    { listeners.Remove(listener); }
}
