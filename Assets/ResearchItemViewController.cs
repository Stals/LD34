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

    [SerializeField]
    UISprite selector;

	public StatUpgrade upgrade;

	public void setup(StatUpgrade _upgrade){
		upgrade = _upgrade;

		nameLabel.text = upgrade.description;
		bonusLabel.text = "(" + Utils.getColorDescription(upgrade.bonusText ()) + ")";

		if (!upgrade.isEnabled) {
			priceLabel.text = upgrade.cost + "$";
		} else {
			priceLabel.text = "";
            nameLabel.color = new Color(97f/255f, 168f / 255f, 0f / 255f);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void onItemPress()
    {
        selector.alpha = UIToggle.current.value ? 1f : 0f;
    }
}
