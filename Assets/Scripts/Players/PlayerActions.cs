using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public playerSO player;
    public cardDrawSO cardBanks;

    public DrawType tempBank;
    public int tempLoc;

    public bool debug;

    public void testBuy()
    {
        buyCard(tempBank, tempLoc);
    }

    public void buyCard(DrawType bank, int cardLoc)
    {
        int money = player.getMoney();

        gameCard boughtCard = cardBanks.getCard(bank, cardLoc);
        
        if(boughtCard != null)
        {
            int cardCost = boughtCard.getCost();

            if (debug)
            {
                Debug.Log("Bank: " + bank + ", Card Loc: " + cardLoc + ", Card Cost: " + cardCost + ", Player Money: " + money);
            }

            if (cardCost <= money)
            {
                if (debug)
                {
                    Debug.Log("Card is buyable");
                }

                player.addCard(CardLocation.Disc, boughtCard.getName());
                cardBanks.takeBankCard(bank, cardLoc);

                player.spendMoney(cardCost);
            }
            else if (debug)
            {
                Debug.Log("Card is not buyable.");
            }
        }
    }
}
