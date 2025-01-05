using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanel : MonoBehaviour
{
    private const string PLAY_CARDS_ADD = "Images/Cards/";

    public GameObject players;

    public GameObject cardPrefab;

    public List<GameObject> cardList;

    public string[] cardNames;

    public void Start()
    {
        cardList = new List<GameObject>();

        addCards(cardNames);
    }

    public void addCards(string[] cardNames)
    {
        RectTransform rectangle = GetComponent<RectTransform>();
        
        //Get the space between cards
        float xLength = rectangle.rect.width;
        float xSpace = xLength / (cardNames.Length + 1);

        float yPos = (rectangle.sizeDelta.y / 2) + rectangle.pivot.y;

        for (int i = 0; i < cardNames.Length; i++)
        {
            float xPos = (xSpace * (i+1)) + (xSpace);

            
            Vector2 pos = new Vector2(xPos, yPos);
            addOneCard(cardNames[i], pos);
        }
    }

    private void addOneCard(string cardName, Vector2 cardPos)
    {
        
        GameObject newCard = Instantiate(cardPrefab, cardPos, Quaternion.identity, this.transform);
        CardDisplay cardDisplay = newCard.GetComponent<CardDisplay>();

        cardList.Add(newCard);

        cardDisplay.cardID = cardList.Count;

        newCard.GetComponent<Image>().sprite = Resources.Load<Sprite>(PLAY_CARDS_ADD + cardName);
    }

    public void UpdateCards(string[] cardNames)
    {
        clearCards();

        addCards(cardNames);
    }

    private void clearCards()
    {
        for(int i = cardList.Count -1; i >= 0; i--)
        {
            Destroy(cardList[i]);
        }
    }

    public void cardClicked(int cardID)
    {
        Debug.Log("Card at position " + cardID + " clicked");
        int adjID = cardID % cardList.Count;
        
    }
}
