using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankDisplayInator : MonoBehaviour
{
    private const string PLAY_CARDS_ADD = "Images/Cards/";

    public SpriteRenderer[] cardDisplays;
    public bool debug;


    public void fullUpdateBank(string[] cardIDs)
    {
        for(int i = 0; i < cardDisplays.Length; i++)
        {
            if (debug)
            {
                Debug.Log(cardIDs[i] + " is at: position " + i);
            }
            

            string cardAdd = PLAY_CARDS_ADD + cardIDs[i];
            cardDisplays[i].sprite = Resources.Load<Sprite>(cardAdd);
        }
    }
}
