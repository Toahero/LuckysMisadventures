using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HirelingDisplay : MonoBehaviour
{
    public SpriteRenderer topCard;
    private const string PLAY_CARDS_ADD = "Images/Cards/";

    public void UpdateHireling(string hirelingName)
    {
        string cardAddress = PLAY_CARDS_ADD + hirelingName;
        topCard.sprite = Resources.Load<Sprite>(cardAddress);
    }
}
