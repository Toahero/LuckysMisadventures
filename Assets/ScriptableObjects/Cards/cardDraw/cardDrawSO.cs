using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public enum DrawType { Junk, Maven, Hireling }


[CreateAssetMenu(fileName = "ScriptableObjectCardDraw", menuName = "ScriptableObject/cardDraw")]


public class cardDrawSO : ScriptableObject
{
    private List<gameCard> junkDeck;
	private gameCard[] junkBank;
	
	private List<gameCard> mavenDeck;
    private gameCard[] mavenBank;
	
	private List<gameCard> hirelingDeck;
	public gameCard topHireling;

    public CardDataSO cardData;

    [SerializeField] private string[] junkBankNames;
    [SerializeField] private string[] mavenBankNames;
    [SerializeField] private string topHirelingName;

	public BankEventsSO junkDisplayTrigger;
	public BankEventsSO mavenDisplayTrigger;
	public StringEventSO updateTopHireling;

	public gameCard getCard(DrawType cardType, int cardLoc)
	{
		int resizedLoc = cardLoc % constantInts.bankSize;

		switch (cardType) {
			case DrawType.Junk:
				return junkBank[resizedLoc];	

			case DrawType.Maven:
				return mavenBank[resizedLoc];

			case DrawType.Hireling:
				return topHireling;

			default:
				return null;
		}
	}

	public string getCardName(DrawType cardType, int cardLoc) {

        int resizedLoc = cardLoc % constantInts.bankSize;

        switch (cardType)
		{
			case DrawType.Junk:
				return junkBank[resizedLoc].getName(); ;
			
			case DrawType.Maven:
				return mavenBank[resizedLoc].getName();
			
			case DrawType.Hireling:
				return topHireling.getName();

			default: return null;
		}
	}

	

	public void takeBankCard(DrawType targetBank, int cardLoc)
	{
        int resizedLoc = cardLoc % constantInts.bankSize;

        switch (targetBank)
		{
			case DrawType.Junk:
				junkBank[resizedLoc] = null;
				junkBankNames[resizedLoc] = null;

				break;

			case DrawType.Maven:
				mavenBank[resizedLoc] = null;
				break;

			case DrawType.Hireling:
				topHireling = null;
				break;

			default:
				break;
		}

        drawNewCard(targetBank, resizedLoc);
    }

	private void drawNewCard(DrawType targetBank, int cardLoc)
	{
		int resizedLoc = cardLoc % constantInts.bankSize;

		switch(targetBank)
		{
			case DrawType.Junk:
				if ((junkBank[resizedLoc] == null) && junkDeck.Count > 0)
				{
					junkBank[resizedLoc] = junkDeck[0];
					junkDeck.RemoveAt(0);

					//TODO: Switch the general update bank command for a draw-specific one
					junkDisplayTrigger.BankUpdate(getNameArray(DrawType.Junk));
				}
				break;

			case DrawType.Maven:
				if ((mavenBank[resizedLoc] == null) && junkDeck.Count > 0)
				{
					mavenBank[resizedLoc] = mavenDeck[0];
					mavenDeck.RemoveAt(0);

					//TODO: Ditto as with Junk Bank
					mavenDisplayTrigger.BankUpdate(getNameArray(DrawType.Maven));
				}
				break;

			case DrawType.Hireling:
				if(topHireling == null && hirelingDeck.Count > 0)
				{
					topHireling = hirelingDeck[0];
					hirelingDeck.RemoveAt(0);

					updateTopHireling.Raise(topHireling.getName());
				}
				break;
		}
	}

	public void fillAllBanks()
	{
		fillBank(DrawType.Junk);
		fillBank(DrawType.Maven);
		fillBank(DrawType.Hireling);
	}

	public void fillBank(DrawType currentBank)
	{
		switch(currentBank)
		{
			case DrawType.Junk:
				for(int i = 0; i < junkBank.Length; i++)
				{
					if ((junkBank[i] == null) && (junkDeck.Count > 0))
					{
						junkBank[i] = junkDeck[0];
						junkDeck.RemoveAt(0);
					}
				}

				//Update the List of Names, then signal the display to render the new cards;
                updateBankNames(currentBank);
                junkDisplayTrigger.BankUpdate(getNameArray(DrawType.Junk));
				break;

			case DrawType.Maven:
				for(int i = 0; i < mavenBank.Length; i++)
				{
					if (mavenBank[i] == null && (mavenDeck.Count > 0))
					{
						mavenBank[i] = mavenDeck[0];
						mavenDeck.RemoveAt(0);
					}
				}
				//Update the List of names, then signal the display
                updateBankNames(currentBank);
                mavenDisplayTrigger.BankUpdate(getNameArray(DrawType.Maven));
                break;

			case DrawType.Hireling:
				if((topHireling == null) && (hirelingDeck.Count > 0))
				{
                    topHireling = hirelingDeck[0];
                    hirelingDeck.RemoveAt(0);
                }
				//Update then signal
                updateBankNames(currentBank);
				updateTopHireling.Raise(topHireling.getName());

                break;
		}

        updateBankNames(currentBank);
    }
	
