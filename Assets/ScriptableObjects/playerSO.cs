using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public enum CardLocation {Draw, Hand, Play, Disc}


//[CreateAssetMenu(fileName = "ScriptableObjectPlayer", menuName = "ScriptableObject/Player")]
public class playerSO : ScriptableObject
{
    public List<gameCard> drawDeck;
    public List<gameCard> playerHand;
    public List<gameCard> inPlay;
    public List<gameCard> discardDeck;

    public CardDataSO cardData;

    public bool targetImmune;
    [SerializeField] private int buyPower;
    [SerializeField] private int tinkerPower;
    [SerializeField] private int wickedPower;

    [SerializeField] private List<string> playerHandNames;

    private int luckyChoice = 1;

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
        luckyChoice = lucky;
        
        addCard(CardLocation.Draw, "Lucky-1");//TODO: Add option to pick Lucky card

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
    }

    public void drawCard()
    {
        if(drawDeck.Count <= 0)
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
    }

    public void playCards(List<int> playedCards)
    {
        for (int i = playedCards.Count - 1; i >= 0; i--) 
        {
            handStatsChange(true, playerHand[i]);
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
        while(discardDeck.Count > 0)
        {
            cardPos = Random.Range(0, discardDeck.Count);
            drawDeck.Add(discardDeck[cardPos]);
            discardDeck.RemoveAt(cardPos);
        }
    }

    public void moveCard(int cardPos, CardLocation firstLoc, CardLocation secondLoc)
    {
        //If the two locations are the same, movement is unnecessary
        if(firstLoc == secondLoc)
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
    private void handStatsChange(bool cardAdded, gameCard card)
    {
        int i;

        if(cardAdded)
        {
            i = 1;
        }
        else
        {
            i = -1;
        }

        switch (card.cardType)
        {
            case CardType.Wildling :
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

    private void resetHandStats()
    {
        buyPower = 0;
        tinkerPower = 0;
        wickedPower = 0;
    }

    public List<string> getNames(CardLocation whereAt)
    {
        List<string> nameList = new List<string>();

        switch (whereAt)
        {
            case CardLocation.Draw:
                for(int i = 0; i < drawDeck.Count; i++)
                {
                    nameList.Add(drawDeck[i].getName());
                }
                break;

            case CardLocation.Hand:
                for(int i = 0; i < playerHand.Count; i++) {
                    nameList.Add(playerHand[i].getName());
                }
                break;

            case CardLocation.Disc:
                for(int i = 0; i < discardDeck.Count; i++)
                {
                    nameList.Add(discardDeck[i].getName());
                }
                break;

            default:
                for(int i = 0; i < inPlay.Count; i++)
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
        if((amount > buyPower) || (amount < 0))
        {
            return;
        }

        buyPower = buyPower - amount;
    }

}
