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
        descrptionLabel.text = getColorDescription(card.DescriptionPerk);
        descriptionSmallLabel.text = getColorDescription(card.DescriptionMain);
    }

    string getColorDescription(string description)
    {
        description = description.Replace("GPU", "[FF0000][b]GPU[/b][-]");
        description = description.Replace("RAM", "[00FF00][b]RAM[/b][-]");
        description = description.Replace("CPU", "[0000FF][b]CPU[/b][-]");

        return description;
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
