using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ioBus : MonoBehaviour
{
    public Console[] consoleArr;

    public void Start()
    {
        createConsoles(4);
    }

    public void createConsoles(int numPlayers)
    {
        consoleArr = new Console[numPlayers];

        for (int i = 0; i < numPlayers; i++)
        {
            consoleArr[i] = new GameObject().AddComponent<Console>();
        }
    }

    public void initializeCards(int playerNum, string[] draw, string[] hand, string[] inPlay, string[] discard)
    {
        Console console = consoleArr[playerNum];
        console.fullUpdateDeck(CardLocation.Draw, draw);
        console.fullUpdateDeck(CardLocation.Hand, hand);
        console.fullUpdateDeck(CardLocation.Play, inPlay);
        console.fullUpdateDeck(CardLocation.Disc, discard);
    }
}
