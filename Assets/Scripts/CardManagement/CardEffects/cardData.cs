using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardSO;

public class cardData : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, gameCard> cardStats = new Dictionary<string, gameCard>();

    public List<string> dictionaryKeys = new List<string>();

    public string inputName;

    public gameCard card;

    public CardType type;
    public int cost;
    public int vPs;
    public int power;
    public InvokeColorReq fateTrigger;
    

    void Start()
    {
        setCards();
    }

    private void Update()
    {
        card = cardStats[inputName];
        type = card.cardType;
        cost = card.cost;
        vPs = card.victoryPoints;
        power = card.power;
        fateTrigger = card.invokeColor;
    }


    void setCards()
    {
        TextAsset cardStatFile;
        string cardStatString;

        string[] statRows;
        string[] currentStatRow;


        //Load the card stats csv, and convert it to a string
        cardStatFile = Resources.Load<TextAsset>("Data/cardStats");
        cardStatString = cardStatFile.ToString();

        string cardName;


        //split the string into rows for each card
        statRows = cardStatString.Split("\n");

        gameCard newCard;

        for (int i = 1; i < statRows.Length - 1; i++)
        {
            currentStatRow = statRows[i].Split(",");
            cardName = currentStatRow[0];
            newCard = getCardStats(currentStatRow);

            dictionaryKeys.Add(cardName);
            

            cardStats.Add(cardName, newCard);
        }
    }
    
    gameCard getCardStats(string[] stats)
    {
        gameCard currentCard;
        string cardName;
        CardType cardType;
        int cardCost;
        int cardVP;
        int cardPower;
        InvokeColorReq color;

        cardName = stats[0];
        //Set the Card type
        switch (stats[1])
        {
            case "Junk":
                cardType = CardType.Junk;
                break;

            case "Machine":
                cardType = CardType.Machine;
                break;

            case "Tinkerer":
                cardType = CardType.Tinkerer;
                break;

            case "Wicked":
                cardType = CardType.Wicked;
                break;

            case "Wildling":
                cardType = CardType.Wildling;
                break;

            default:
                cardType = CardType.Junk;
                Debug.Log("Error: Card Type '" + stats[1] + "' does not exist.");
                break;
        }
        //set the numerical stats (cost, victory points, and power)
        cardCost = int.Parse(stats[2]);
        cardVP = int.Parse(stats[3]);
        cardPower = int.Parse(stats[4]);

        switch (stats[5])
        {
            case "N":
                color = InvokeColorReq.None;
                //Debug.Log("Noncolored Activation");
                break;

            case "R":
                color = InvokeColorReq.Red;
                //Debug.Log("Red Activation");
                break;

            case "B":
                color = InvokeColorReq.Blue;
                //Debug.Log("Blue Activation");
                break;

            case "Y":
                color = InvokeColorReq.Yellow;
                //Debug.Log("Yellow Activation");
                break;

            default:
                color = InvokeColorReq.None;
                Debug.Log("Error: Fate Activation Color: '" + stats[5] + "' does not exist.");
                break;
        }


        currentCard = new gameCard(cardName, cardType, cardCost, cardVP, cardPower, color);

        return currentCard;
    }
}