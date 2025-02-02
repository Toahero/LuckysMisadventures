using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConsoleManager : MonoBehaviour
{
    public ioBus bus;

    [SerializeField] private Console[] consoles;

    public void Start()
    {
        createConsoles(4);
    }

    public void createConsoles(int numPlayers)
    {
        consoles = new Console[numPlayers];

        for(int i = 0; i < numPlayers; i++)
        {
            consoles[i] = new GameObject().AddComponent<Console>();
        }
    }
}
