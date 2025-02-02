using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundinator : MonoBehaviour
{
    private int currentRound;

    private int tokenPlayer = 0;
    private int nextActivePlayer;

    private int cardsToPlay = 3;

    public fateSO fateScriptable;
    private CardDataSO dataScriptable;
    public cardDrawSO bankScriptable;
    
    public PlayerManager playerinator;
    public BasicEventSO matchCreated;

    //TODO: Change this to allow starting mid-game.
    public bool newGame = true;

    //These will be chosen by players at match start.
    public int numPlayers;
    public hirelingChoice[] startHirelings;

    // Start is called before the first frame update
    void Start()
    {
        dataScriptable = Resources.Load<CardDataSO>(constantStrings.cardDataLoc);
        dataScriptable.setUpStats();
        initBanks();
        
        if(newGame)
        {
            startNewGame(startHirelings);
            startNewRound();
        }
        
        matchCreated.Raise();
    }

    public void startNewRound()
    {
        //Reset CardsToPlay to 3 (in case Fortune changed it last round)
        cardsToPlay = 3;

        //Advances the current round number by one.
        currentRound++;
        Debug.Log("Starting Round " + currentRound + ".");
        
        //Triggers feed the toad, records the number of times the token should be passed.
        fateScriptable.feedTheToad();
        int passTimes = fateScriptable.getPassCount();
        Debug.Log("The token will be passed " + passTimes + " times.");
        
        //Uses the % function to figure out who gets the token next
        tokenPlayer = (tokenPlayer + passTimes) % numPlayers;
        Debug.Log("Player " + tokenPlayer + " has the token.");
        nextActivePlayer = tokenPlayer;

        //Arcana are triggered in ascending fate value
        //Magician is handled by fateScriptable(sets wands/cups/swords to true, disables all others)

        //Tower: Before playing cards, you may discard your hand and draw 4 new cards

        //Fool: Each player may choose to draw and immediately trash one card (are machines discarded?)

        //Judgement: Triggers after all cards are played.

        //Fortune: Effect varies based on the other two cards
        fortuneEffects();

        //Trigger the play phase for all units
        playerinator.playPhase(cardsToPlay);
    }

    private void startNewGame(hirelingChoice[] hirelings)
    {
        currentRound = 0;
        //Generate players with the default start, along with their choice of hirelings.
        playerinator.generatePlayers(true, numPlayers, hirelings);
        Debug.Log("Generating " + numPlayers + " players");
        playerinator.drawPhase();
    }

    private void initBanks()
    {
        bankScriptable.initDecks();

        bankScriptable.generateNewDecks(1, 1, 0);
        bankScriptable.fillAllBanks();
    }

    private void fortuneEffects()
    {

        int fortPow = fateScriptable.getFortunePower();
        
        //Debugging: log the value recieved.
        Debug.Log("Fortune Value: " + fortPow);

        if (fortPow < 2)
        {
            Debug.Log("This is not a fortune round");
            return;
        }
        if(fortPow < 6)
        {
            Debug.Log("Effect: No Tinkerer Phase");
            return;
        }
        if(fortPow < 7)
        {
            Debug.Log("Effect: No Wicked Phase");
            return;
        }
        if(fortPow < 10)
        {
            Debug.Log("Effect: Must play 4 cards.");
            cardsToPlay = 4;
            return;
        }
        if(fortPow < 13)
        {
            Debug.Log("Effect: Machines do not function");
            return;
        }
        Debug.Log("Error: Value out of range");
        return;
    }

    //Basic reference commands
    public int getNumPlayers() { return numPlayers; }

    public int getTokenPlayer() {  return tokenPlayer; }

    public int getCurrentRound() {  return currentRound; }
}
