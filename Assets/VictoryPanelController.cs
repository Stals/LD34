using UnityEngine;
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
            int randomIndex = UnityEngine.Random.Range (i, alpha.Count);
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

            // TODO setup buttons text

            ++i;            
        }
    }

    public void onPurchasePressed(GameObject cardHolder)
    {
        cardHolder.GetComponentInChildren<UIButton>().gameObject.SetActive(false);

        // TODO find this card and upgrade
    }
}
