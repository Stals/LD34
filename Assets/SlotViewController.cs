using UnityEngine;
using System.Collections;
using CraftCore;

public class SlotViewController : MonoBehaviour {

	[SerializeField]
	UISprite background;

    int x;
    int y;

    public void setup(EnergyType energyType, int _x, int _y)
	{
        x = _x;
        y = _y;

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


    void OnDrop(GameObject drag)
    {
        CardViewController cardView = drag.GetComponent<CardViewController>();
        if (!cardView) return;

        GetComponent<BoxCollider>().enabled = false;

        cardView.disableTouch();
        Game.Instance.getManager().getBoardViewController().onCardPlaced(x, y, cardView.getCard());
    }
}
