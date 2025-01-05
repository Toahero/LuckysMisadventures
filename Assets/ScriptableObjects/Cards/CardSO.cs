using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObjectCard", menuName = "ScriptableObject/Card")]

public class CardSO : ScriptableObject
{
    public int cost;
    public int victoryPoints;
    public enum CardType { junk, machine, tinker, wicked, oddling}
    public CardType faction;
    public int power;

    public MonoBehaviour StaticEffect;
    public MonoBehaviour CardAction;
}
