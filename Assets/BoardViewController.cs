using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
