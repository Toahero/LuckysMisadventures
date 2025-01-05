using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class datainator : MonoBehaviour
{
    public CardDataSO dataSO;

    //input output test
    /*public string cardName;
    public gameCard outputCard;
    public string outputName;
    public int outputCost;
    */
    private string prevCardName;

    // Start is called before the first frame update
    void Start()
    {
        //dataSO.setUpStats();
    }

    /*void Update()
    {
        if(cardName != prevCardName) 
        {
            Debug.Log("Searching for: " + cardName);
            if(dataSO.dictionaryKeys.Contains(cardName))
            {
                Debug.Log("Match Found.");
                outputCard = dataSO.statDictionary[cardName];
                outputName = outputCard.name;
                outputCost = outputCard.cost;
            }
            prevCardName = cardName;
        }
    }*/

}
