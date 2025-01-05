using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCardTests : MonoBehaviour
{
    public PlayerManager playerManager;
    public List<string> cards;

    public CardLocation deckToView;
    public int playerNum;

    public PlayerActions testActions;
    public cardDrawSO testDraw;

    public DrawType cardDeck;
    public int cardLoc;

    public void checkCards()
    {
        playerSO currentPlayer = playerManager.GetPlayer(playerNum);

        cards = currentPlayer.getNames(deckToView);
    }

    public void testBuy()
    {
        playerSO currentPlayer = playerManager.GetPlayer(playerNum);
        testActions.player = currentPlayer;
        testActions.cardBanks = testDraw;

        testActions.buyCard(cardDeck, cardLoc);
    }
}
