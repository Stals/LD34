using UnityEngine;
using System.Collections;
using CraftCore;

public class CardViewController : MonoBehaviour {

	[SerializeField]
	UISprite partImage;

	[SerializeField]
	UILabel descrptionLabel;

    public void onPress()
    {
        Game.Instance.getManager().getBoardViewController().onCardPress(this);
    }

	public void setup(CraftCore.Card card){
		switch (card.Type) {

			case EnergyType.Black:
			partImage.color = Color.black;
				descrptionLabel.text = "Black";
				break;
			case EnergyType.Red:
			partImage.color = Color.red;
				descrptionLabel.text = "Red";
				break;
			case EnergyType.Green:
			partImage.color = Color.green;
				descrptionLabel.text = "Green";
				break;
			case EnergyType.Blue:
			partImage.color = Color.blue;
				descrptionLabel.text = "Blue";
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
