using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardStrings
{
    static char locSeperator = ':';
    static char cardSeperator = ',';
    static char deckSeperator = '-';
    
	//Recording all a player's cards
	public static string playerCardsToString(List<string> drawDeck, List<string> playerHand, List<string> inPlay, List<string> discard){
		string outputString = "";
		outputString += deckToString(CardLocation.Draw, drawDeck);
		outputString += deckToString(CardLocation.Hand, playerHand);
		outputString += deckToString(CardLocation.Play, inPlay);
		outputString += deckToString(CardLocation.Disc, discard);
		return outputString;
	}
	
	//Storing for a single Deck
	public static string deckToString(CardLocation loc, List<string> nameString)
    {
		
        //Initialize the string with the location and a seperator char
        string outputString = loc.ToString() + locSeperator;

        //Add each name on the string to the list, with a comma seperating.
        for (int i = 0; i < nameString.Count - 1; i++)
        {
            outputString += nameString[i] + cardSeperator;
        }
        outputString += nameString[nameString.Count - 1];

        return outputString;
    }

    public static CardLocation deckStringToLoc(string deckString)
    {
        string[] stringArr = deckString.Split(locSeperator);

        string locString = stringArr[0];

        switch(locString)
        {
            case "Draw":
                return CardLocation.Draw;

            case "Hand":
                return CardLocation.Hand;

            case "Play":
                return CardLocation.Play;

            case "Disc":
                return CardLocation.Disc;

            default:
                Debug.Log("Error: Location " + locString + " does not exist.");
                return CardLocation.Disc;
        }
    }

    public static string[] deckStringToNameArr(string deckString)
    {
        string[] stringArr = deckString.Split(locSeperator);

        string fusedList = stringArr[1];

        return fusedList.Split(cardSeperator);
    }
}
