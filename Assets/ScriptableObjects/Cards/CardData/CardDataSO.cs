using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectCardData", menuName = "ScriptableObject/CardData")]
public class CardDataSO : ScriptableObject
{
    
	public Dictionary<string, gameCard> statDictionary;	
	public List<string> dictionaryKeys;



    public gameCard cardFromName(string name)
    {
        if(statDictionary == null)
        {
            Debug.Log("Error: statDictionary not initialized");
            return null;
        }

        if(!statDictionary.ContainsKey(name))
        {
            Debug.Log("Error: statDictionary does not contain: " + name);
            return null;
        }
        else
        {
            return statDictionary[name];
        }
    }



    public void setUpStats(){
		initialize();
		setCards();
	}

	private void initialize(){
		statDictionary = new Dictionary<string, gameCard>();
		dictionaryKeys = new List<string>();
	}
	
	private void setCards()
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
            

            statDictionary.Add(cardName, newCard);
        }
    }

    
    private gameCard getCardStats(string[] stats)
    {
        string cardName;
        gameCard currentCard;
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
