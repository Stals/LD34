using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;
using System;

public class BoardViewController : MonoBehaviour {

    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    UIGrid cardSelectionGrid;

    [SerializeField]
    List<UIGrid> slots;

    [SerializeField]
    VictoryPanelController victoryPanel;

    [SerializeField]
    GameObject cardsSpawn;

    [SerializeField]
    UIGrid cardDiscardPlace;

	[SerializeField]
	UILabel achivDescritpion;

    const int cardsPerDraft = 2;
    Motherboard board;
    public GameSession session;

    List<GameObject> currentChoiceCards;

    List<Card> getDeck()
    {
        return Game.Instance.getDeckCombiner().combineDeck();
    }

    public bool onCardPlaced(int x, int y, Card card, CardViewController cardController)
    {
        bool success = session.pickCard(card, x, y);

        if (success)
        {
            // TODO place remaining card into discard pile
            foreach (GameObject cardGO in currentChoiceCards)
            {
                if (cardGO.GetComponent<CardViewController>().getCard() != card)
                {
                    moveCardToDiscard(cardGO);
                }
            }

            drawNewCards();
        }
        else {
            callCardBack(cardController.gameObject);
        }

        return success;
    }

    public void setup(Motherboard motherboard)
    {
        board = motherboard;
        session = new GameSession(getDeck(), motherboard);

        setupBoard();
        applyResearches();

        currentChoiceCards = new List<GameObject>();
        drawNewCards();

		if (board.Achievment != null) {
			achivDescritpion.text = Utils.getColorDescription (board.Achievment.Descrition);
		}
    }

    private void applyResearches()
    {
        board.Modifiers.Clear();

        foreach (var upg in Game.Instance.getPlayer().getStatUpgrades()) {
            if (upg.isEnabled) {
                if (upg.energyType == EnergyType.Black)
                {
                    board.Heat += upg.bonus;
                }
                else if (upg.energyType == EnergyType.Empty)
                {
                    continue;
                }
                else {
                    if (board.Modifiers.ContainsKey(upg.energyType))
                    {
                        board.Modifiers[upg.energyType] += upg.bonus;
                    }
                    else {
                        board.Modifiers[upg.energyType] = upg.bonus;
                    }
                }
            }
        }
    }

    private void moveCardToDiscard(GameObject cardGO)
    {
        // Todo implamant animation and some discard pile

        moveCardToGrid(cardDiscardPlace, cardGO);

        //cardSelectionGrid.Reposition();

        //NGUITools.Destroy(cardGO);
    }

    void setupBackend()
    {
        //board = new Motherboard(getRandomMotherboardSetup());
        //session = new GameSession (getDeck (), board);
    }

    // Use this for initialization
    void Start() {
        Game.Instance.getManager().setBoardViewController(this);
    }

    // Update is called once per frame
    void Update() {

    }

    void setupBoard()
    {
        for (int x = 0; x < Motherboard.xSize; ++x) {
            for (int y = 0; y < Motherboard.ySize; ++y) {
                EnergyType energyType = board.GetTyle(x, y);

                slots[x].GetChild(y).gameObject.GetComponent<SlotViewController>().setup(energyType, x, y);
            }
        }
    }

    void drawNewCards()
    {
        currentChoiceCards.Clear();

        foreach (Card card in session.AvaliableCards) {
            GameObject cardGO = NGUITools.AddChild(cardsSpawn, cardPrefab);
            currentChoiceCards.Add(cardGO);

            cardGO.GetComponent<CardViewController>().setup(card);

            callCardBack(cardGO);
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

    public void moveCardToGrid(UIGrid grid, GameObject cardObject)
    {
        cardObject.transform.parent = grid.transform;

        NGUITools.MarkParentAsChanged(cardObject);
        grid.Reposition();
    }


    // returns the card onto its dealing place
    public void callCardBack(GameObject cardObject) {
        moveCardToGrid(cardSelectionGrid, cardObject);
    }

    public void onShipPressed()
    {
        // discard your last cards (for next session)
        /*foreach (GameObject cardGO in currentChoiceCards)
        {
            moveCardToDiscard(cardGO);
        }*/

        // TODO switch to victory screen and cards choosign 
        // + ability to select cards

        victoryPanel = transform.parent.gameObject.GetComponentInChildren<VictoryPanelController>();
        victoryPanel.GetComponent<UITweener>().PlayForward();
        GetComponent<UITweener>().PlayReverse();

        victoryPanel.setup(session);

        Destroy(this.gameObject, 2f);
    }

    public void onReDrawPressed()
    {
        session.Discard();

        foreach (GameObject cardGO in currentChoiceCards)
        {
            moveCardToDiscard(cardGO);    
        }

        drawNewCards();
    }
}
