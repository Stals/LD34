using UnityEngine;
using System.Collections;
using System;

public class Player {

    int money;

    // Use this for initialization
    public Player() {
        money = 0;
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
