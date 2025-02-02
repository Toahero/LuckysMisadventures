using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Console : MonoBehaviour
{
    public roundinator roundinator;
    public PlayerManager playerManager;
    public DisplayPanel displayPanel;
    
    [SerializeField] private int playerNum;

    private CardLocation curDeck = CardLocation.Draw;

    //Player Card Decks
    [SerializeField] private List<string> drawDeck;
    [SerializeField] private List<string> inHand;
    [SerializeField] private List<string> inPlay;
    [SerializeField] private List<string> discard;

    public void fullUpdateDeck(CardLocation updateTarget, string[] cardArr)
    {
        switch (updateTarget)
        {
            case CardLocation.Draw:
                drawDeck = cardArr.ToList<string>(); break;

            case CardLocation.Hand:
                inHand = cardArr.ToList<string>(); break;

            case CardLocation.Play:
                inPlay = cardArr.ToList<string>(); break;

            default:
                discard = cardArr.ToList<string>(); break;

        }
    }



}
