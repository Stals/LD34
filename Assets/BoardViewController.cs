﻿using UnityEngine;
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

    const int cardsPerDraft = 2;
	Motherboard board;
	public GameSession session;

    List<GameObject> currentChoiceCards;

	List<Card> getDeck()
	{
        List<Card> cards = new List<Card>();

        {
            Card card = new Card();
            card.Type = EnergyType.Black;
            card.setLevelEnergy(0, 0, 0);

            cards.Add(card);
        }

        {
            Card card = new Card();
            card.Type = EnergyType.Red;
            card.setLevelEnergy(0, 0, 0);

            cards.Add(card);
        }

        {
            Card card = new Card();
            card.Type = EnergyType.Blue;
            card.setLevelEnergy(0, 0, 0);

            cards.Add(card);
        }

        {
            Card card = new Card();
            card.Type = EnergyType.Green;
            card.setLevelEnergy(0, 0, 0);

            cards.Add(card);
        }

        {
            Card card = new Card();
            card.Type = EnergyType.Black;
            card.setLevelEnergy(0, 0, 0);

            cards.Add(card);
        }

        {
            Card card = new Card();
            card.Type = EnergyType.Red;
            card.setLevelEnergy(0, 0, 0);

            cards.Add(card);
        }

        // TODO shuffle

        return cards;
	}

    public void onCardPlaced(int x, int y, Card card)
    {
        bool success = session.pickCard(card, x, y);

        if (success)
        {
            // TODO place remaining card into discard pile

            foreach (GameObject cardGO in currentChoiceCards) {
                if (cardGO.GetComponent<CardViewController>().getCard() != card) {
                    moveCardToDiscard(cardGO);
                }
            }

            drawNewCards();
        }
    }

    private void moveCardToDiscard(GameObject cardGO)
    {
        // Todo implamant animation and some discard pile
        NGUITools.DestroyImmediate(cardGO);

        cardSelectionGrid.Reposition();
    }

    void setupBackend()
	{
		//board = new Motherboard(getRandomMotherboardSetup());
		//session = new GameSession (getDeck (), board);
	}

    // Use this for initialization
    void Start() {
        Game.Instance.getManager().setBoardViewController(this);
		setupBackend ();

		setupBoard ();

        currentChoiceCards = new List<GameObject>();
        drawNewCards();
    }

    // Update is called once per frame
    void Update() {

    }

	void setupBoard()
	{
		for(int x = 0; x < Motherboard.xSize; ++x){
			for(int y = 0; y < Motherboard.ySize; ++y){
				EnergyType energyType = board.GetTyle(x, y);

				slots[x].GetChild(y).gameObject.GetComponent<SlotViewController>().setup(energyType, x, y);
			}
		}
	}

    void drawNewCards()
    {
        currentChoiceCards.Clear();

        foreach (Card card in session.AvaliableCards){
			GameObject cardGO = NGUITools.AddChild(cardSelectionGrid.gameObject, cardPrefab);
			currentChoiceCards.Add(cardGO);

			cardGO.GetComponent<CardViewController>().setup(card);
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
