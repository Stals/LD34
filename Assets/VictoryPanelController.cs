using UnityEngine;
using System.Collections;
using CraftCore;
using System;

public class VictoryPanelController : MonoBehaviour {

    [SerializeField]
    UILabel resultScoreLabel;

    [SerializeField]
    UILabel newMoneyLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    int convertScoreToMoney(float score) {
        return (int)(score * 4);
    }

    internal void setup(GameSession session)
    {
        resultScoreLabel.text = session.ResultScore().ToString();

        int reward = convertScoreToMoney(session.ResultScore());
        newMoneyLabel.text = reward.ToString();
        Game.Instance.getPlayer().addMoney(reward);
    }
}
