using UnityEngine;
using System.Collections;
using CraftCore;

public class ResearchItemViewController : MonoBehaviour {
	[SerializeField]
	UILabel nameLabel;

	[SerializeField]
	UILabel bonusLabel;

	[SerializeField]
	UILabel priceLabel;

	StatUpgrade upgrade;

	public void setup(StatUpgrade _upgrade){
		upgrade = _upgrade;

		nameLabel.text = upgrade.description;
		bonusLabel.text = "(" + Utils.getColorDescription(upgrade.bonusText ()) + ")";

		if (!upgrade.isEnabled) {
			priceLabel.text = upgrade.cost + "$";
		} else {
			priceLabel.text = "";
			nameLabel.color = Color.green;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
