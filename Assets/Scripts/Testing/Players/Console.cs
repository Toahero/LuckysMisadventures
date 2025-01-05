using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    public roundinator roundinator;
    public PlayerManager playerManager;
    public DisplayPanel displayPanel;
    
    [SerializeField] private int playerNum;
    private playerSO activePlayer;
    private CardLocation curDeck = CardLocation.Draw;

    //Player Card Decks
    [SerializeField] private List<string> drawDeck;
    [SerializeField] private List<string> inHand;
    [SerializeField] private List<string> inPlay;
    [SerializeField] private List<string> discard;


    public void updateDeck()
    {
        changeDeck((int)curDeck);
    }
    public void changeDeck(int loc)
    {
        string[] cardNames;
        
        switch (loc)
        {
            case 0:
                cardNames = activePlayer.getNames(CardLocation.Draw).ToArray(); 
                break;

            case 1:
                cardNames = activePlayer.getNames(CardLocation.Hand).ToArray();
                break;

            case 2:
                cardNames = activePlayer.getNames(CardLocation.Play).ToArray();
                break;

            default:
                cardNames = activePlayer.getNames(CardLocation.Disc).ToArray();
                break;
        }

        displayPanel.UpdateCards(cardNames);
    }

    public void StartUp()
    {
        playerNum = 0;
        activePlayer = playerManager.GetPlayer(playerNum);
        changeDeck((int) curDeck);
    }

    public void changePlayer(int newPlayer) 
    {
        playerNum = newPlayer;
        activePlayer = playerManager.GetPlayer(playerNum);
        updateDecks();

        changeDeck((int)curDeck);
    }

    public void nextPlayer()
    {
        changePlayer(playerNum + 1);
    }

    public void prevPlayer()
    {
        changePlayer(playerNum + 1);
    }

    private void updateDecks()
    {
        drawDeck = activePlayer.getNames(CardLocation.Draw);
        inHand = activePlayer.getNames(CardLocation.Hand);
        inPlay = activePlayer.getNames(CardLocation.Play);
        discard = activePlayer.getNames(CardLocation.Disc);
    }
}
