using UnityEngine;
using System.Collections;
using CraftCore;
using System;

public class CardViewController : MonoBehaviour {

    [SerializeField]
    UISprite partImage;

    [SerializeField]
    UILabel descrptionLabel;
    [SerializeField]
    UILabel descriptionSmallLabel;

    [SerializeField]
    UILabel heatCost;

    [SerializeField]
    GameObject upg2;

    [SerializeField]
    GameObject upg3;


	[SerializeField]
	GameObject cpuImage;

	[SerializeField]
	GameObject gpuImage;

	[SerializeField]
	GameObject ramImage;

	[SerializeField]
	GameObject utiImage;

    Card card;

    public void onPress()
    {
        Game.Instance.getManager().getBoardViewController().onCardPress(this);
        Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Generic-pickup");
        transform.localScale = new Vector3 (0.77f, 0.77f);
    }

    public void setup(CraftCore.Card _card) {
        card = _card;
        switch (card.Type) {

            case EnergyType.Black:
				utiImage.SetActive(true);
                break;
            case EnergyType.Red:
				gpuImage.SetActive(true);
                break;
            case EnergyType.Green:
				ramImage.SetActive(true);	
                break;
            case EnergyType.Blue:
				cpuImage.SetActive(true);
                break;
        }

        heatCost.text = card.HeatPrice.ToString();
        descrptionLabel.text = Utils.getColorDescription(card.DescriptionPerk);
		descriptionSmallLabel.text = Utils.getColorDescription(card.DescriptionMain);

        upg2.SetActive(card.UpgradeLevel == 1);
        upg3.SetActive(card.UpgradeLevel == 2);
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public Card getCard()
    {
        return card;
    }

    public void disableTouch()
    {
        GetComponent<UIDragDropItem>().interactable = false;
        GetComponent<BoxCollider>().enabled = false;
    }

}
