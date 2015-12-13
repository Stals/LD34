using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using CraftCore;
using System.Collections.Generic;

public class TestingSaver : MonoBehaviour
{

    [SerializeField]
    int x;
    [SerializeField]
    int y;

    [SerializeField]
    bool pickFirst;
    [SerializeField]
    bool pickSecond;

    GameSession game;

    // Use this for initialization
    void Start()
    {
        List<Card> cards = new List<Card>();

        Card card = new Card();
        card.Type = EnergyType.Black;
        card.setLevelEnergy(1, 2, 3);
        cards.Add(card);

        for (int i = 0; i < 10; ++i)
        {
            card = new Card();
            card.Type = EnergyType.Blue;
            card.setLevelEnergy(1, 2, 3);
            cards.Add(card);
            card = new Card();
            card.Type = EnergyType.Green;
            card.setLevelEnergy(1, 2, 3);
            cards.Add(card);
            card = new Card();
            card.Type = EnergyType.Red;
            card.setLevelEnergy(1, 2, 3);
            cards.Add(card);
        }


        EnergyType[,] arr = {   {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                {EnergyType.Blue, EnergyType.Green, EnergyType.Black, EnergyType.Red},
                                {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};

        Motherboard b = new Motherboard(arr);

        game = new GameSession(cards, b);
    }

    // Update is called once per frame
    void Update()
    {

        if (pickFirst || pickSecond)
        {
            if (pickFirst) game.pickCard(game.AvaliableCards[0], x, y);
            if (pickSecond) game.pickCard(game.AvaliableCards[1], x, y);

            string json;

            json = JsonConvert.SerializeObject(game.Board);
            Debug.Log(json);

            json = JsonConvert.SerializeObject(game.AvaliableCards[0]);
            Debug.Log(json);
            json = JsonConvert.SerializeObject(game.AvaliableCards[1]);
            Debug.Log(json);

            Debug.Log("Red energy = " + game.Board.Energy(EnergyType.Red));
            Debug.Log("Green energy = " + game.Board.Energy(EnergyType.Green));
            Debug.Log("Blue energy = " + game.Board.Energy(EnergyType.Blue));

            pickFirst = false;
            pickSecond = false;       
        }
    }
}
