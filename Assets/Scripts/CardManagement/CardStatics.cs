using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStatics 
{
    public static List<int> minPowLoc(gameCard[] cardArray)
    {

        if (cardArray.Length <= 0)
        {
            return null;
        }

        int minPower = minPowVal(cardArray);

        List<int> minCards = new List<int>();

        for (int i = 0; i < cardArray.Length; i++)
        {
            if (cardArray[i].getPower() == minPower)
            {
                minCards.Add(i);
            }
        }

        return minCards;
    }

    public static int minPowVal(gameCard[] cardArray)
    {
        int minPower = cardArray[0].getPower();

        for (int i = 0; i < cardArray.Length; i++)
        {
            int cardPower = cardArray[i].getPower();

            if (cardPower < minPower)
            {
                minPower = cardPower;
            }
        }
        return minPower;
    }

    public static List<int> maxPowLoc(gameCard[] cardArray)
    {
        if (cardArray.Length <= 0)
        {
            return null;
        }

        int maxPower = maxPowVal(cardArray);

        

        List<int> maxCards = new List<int>();

        for (int i = 0; i < cardArray.Length; i++)
        {
            if (cardArray[i].getPower() == maxPower)
            {
                maxCards.Add(i);
            }
        }

        return maxCards;
    }

    public static int maxPowVal(gameCard[] cardArray)
    {
        int maxPower = cardArray[0].getPower();

        for (int i = 0; i < cardArray.Length; i++)
        {
            int cardPower = cardArray[i].getPower();

            if (cardPower > maxPower)
            {
                maxPower = cardPower;
            }
        }

        return maxPower;
    }
}
