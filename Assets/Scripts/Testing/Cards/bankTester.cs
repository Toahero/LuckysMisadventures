using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bankTester : MonoBehaviour
{
    
    public cardStat dataType;

    public cardDrawSO banks;

    public DrawType bankSec;

    public int cardNum = 0;

    public string result;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameCard currentCard = banks.getCard(bankSec, cardNum);

        result = testStatics.getStatString(currentCard, dataType);
    }

    
}
