using UnityEngine;
using System.Collections;
using CraftCore;
using System;

public class VictoryPanelController : MonoBehaviour {

    [SerializeField]
    UILabel resultScoreLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void setup(GameSession session)
    {
        resultScoreLabel.text = session.ResultScore().ToString();
    }
}
