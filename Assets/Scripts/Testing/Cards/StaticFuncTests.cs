using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFuncTests : MonoBehaviour
{
    public PlayerManager playerinator;

    public cardDrawSO banks;

    public int playerNum;

    public DrawType bankSec;


    public List<int> playerMaxPower;
    public List<int> playerMinPower;

    public int bankMaxPow;
    public List<int> bankMaxCards;
    public int bankMin;


     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameCard[] bankCardArr = banks.getBankCards(bankSec);
        bankMaxPow = CardStatics.maxPowVal(bankCardArr);
        bankMaxCards = CardStatics.maxPowLoc(bankCardArr);
    }
}