	public void updateBankNames(DrawType currentBank)
	{
		switch (currentBank)
		{
			case DrawType.Junk:
                for (int i = 0; i < junkBank.Length; i++)
                {
                    junkBankNames[i] = junkBank[i].getName();
                }
				break;

			case DrawType.Maven:
				for(int i = 0;i < mavenBank.Length; i++)
				{
					mavenBankNames[i] = mavenBank[i].getName();
				}
				break;

			case DrawType.Hireling:
				topHirelingName = topHireling.getName();
				break;
        }
	}

	//Setup Functions

    public void initDecks()
    {
		hirelingDeck = new List<gameCard>();
		
		junkDeck = new List<gameCard>();
		junkBank = new gameCard[constantInts.bankSize];
		
		mavenDeck = new List<gameCard>();
		mavenBank = new gameCard[constantInts.bankSize];

		junkBankNames = new string[constantInts.bankSize];
		mavenBankNames = new string[constantInts.bankSize];

    }

	public void generateNewDecks(int clockmakersPicked, int wingedMonkeysPicked, int whiteRabbitsPicked)
	{
		generateJunkDeck();
		generateMavenDeck();
		generateHirelingDeck(clockmakersPicked, wingedMonkeysPicked, whiteRabbitsPicked);
	}
	
	private void generateJunkDeck(){
		
		string junkInvString;
		string[] junkInvRows;
		string[] junkInvLine;

		string cardName;
		int cardQty;

		
		junkInvString = Resources.Load<TextAsset>("Data/junkInv").ToString();
		junkInvRows = junkInvString.Split('\n');

		for(int i = 1; i < junkInvRows.Length - 1; i++)
		{
			junkInvLine = junkInvRows[i].Split(',');
			cardName = junkInvLine[0];
			cardQty = int.Parse(junkInvLine[1]);

			addCard(DrawType.Junk, cardName, cardQty);
		}
		
		shuffleDeck(DrawType.Junk);
	}

	private void generateMavenDeck()
	{
		string mavenInvString;
		string[] mavenInvRows;
		string[] mavenInvLine;

		string cardName;
		int cardQty;

		mavenInvString = Resources.Load<TextAsset>("Data/mavenInv").ToString();
		mavenInvRows = mavenInvString.Split('\n');

		for(int i = 1; i < mavenInvRows.Length - 1; i++)
		{
			mavenInvLine = mavenInvRows[i].Split(',');
			cardName = mavenInvLine[0];
			cardQty = int.Parse(mavenInvLine[1]);

			addCard(DrawType.Maven, cardName, cardQty);
		}
		shuffleDeck(DrawType.Maven);
	}

	private void generateHirelingDeck(int cmChosen, int wmChosen, int wrChosen)
	{
		int numToAdd;
		//Add Clockmakers
		numToAdd = 8 - cmChosen;
		addCard(DrawType.Hireling, "Clockmakers", numToAdd);

		//Add Winged Monkeys
		numToAdd = 8 - wmChosen;
		addCard(DrawType.Hireling, "Winged Monkey", numToAdd);

		//Add White Rabbits
		addCard(DrawType.Hireling, "White Rabbit", numToAdd);

		shuffleDeck(DrawType.Hireling);

	}

	private void addCard(DrawType selectedDeck, string cardName, int cardCount)
	{
        gameCard currentCard;
		//Debug.Log("Bank: spawning " + cardName);

		currentCard = cardData.statDictionary[cardName];
		int i;
		switch(selectedDeck)
		{
			case DrawType.Junk:
                
				for (i = 0; i < cardCount; i++)
                {
                    junkDeck.Add(currentCard);
					//display
                    //junkDeckNames.Add(currentCard.name);
                }
            break;

			case DrawType.Maven:
                for (i = 0; i < cardCount; i++)
                {
                    mavenDeck.Add(currentCard);
					//display
					//mavenDeckNames.Add(currentCard.name);
                }
			break;

			case DrawType.Hireling:
				for(i = 0;i < cardCount; i++)
				{
					hirelingDeck.Add(currentCard);
					//display
					//hirelingDeckNames.Add(currentCard.name);
				}
			break;

			default:
				Debug.Log("Error: Invalid Deck Choice");
			break;
        }
		
    }

