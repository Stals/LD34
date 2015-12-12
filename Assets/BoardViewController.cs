using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;

public class BoardViewController : MonoBehaviour {

    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    UIGrid cardSelectionGrid;

    const int cardsPerDraft = 2;

    List<GameObject> currentChoiceCards;

    // Use this for initialization
    void Start() {
        Game.Instance.getManager().setBoardViewController(this);


        Card card1 = new Card();
        card1.Type = EnergyType.Black;
        card1.setLevelEnergy(1, 2, 3);


        Card card2 = new Card();
        card2.Type = EnergyType.Black;
        card2.setLevelEnergy(1, 2, 3);

        EnergyType[,] arr = {   {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                {EnergyType.Blue,  EnergyType.Green, EnergyType.Black, EnergyType.Red},
                                {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};
        Motherboard board = new Motherboard(arr);




        List<Card> cards = new List<Card>();
        cards.Add(card1);
        cards.Add(card2);

        GameSession session = new GameSession(cards, board);


        currentChoiceCards = new List<GameObject>();
        drawNewCards();
    }

    // Update is called once per frame
    void Update() {

    }

    void drawNewCards()
    {
        for (int i = 0; i < cardsPerDraft; ++i)
        {
            currentChoiceCards.Add(NGUITools.AddChild(cardSelectionGrid.gameObject, cardPrefab));
        }
        cardSelectionGrid.Reposition();
    }

    public void onCardPress(CardViewController cardController) {
        // TODO call all other cards back
        foreach (var card in currentChoiceCards)
        {
            if (card != cardController.gameObject)
            {
                callCardBack(card);
            }
        }
    }

    // returns the card onto its dealing place
    public void callCardBack(GameObject cardObject) {
        cardObject.transform.parent = cardSelectionGrid.transform;

        NGUITools.MarkParentAsChanged(cardObject);
        cardSelectionGrid.Reposition();
    }
}
