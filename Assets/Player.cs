using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CraftCore;

public class StatUpgrade{
	public StatUpgrade(EnergyType _energyType, int _bonus, string _description, int _cost){
		energyType = _energyType;
		bonus = _bonus;
		description = _description;
		cost = _cost;

		isEnabled = false;
	}

	public string description;
	public int cost;
	public EnergyType energyType;
	public int bonus;

	public bool isEnabled;

	public string bonusText()
	{
		string appendix;
		if(energyType != EnergyType.Black){
			appendix = Utils.NameFromType (energyType);
		}else{
			appendix = "Energy";
		}

		return "+" + bonus.ToString () + " " + appendix;
	}
}

public class Player {

    int money;
	List<StatUpgrade> statUpgrades;

	public List<StatUpgrade> getStatUpgrades()
	{
		return statUpgrades;
	}

    // Use this for initialization
    public Player() {
        money = 0;
		// TODO sort by cost?
		statUpgrades = new List<StatUpgrade> ();

		statUpgrades.Add(new StatUpgrade(EnergyType.Red, 1, "Integrated audio chip", 50));
		statUpgrades.Add(new StatUpgrade(EnergyType.Red, 1, "Integrated graphics", 150));
		statUpgrades.Add(new StatUpgrade(EnergyType.Red, 2, "Power connectors", 430));

		statUpgrades.Add(new StatUpgrade(EnergyType.Blue, 1, "CMOS back up battery", 50));
		statUpgrades.Add(new StatUpgrade(EnergyType.Blue, 1, "SATA connector", 250));
		statUpgrades.Add(new StatUpgrade(EnergyType.Blue, 2, "Non-volatile memory", 300));

		statUpgrades.Add(new StatUpgrade(EnergyType.Green, 1, "DIMM modules", 100));
		statUpgrades.Add(new StatUpgrade(EnergyType.Green, 1, "Slots for expansion cards", 250));
		statUpgrades.Add(new StatUpgrade(EnergyType.Green, 3, "Super IO Chip", 400));

		statUpgrades.Add(new StatUpgrade(EnergyType.Black, 1, "CPU Socket update", 25));
		statUpgrades.Add(new StatUpgrade(EnergyType.Black, 1, "A clock generator", 650));
		statUpgrades.Add(new StatUpgrade(EnergyType.Black, 1, "A clock generator", 1000));
    }

    // Update is called once per frame
    void Update() {

    }

    public int getMoney()
    {
        return money;
    }

    public void addMoney(int v)
    {
        money += v;
    }
}
