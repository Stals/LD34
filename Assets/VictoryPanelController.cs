﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;
using System;

public class VictoryPanelController : MonoBehaviour {

    [SerializeField]
    UILabel resultScoreLabel;

    [SerializeField]
    UILabel newMoneyLabel;

    [SerializeField]
    List<GameObject> cardHolders;

    [SerializeField]
    GameObject cardPrefab;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    int convertScoreToMoney(float score) {
        return (int)(score * 4);
    }

    List<DeckCombiner.CardDublicating> shuffleCards(List<DeckCombiner.CardDublicating> alpha) {
        for (int i = 0; i < alpha.Count; i++)
        {
            DeckCombiner.CardDublicating temp = alpha[i];
            int randomIndex = UnityEngine.Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
        return alpha;
    }

    public void setup(GameSession session)
    {
        resultScoreLabel.text = session.ResultScore().ToString();

        int reward = convertScoreToMoney(session.ResultScore());
        newMoneyLabel.text = reward.ToString();
        Game.Instance.getPlayer().addMoney(reward);

        var cards = shuffleCards(Game.Instance.getDeckCombiner().Cards);

        int i = 0;
        foreach (var holder in cardHolders) {
            Card card = cards[i].card;

            var cardController = NGUITools.AddChild(holder, cardPrefab).GetComponent<CardViewController>();
            cardController.setup(card);
            cardController.disableTouch();

            ++i;
        }

        updateButtons();
    }

    void updateButtons()
    {
        foreach (var holder in cardHolders)
        {
            Card card = holder.GetComponentInChildren<CardViewController>().getCard();
            UIButton buyButton = holder.GetComponentInChildren<UIButton>();

            if (card.UpgradeLevel == 2) { // is max
                buyButton.SetState(UIButtonColor.State.Disabled, true);
                buyButton.isEnabled = false;
                buyButton.GetComponentInChildren<UILabel>().text = "";
                return;
            }

            int upgradeCost = getPriceForUpgrade(card.UpgradeLevel);
            if (Game.Instance.getPlayer().getMoney() < upgradeCost)
            {
                buyButton.SetState(UIButtonColor.State.Disabled, true);
                buyButton.isEnabled = false;
            }

            buyButton.GetComponentInChildren<UILabel>().text = upgradeCost + " $";
        }
    }


    int getPriceForUpgrade(int level)
    {
        if (level == 0)
        {
            return 50;
        }
        else {
            return 350;
        }
    }


    public void onPurchasePressed(GameObject cardHolder)
    {
        Card card = cardHolder.GetComponentInChildren<CardViewController>().getCard();
        int upgradeCost = getPriceForUpgrade(card.UpgradeLevel);
        if (Game.Instance.getPlayer().getMoney() >= upgradeCost)
        {
            cardHolder.GetComponentInChildren<UIButton>().SetState(UIButtonColor.State.Disabled, true);
            cardHolder.GetComponentInChildren<CardViewController>().getCard().UpgradeLevel += 1;

            Game.Instance.getPlayer().addMoney(-upgradeCost);
        }
        else {
            //TODO because buttons are disabled - impossible
        }

        updateButtons();
    }
}
