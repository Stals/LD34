using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;

public class BoardViewController : MonoBehaviour {

    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    UIGrid cardSelectionGrid;

	[SerializeField]
	List<UIGrid> slots;

    const int cardsPerDraft = 2;
	Motherboard board;
	GameSession session;

    List<GameObject> currentChoiceCards;

	EnergyType[,] getRandomMotherboardSetup()
	{
		EnergyType[,] arr =  {    	{EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
									{EnergyType.Blue,  EnergyType.Green, EnergyType.Black, EnergyType.Red},
									{EnergyType.Blue, EnergyType.Green, EnergyType.Black, EnergyType.Red},
									{EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};
		return arr;
	}

	List<Card> getDeck()
	{
		Card card1 = new Card();
		card1.Type = EnergyType.Black;
		card1.setLevelEnergy(0, 0, 0);
		
		Card card2 = new Card();
		card2.Type = EnergyType.Red;
		card2.setLevelEnergy(1, 1, 1);
		
		List<Card> cards = new List<Card>();
        cards.Add(card1);
        cards.Add(card2);

		return cards;
	}

	void setupBackend()
	{
		board = new Motherboard(getRandomMotherboardSetup());
		session = new GameSession (getDeck (), board);
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

				slots[x].GetChild(y).gameObject.GetComponent<SlotViewController>().setup(energyType);
			}
		}
	}

    void drawNewCards()
    {
		foreach(Card card in session.AvaliableCards){
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
