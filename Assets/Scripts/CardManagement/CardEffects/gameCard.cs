using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum cardStat { NAME, TYPE, COST, VP, POW, COLOR }
public enum CardType{Junk, Machine, Tinkerer, Wicked, Wildling}
public enum InvokeColorReq {None, Red, Blue, Yellow}

public class gameCard{

	//Name of the card
	public string name;
	
	//What faction the card belongs to
	public CardType cardType;
	
	//The cost to purchase this card from a bank
	public int cost;
	
	//The victory points that this card adds to your deck.
	public int victoryPoints;
	
	//For Wildlings, the purchase power from this card. For Tinkerers/Wickeds, the power they give for their round.
	public int power;
	
	//The color of fate needed to activate a card. Cards with "None" can be activated regardless of fate.
	public InvokeColorReq invokeColor;
	
	//Basic constructor
	public gameCard(string _name, CardType _type, int _cost, int _victoryPoints, int _power, InvokeColorReq _color){
		name = _name;
		cardType = _type;
		cost = _cost;
		victoryPoints = _victoryPoints;
		power = _power;
		invokeColor = _color;
		
	}

	public string getName()
	{
		return name;
	}
	
	public CardType getCardType()
	{
		return cardType;
	}

	public int getCost()
	{
		return cost;
	}
	public int getVictoryPoints()
	{
		return victoryPoints;
	}
	public int getPower()
	{
		return power;
	}

	public InvokeColorReq getColorReq()
	{
		return invokeColor;
	}
}
