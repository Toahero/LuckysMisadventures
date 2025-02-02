using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string soPrefix = "player";
    //set this to private later
    public PlayerData[] playerArray;

    public CardDataSO dataScriptable;

    public void generatePlayers(bool newGame, int numPlayers, hirelingChoice[] hirelingList)
    {
        dataScriptable = Resources.Load<CardDataSO>(constantStrings.cardDataLoc);

        playerArray = new PlayerData[numPlayers];
        string playerName;
        

        for (int i = 0; i < numPlayers; i++)
        {
            playerName = soPrefix + (i.ToString());
            
            if (newGame == true)
            {
                hirelingChoice hirelingPick;

                if (hirelingList.Length > i)
                {
                    hirelingPick = hirelingList[i];
                }
                else
                {
                    hirelingPick = hirelingChoice.WhiteRabbit;
                }

                //Spawns a new player with the given name and choice of starter hireling
                newPlayer(i, playerName, hirelingPick);
            }
        }
    }
	
	public void newPlayer(int playerNum, string name, hirelingChoice hireling)
	{
        int luckyChoice = 1;
        //Generate the player objects
        playerArray[playerNum] = new PlayerData(playerNum, true, hireling, luckyChoice);
		
	}

    //Round Phases

    public void drawPhase()
    {
        for (int i = 0; i < playerArray.Length; i++)
        {
            playerArray[i].drawTo(constantInts.baseDrawSize);
        }
    }

    public void playPhase(int playCount)
    {
        for(int i = 0; i < playerArray.Length;i++)
        {
            playerArray[i].playPhase(playCount);
        }
    }

    //Basic Return functions
    public PlayerData GetPlayer(int playerNum)
    {
        int inRangeNum = playerNum % playerArray.Length;
        
        return playerArray[inRangeNum];
    }

//Test programs for practicing deck operations on the various cards.
    public void ShuffleDrawTest(int testInt)
    {
        playerArray[testInt].shuffleDraw();
    }

    public void drawTest(int testPlayer)
    {
        playerArray[testPlayer].drawCard();
    }

    public void discardTest(int cardNum)
    {
        playerArray[0].discardCard(cardNum);
    }

    public void drawHandTest(int handSize)
    {
        playerArray[0].drawTo(handSize);
    }
}
