using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static bankTester;
using static UnityEditor.LightingExplorerTableColumn;

public class testStatics : MonoBehaviour
{
    public static string getStatString(gameCard currentCard, cardStat dataType)
    {
        string result = "";

        switch (dataType)
        {
            case cardStat.NAME:
                result = currentCard.getName();
                break;

            case cardStat.TYPE:
                CardType currentType = currentCard.getCardType();
                switch (currentType)
                {
                    case CardType.Junk:
                        result = "Junk";
                        break;

                    case CardType.Machine:
                        result = "Machine";
                        break;

                    case CardType.Wildling:
                        result = "Wildling";
                        break;

                    case CardType.Tinkerer:
                        result = "Tinkerer";
                        break;

                    case CardType.Wicked:
                        result = "Wicked";
                        break;
                }
                break;

            case cardStat.COST:
                result = currentCard.getCost().ToString();
                break;

            case cardStat.VP:
                result = currentCard.getVictoryPoints().ToString();
                break;

            case cardStat.POW:
                result = currentCard.getPower().ToString();
                break;

            default:
                InvokeColorReq cardColor = currentCard.getColorReq();
                switch (cardColor)
                {
                    case InvokeColorReq.Red:
                        result = "Red";
                        break;

                    case InvokeColorReq.Blue:
                        result = "Blue";
                        break;

                    case InvokeColorReq.Yellow:
                        result = "Yellow";
                        break;

                    default:
                        result = "None";
                        break;
                }
                break;

        }

        return result;
    }
}
