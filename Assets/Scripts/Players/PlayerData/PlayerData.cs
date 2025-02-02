using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private int playerNum;

    private List<gameCard> drawDeck;
    private List<gameCard> playerHand;
    private List<gameCard> inPlay;
    private List<gameCard> discardDeck;

    private CardDataSO cardData;
    private UpdateDeck updateEvent;

    private bool targetImmune;
    [SerializeField] private int buyPower;
    [SerializeField] private int tinkerPower;
    [SerializeField] private int wickedPower;

    [SerializeField] private List<string> playerHandNames;

    //private int luckyChoice = 1;

    public PlayerData(int num, bool newGame, hirelingChoice hireling, int luckyPick)
    {
        playerNum = num;
        cardData = Resources.Load<CardDataSO>(constantStrings.cardDataLoc);
        updateEvent = Resources.Load<UpdateDeck>(constantStrings.deckUpdateLoc);
        initializeDeck();

        if(newGame)
        {
            newDeck(hireling, luckyPick);
        }
        Debug.Log("Deck Created");
        CardStrings.deckToString(CardLocation.Draw, getNames(CardLocation.Draw));
    }

    public void initializeDeck()
    {
        drawDeck = new List<gameCard>();
        playerHand = new List<gameCard>();
        inPlay = new List<gameCard>();
        discardDeck = new List<gameCard>();

        //Name Lists
        playerHandNames = new List<string>();
    }

    public void newDeck(hirelingChoice firstHireling, int lucky)
    {
        int luckyPick = lucky % constantInts.numLuckys;

        addCard(CardLocation.Draw, "Lucky-" + lucky);//TODO: Add option to pick Lucky card

        for (int i = 0; i < 3; i++)
        {

            addCard(CardLocation.Draw, "Tinman");


            addCard(CardLocation.Draw, "Lion");
        }

        addCard(CardLocation.Draw, "ScarecrowR");


        addCard(CardLocation.Draw, "ScarecrowB");


        addCard(CardLocation.Draw, "ScarecrowY");

        switch (firstHireling)
        {
            case hirelingChoice.WhiteRabbit:

                addCard(CardLocation.Draw, "White Rabbit");
                break;

            case hirelingChoice.ClockMakers:

                addCard(CardLocation.Draw, "Clockmakers");
                break;

            default:

                addCard(CardLocation.Draw, "Winged Monkey");
                break;

        }

        shuffleDraw();
        updateDeckVisual(CardLocation.Draw);
    }

    public void drawTo(int handSize)
    {
        int cardsToDraw = handSize - playerHand.Count;

        //Debug.Log("You should draw " + cardsToDraw + " cards");
        if (cardsToDraw > 0)
        {
            for (int i = 0; i < cardsToDraw; i++)
            {
                drawCard();
            }
        }

        //Update the player hand and draw deck
        updateDeckVisual(CardLocation.Draw);
        updateDeckVisual(CardLocation.Hand);
    }

    public void drawCard()
    {
        if (drawDeck.Count <= 0)
        {
            cycleDiscard();
        }

        playerHandNames.Add(drawDeck[0].getName());

        moveCard(0, CardLocation.Draw, CardLocation.Hand);
    }

    public void playPhase(int playSize)
    {
        //TODO: Change this so that the player picks cards
        List<int> playedCards = new List<int>();
        playedCards.Add(0);
        playedCards.Add(1);
        playedCards.Add(2);

        playCards(playedCards);

        updateDeckVisual(CardLocation.Hand);
        updateDeckVisual(CardLocation.Play);
    }

    public void playCards(List<int> playedCards)
    {
        //For each card, update the stats to reflect the new card's addition, then move it from hand

        for (int i = playedCards.Count - 1; i >= 0; i--)
        {
            playStatsChange(true, playerHand[i]);
            moveCard(playedCards[i], CardLocation.Hand, CardLocation.Play);
        }
    }

    public void discardCard(int cardPlace)
    {
        //Debug.Log(playerHand[cardPlace] + "at place " + cardPlace + " will be discarded.");
        discardDeck.Add(playerHand[cardPlace]);
        playerHand.RemoveAt(cardPlace);
        playerHandNames.RemoveAt(cardPlace);
    }

    public void shuffleDraw()
    {
        List<gameCard> tempList = new List<gameCard>();
        int cardPos;

        while (drawDeck.Count > 0)
        {
            cardPos = Random.Range(0, drawDeck.Count);
            tempList.Add(drawDeck[cardPos]);
            drawDeck.RemoveAt(cardPos);
        }
        drawDeck = tempList;
    }

    public void cycleDiscard()
    {
        //Debug.Log("Cycling your discard into the draw deck");

        int cardPos;
        while (discardDeck.Count > 0)
        {
            cardPos = Random.Range(0, discardDeck.Count);
            drawDeck.Add(discardDeck[cardPos]);
            discardDeck.RemoveAt(cardPos);
        }
    }

    public void moveCard(int cardPos, CardLocation firstLoc, CardLocation secondLoc)
    {
        //If the two locations are the same, movement is unnecessary
        if (firstLoc == secondLoc)
        {
            return;
        }

        List<gameCard> firstList;
        List<gameCard> secondList;

        //Set firstList to the relevant deck
        switch (firstLoc)
        {
            case CardLocation.Draw:
                firstList = drawDeck;
                break;

            case CardLocation.Hand:
                firstList = playerHand;
                break;

            case CardLocation.Play:
                firstList = inPlay;
                break;

            default:
                firstList = discardDeck;
                break;
        }

        if (firstList.Count <= cardPos)
        {
            return;
        }

        //Set secondList to the relevant deck
        switch (secondLoc)
        {
            case CardLocation.Draw:
                secondList = drawDeck;
                break;

            case CardLocation.Hand:
                secondList = playerHand;
                break;

            case CardLocation.Play:
                secondList = inPlay;
                break;

            default:
                secondList = discardDeck;
                break;
        }

        secondList.Add(firstList[cardPos]);
        firstList.RemoveAt(cardPos);
    }

    public void addCard(CardLocation addAt, string cardName)
    {
        gameCard newCard;

        newCard = cardData.cardFromName(cardName);

        switch (addAt)
        {
            case CardLocation.Draw:
                drawDeck.Add(newCard);
                break;

            case CardLocation.Hand:
                playerHand.Add(newCard);
                break;

            case CardLocation.Play:
                inPlay.Add(newCard);
                break;

            default:
                discardDeck.Add(newCard);
                break;
        }
    }

    //If cardAdded is true, the card enters play, increasing stats. If false, card is removed, decreasing them
    private void playStatsChange(bool cardAdded, gameCard card)
    {
        int i;

        if (cardAdded)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }

        switch (card.cardType)
        {
            case CardType.Wildling:
                buyPower += card.getPower() * i;
                break;

            case CardType.Tinkerer:
                tinkerPower += card.getPower() * i;
                break;

            case CardType.Wicked:
                wickedPower += card.getPower() * i;
                break;

            default:
                break;
        }
    }

    private void updateDeckVisual(CardLocation location)
    {
        updateEvent.fullUpdateDeck(playerNum, location, getNameArray(location));
    }

    private void resetplayStats()
    {
        buyPower = 0;
        tinkerPower = 0;
        wickedPower = 0;
    }

    private string[] getNameArray(CardLocation deckNeeded)
    {
        List<gameCard> cards = new List<gameCard>();

        switch (deckNeeded)
        {
            case CardLocation.Draw:
                cards = drawDeck;
                break;

            case CardLocation.Hand:
                cards = playerHand;
                break;

            case CardLocation.Play:
                cards = inPlay;
                break;

            case CardLocation.Disc:
                cards = discardDeck;
                break;
        }

        string[] nameArray = new string[cards.Count];

        for(int i = 0; i < cards.Count; i++)
        {
            nameArray[i] = cards[i].getName();
        }

        return nameArray;
    }

    public List<string> getNames(CardLocation whereAt)
    {
        List<string> nameList = new List<string>();

        switch (whereAt)
        {
            case CardLocation.Draw:
                for (int i = 0; i < drawDeck.Count; i++)
                {
                    nameList.Add(drawDeck[i].getName());
                }
                break;

            case CardLocation.Hand:
                for (int i = 0; i < playerHand.Count; i++)
                {
                    nameList.Add(playerHand[i].getName());
                }
                break;

            case CardLocation.Disc:
                for (int i = 0; i < discardDeck.Count; i++)
                {
                    nameList.Add(discardDeck[i].getName());
                }
                break;

            default:
                for (int i = 0; i < inPlay.Count; i++)
                {
                    nameList.Add(inPlay[i].getName());
                }
                break;
        }

        return nameList;
    }

    public int getTinkerPower()
    {
        return tinkerPower;
    }

    public int getWickedPower()
    {
        return wickedPower;
    }

    public int getMoney()
    {
        return buyPower;
    }

    public void spendMoney(int amount)
    {
        if ((amount > buyPower) || (amount < 0))
        {
            return;
        }

        buyPower = buyPower - amount;
    }

}
