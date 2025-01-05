using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



[CreateAssetMenu(fileName = "ScriptableObjectFate", menuName = "ScriptableObject/fate")]
public class fateSO : ScriptableObject
{
	public const int FATE_SIZE = 3;
	
    public List<string> fateDraw;
    public string[] currentFate;
    public List<string> fateDiscard;
	
	public int[] fateValues;

    public BasicEventSO newFateCards;

    [SerializeField] private int roundNum;
    [SerializeField] private int fateSum;
    [SerializeField] private int passCount;

    [SerializeField] private bool cupRound;
    [SerializeField] private bool swordRound;
    [SerializeField] private bool wandRound;

    [SerializeField] private bool magician;
    [SerializeField] private bool tower;
    [SerializeField] private bool fool;
    [SerializeField] private bool judgment;
    [SerializeField] private bool fortune;
    [SerializeField] private bool death;


    public void initFate()
    {
        fateDraw = new List<string>();
        currentFate = new string[FATE_SIZE];
		fateValues = new int[FATE_SIZE];
        fateDiscard = new List<string>();

        roundNum = 0;
    }

    public void generateDeck()
    {
        int tempNum;

        for(int i = 0; i < 6; i++)
        {
            tempNum = i + 1;

            fateDraw.Add("cup-" + tempNum);
            fateDraw.Add("sword-" + tempNum);
            fateDraw.Add("wand-" + tempNum);
            fateDraw.Add("special-" + tempNum);
        }
        ShuffleAndDiscard();

        
    }

    

    public void feedTheToad()
    {
       

        if(roundNum > 0)
        {
            discardFate();
            clearData();
        }

        if (fateDraw.Count < 3)
        {
            cycleFate();
            
        }

        drawFate();
        setRoundData();
        ++roundNum;
        newFateCards.Raise();
    }

    private void drawFate()
    {
        for(int i = 0; i < 3;i++)
        {
            currentFate[i] = fateDraw[0];
            fateDraw.RemoveAt(0);

            fateValues[i] = getCardValue(currentFate[i]);
        }
    }

    private void discardFate()
    {
        for(int i = 0; i < 3; i++)
        {
            fateDiscard.Add(currentFate[i]);
        }
    }

    private void cycleFate()
    {
        fateDraw.Clear();
        discardFate();
        for(int i = 0; i< fateDiscard.Count;i++)
        {
            fateDraw.Add(fateDiscard[i]);
        }
        fateDiscard.Clear();
        ShuffleAndDiscard();
    }

    private void clearData()
    {
        fateSum = 0;
        passCount = 0;

        cupRound = false;
        swordRound = false;
        wandRound = false;

        magician = false;
        tower = false;
        fool = false;
        judgment = false;
        fortune = false;
        death = false;
    }

    private void clearFateCards()
    {
        for (int i = 0; i < FATE_SIZE; i++)
        {
            currentFate[i] = "";
            fateValues[i] = 0;
        }
    }
    private void setRoundData()
    {
        int fateVal;
        string cardName;
        clearData();

        for(int i = 0;i < FATE_SIZE;i++)
        {
            fateVal = getCardValue(currentFate[i]);
            cardName = getCardName(currentFate[i]);

            fateSum += fateVal;

            switch (cardName)
            {
                case "cup":
                    cupRound = true;
                    break;

                case "sword":
                    swordRound = true;
                    break;

                case "wand":
                    wandRound = true;
                    passCount++;
                    break;

                default:
                    passCount++;
                    switch (fateVal)
                    {
                        case 1:
                            magician = true;
                            break;

                        case 2:
                            tower = true;
                            break;

                        case 3:
                            fool = true;
                            break;

                        case 4:
                            judgment = true;
                            break;

                        case 5:
                            fortune = true;
                            break;

                        case 6:
                            death = true;
                            break;

                        default:
                            break;
                    }
                    break;
            }
        }

        if (magician)
        {
            tower = false;
            fool = false;
            judgment = false;
            fortune = false;
            death = false;

            cupRound = true;
            swordRound = true;
            wandRound = true;

        }
    }

    

    public void ShuffleAndDiscard()
    {
        int randNum;
        List<string> tempList = new List<string>();
        while (fateDraw.Count > 0)
        {
            randNum = UnityEngine.Random.Range(0, fateDraw.Count);
            tempList.Add(fateDraw[randNum]);
            fateDraw.RemoveAt(randNum);
        }
        fateDraw = tempList;

        for (int i = 0; i < 3; i++)
        {
            fateDiscard.Add(fateDraw[0]);
            fateDraw.RemoveAt(0);
        }
    }

    //Reference Functions

    public string getCardName(string cardID)
    {
        string[] cardinfo = cardID.Split('-');
        return cardinfo[0];
    }

    public int getCardValue(string cardID)
    {
        string[] cardInfo = cardID.Split('-');

        int cardPower = Int32.Parse(cardInfo[1]);
        return cardPower;
    }

    public int getFortunePower()
    {
        if (!fortune)
        {
            return 0;
        }
        int fortuneSum = 0;

        for(int i = 0; i < FATE_SIZE; i++)
        {
            if (currentFate[i] != "special-5")
            {
                fortuneSum += fateValues[i];
            }
        }
        return fortuneSum;
    }

    //Basic variable references
    public string getFateID(int cardPos)
    {
        if((cardPos < 0) || (cardPos >= FATE_SIZE))
        {
            Debug.Log("Fate Error: the value " + cardPos + " is out of range.");
            return "cup-1";
        }

        if (currentFate[cardPos] == null)
        {
            Debug.Log("Fate error: There is no card currently stored at position " + cardPos);
            return "cup-1";
        }

        return currentFate[cardPos];
    }

    public int getRoundNum()
    {
        return roundNum;
    }

    public int getFateSum()
    {
        return fateSum;
    }

    public int getPassCount()
    {
        return passCount;
    }


    //Fate Checkers
    public bool isCup()
    {
        return cupRound;
    }

    public bool isSword()
    {
        return swordRound;
    }

    public bool isWand()
    {
        return wandRound;
    }

    public bool ismagician()
    {
        return magician;
    }
    public bool isTower()
    {
        return tower;
    }

    public bool isFool()
    {
        return fool;
    }
    public bool isJudgment()
    {
        return judgment;
    }
    public bool isFortune()
    {
        return fortune;
    }
    public bool isDeath()
    {
        return death;
    }

}
