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

    Card card;

    public void onPress()
    {
        Game.Instance.getManager().getBoardViewController().onCardPress(this);
    }

    public void setup(CraftCore.Card _card) {
        card = _card;
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
