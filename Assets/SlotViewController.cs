﻿using UnityEngine;
using System.Collections;
using CraftCore;

public class SlotViewController : MonoBehaviour {

	[SerializeField]
	UISprite background;

	public void setup(EnergyType energyType)
	{
		switch (energyType) {

		case EnergyType.Empty:
			background.alpha = 0f;
			GetComponent<BoxCollider>().enabled = false;
			break;
		case EnergyType.Black:
			background.color = Color.black;
			break;
		case EnergyType.Red:
			background.color = Color.red;
			break;
		case EnergyType.Green:
			background.color = Color.green;
			break;
		case EnergyType.Blue:
			background.color = Color.blue;
			break;
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}