	private void shuffleDeck(DrawType selectedDeck)
	{
		List<gameCard> tempDeck = new List<gameCard>();
		List<string> tempNameList = new List<string>();

		int cardPos;
		switch(selectedDeck)
		{
			case DrawType.Junk:
				while (junkDeck.Count > 0)
				{
					cardPos = Random.Range(0, junkDeck.Count);
					tempDeck.Add(junkDeck[cardPos]);
					junkDeck.RemoveAt(cardPos);

					//tempNameList.Add(junkDeckNames[cardPos]);
					//junkDeckNames.RemoveAt(cardPos);
				}
				junkDeck = tempDeck;
				//junkDeckNames = tempNameList;
				break;

			case DrawType.Maven:
				while(mavenDeck.Count > 0)
				{
					cardPos = Random.Range(0, mavenDeck.Count);
					tempDeck.Add(mavenDeck[cardPos]);
					mavenDeck.RemoveAt(cardPos);

					//tempNameList.Add(mavenDeckNames[cardPos]);
					//mavenDeckNames.RemoveAt(cardPos);
				}
				mavenDeck = tempDeck;
				//mavenDeckNames = tempNameList;
				break;

			case DrawType.Hireling:
				while(hirelingDeck.Count > 0)
				{
                    cardPos = Random.Range(0, hirelingDeck.Count);
                    tempDeck.Add(hirelingDeck[cardPos]);
                    hirelingDeck.RemoveAt(cardPos);

                    //tempNameList.Add(hirelingDeckNames[cardPos]);
                    //hirelingDeckNames.RemoveAt(cardPos);
                }
                hirelingDeck = tempDeck;
                //hirelingDeckNames = tempNameList;
                break;
        }
	}

	private void sortBank(DrawType bankPick)
	{
		switch (bankPick)
		{
			case DrawType.Junk:
				//Sort the Junk Bank
				break;

			case DrawType.Maven:
				//Sort the Maven Bank
				break;

			default: 
			
				break;
		}
	}

	private void swapCards(DrawType cardsLoc, int firstCard, int secCard)
	{
		gameCard tempCard;

		switch (cardsLoc)
		{
			case DrawType.Junk:
				tempCard = junkBank[firstCard];
				junkBank[firstCard] = junkBank[secCard];
				junkBank[secCard] = tempCard;

				junkDisplayTrigger.cardSwitched(firstCard, secCard);

				//temp: Using BankUpdate command until switch is implemented
				junkDisplayTrigger.BankUpdate(getNameArray(DrawType.Junk));

                break;

			case DrawType.Maven:
				tempCard = mavenBank[firstCard];
				mavenBank[firstCard] = mavenBank[secCard];
				mavenBank[secCard] = tempCard;

				mavenDisplayTrigger.cardSwitched(firstCard, secCard);

				//temp: Using BankUpdate command until switch is implemented
				mavenDisplayTrigger.BankUpdate(getNameArray(DrawType.Maven));
				break;

			default:
				break;
		}
	}

	public string[] getNameArray(DrawType cardBank)
	{
		string[] names = new string[constantInts.bankSize];

		switch (cardBank) {
			case DrawType.Junk:
				for(int i = 0; i < junkBank.Length; i++)
				{
					gameCard currentCard = junkBank[i];
					
					if(currentCard == null)
					{
						names[i] = null;
					}
					else
					{
						names[i] = currentCard.getName();
					}
				}
				return names;

		case DrawType.Maven:
			for(int i = 0; i < mavenBank.Length; i++)
				{
                    gameCard currentCard = mavenBank[i];

                    if (currentCard == null)
                    {
                        names[i] = null;
                    }
                    else
                    {
                        names[i] = currentCard.getName();
                    }
                }
			return names;

		default:
			return names;
		
		}
	}

	public gameCard[] getBankCards(DrawType banks)
	{
		switch(banks)
		{
			case DrawType.Junk:
				return junkBank;;

			case DrawType.Maven:
				return mavenBank;

			default:
				return junkBank;
		}

	}
}

