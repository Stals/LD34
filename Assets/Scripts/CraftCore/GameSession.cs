﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CraftCore
{
    public class GameSession
    {
        public delegate void EndGame(float result);
        public event EndGame OnEndGame;

        public delegate void OutOfEnergy();
        public event OutOfEnergy onOutOfEnergy;

        List<Card> deck;
        Motherboard board;
        List<Card> waste = new List<Card>();
        List<Card> avaliableCards = new List<Card>();

        public GameSession(GameSession session)
        {
            deck = new List<Card>(session.deck);
            board = new Motherboard(session.board);
            waste = new List<Card>(session.waste);
            avaliableCards = new List<Card>(session.avaliableCards);
        }

        public GameSession(List<Card> deck, Motherboard board)
        {
            this.deck = new List<Card>(deck);
            this.board = board;

            ReadyMove();
        }

        public List<Card> AvaliableCards
        {
            get
            {
                return avaliableCards;
            }
        }

        internal Motherboard Board
        {
            get
            {
                return board;
            }
        }

        bool CheckEnergyEndGame()
        {
            return ((board.TotalHeat == 0) &&
                (avaliableCards[0].HeatPrice > 0) &&
                (avaliableCards[1].HeatPrice > 0));
        }

        bool CheckSlotsEndGame()
        {
            return (board.SlotsOnBoard.Count - board.CardsOnBoard.Count == 0);
        }

        void CallOutOfEnergy()
        {
            if (onOutOfEnergy != null) onOutOfEnergy();
            Debug.Log("OutOfEnergy");
        }

        void CallEndGame()
        {
            if (OnEndGame != null) OnEndGame(ResultScore());
            Debug.Log("EndGame");
        }

        public bool pickCard(Card card, int x, int y)
        {
            if (avaliableCards.Contains(card))
            {
                ErrorType error = board.addCard(card, x, y);
                if (error!= ErrorType.None)
                {
                    if (error == ErrorType.OutOfEnergy) {
                        CallOutOfEnergy();
                    }

                    Debug.Log("Not a valid card choice!");
                    return false;
                }
                avaliableCards.Remove(card);
                foreach (var c in avaliableCards) waste.Add(c);

                if (deck.Count < 2) CallEndGame();

                ReadyMove();

                if (CheckEnergyEndGame() || CheckSlotsEndGame()) CallEndGame();
                return true;
            }
            else
            {
                Debug.Log("Not a valid card!");
                return false;
            }
        }

        public void Discard()
        {
            if (Board.TotalHeat <= 0) return;
            foreach (var c in avaliableCards)
            {
                waste.Add(c);
            }
            Board.Heat--;
            ReadyMove();

            if (CheckEnergyEndGame() || CheckSlotsEndGame()) CallEndGame();
        }

        private void ReadyMove()
        {
            avaliableCards.Clear();
            avaliableCards.Add(deck[0]);
            avaliableCards.Add(deck[1]);
            deck.RemoveAt(0);
            deck.RemoveAt(0);
        }

        public float ResultScore()
        {
            float res = 0f;
            float[] vals = new float[3];
            vals[0] = board.Energy(EnergyType.Blue);
            vals[1] = board.Energy(EnergyType.Green);
            vals[2] = board.Energy(EnergyType.Red);
            foreach (var v in vals) res += v;
            res = res - (res / 3.0f - Mathf.Min(vals));

			foreach (var v in vals) {
				if(v <= 0) res -= 5f;
			}
			return res <= 0f ? 0f : res;
        }
    }
}
