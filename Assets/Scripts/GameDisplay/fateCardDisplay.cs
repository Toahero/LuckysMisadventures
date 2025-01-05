using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fateCardDisplay : MonoBehaviour
{
    public SpriteRenderer cardRenderer;
    private const string FATE_SPRITE_ADD = "Images/Fate/";

    public void newFateCard(string cardID)
    {
        string cardAddress = FATE_SPRITE_ADD + cardID;
        //Debug.Log("Fate Card Display: Loading card " + cardAddress);
        cardRenderer.sprite = Resources.Load<Sprite>(cardAddress);
    }
}
