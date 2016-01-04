using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CraftCore;

public class StatUpgrade : IComparable
{
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
        if (energyType == EnergyType.Black) {
            appendix = "Energy";
        }else if(energyType == EnergyType.Empty) {
            return "+ Boards";
		}else{
            appendix = Utils.NameFromType(energyType);
        }

		return "+" + bonus.ToString () + " " + appendix;
	}

    public int CompareTo(object obj)
    {
        StatUpgrade upgrade = obj as StatUpgrade;
        if (cost > upgrade.cost)
        {
            return 1;
        }
        if (cost < upgrade.cost)
        {
            return -1;
        }

        // The orders are equivalent.
        return 0;
    }
}

public class Player {

	public int motherboardNumber = 0;
    int money = 0;
    List<StatUpgrade> statUpgrades;

    public List<StatUpgrade> getStatUpgrades()
    {
        return statUpgrades;
    }

    // Use this for initialization
    public Player() {

        statUpgrades = new List<StatUpgrade>();

        statUpgrades.Add(new StatUpgrade(EnergyType.Red, 1, "Integrated audio", 25));
        statUpgrades.Add(new StatUpgrade(EnergyType.Red, 1, "Integrated GPU", 150));
        statUpgrades.Add(new StatUpgrade(EnergyType.Red, 2, "Power", 430));

        statUpgrades.Add(new StatUpgrade(EnergyType.Blue, 1, "CMOS", 25));
        statUpgrades.Add(new StatUpgrade(EnergyType.Blue, 1, "SATA", 250));
        statUpgrades.Add(new StatUpgrade(EnergyType.Blue, 2, "Memory", 300));

        statUpgrades.Add(new StatUpgrade(EnergyType.Green, 1, "DIMM modules", 100));
        statUpgrades.Add(new StatUpgrade(EnergyType.Green, 1, "Exp. cards", 250));
        statUpgrades.Add(new StatUpgrade(EnergyType.Green, 3, "IO Chip", 400));

        // unlock more energy
        statUpgrades.Add(new StatUpgrade(EnergyType.Black, 1, "Socket update", 25));
        statUpgrades.Add(new StatUpgrade(EnergyType.Black, 1, "Generator", 1000));
        //statUpgrades.Add(new StatUpgrade(EnergyType.Black, 1, "A clock generator", 1000));

        // unlock motherboards
        statUpgrades.Add(new StatUpgrade(EnergyType.Empty, 1, "Boards Layouts", 75));
        statUpgrades.Add(new StatUpgrade(EnergyType.Empty, 1, "Boards Layouts", 75));

		// sort by cost
        statUpgrades.Sort();
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

    public float getBoardsAvalible()
    {
        int boardUpgradesUnlocked = 1;
        foreach(var upg in statUpgrades) {
            if ((upg.energyType == EnergyType.Empty) && upg.isEnabled) {
                ++boardUpgradesUnlocked;
            }
        }

		float p = 0.5f;
		// give 0.25 for each next upgrade
		p += (boardUpgradesUnlocked - 1) * 0.25f;

		return p;
    }

}
