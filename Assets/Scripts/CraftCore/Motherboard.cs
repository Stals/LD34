﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace CraftCore
{
    public struct CardOnBoard
    {
        public CardOnBoard(int x, int y, Card card)
        {
            this.x = x;
            this.y = y;
            this.card = card;
        }
        public int x;
        public int y;
        public Card card;
    }

    [Serializable]
    public class Motherboard
    {
        public const int xSize = 4;
        public const int ySize = 4;

        [JsonProperty("TyleMatrix")]
        EnergyType[,] tyleMatrix = new EnergyType[xSize, ySize];
        [JsonProperty("Heat")]
        public int Heat { get; set; }

        Card[,] cardMatrix = new Card[xSize, ySize];

        internal EnergyType[,] TyleMatrix
        {
            set
            {
                tyleMatrix = value;
            }
        }

        public Motherboard(EnergyType[,] matrix)
        {
            Array.Copy(matrix, tyleMatrix, tyleMatrix.GetLength(0) * tyleMatrix.GetLength(1));
        }

        public Motherboard(Motherboard board)
        {
            Array.Copy(board.tyleMatrix, tyleMatrix, board.tyleMatrix.GetLength(0) * board.tyleMatrix.GetLength(1));
            Array.Copy(board.cardMatrix, cardMatrix, board.cardMatrix.GetLength(0) * board.cardMatrix.GetLength(1));
        }

        public int Energy(EnergyType type)
        {
            int result = 0;
            foreach (var card in cardMatrix)
            {
                if (card == null) continue;
                if (card.Type == type)
                {
                    result += card.ProducedEnergy();
                }
            }
            return result;
        }

        public bool addCard(Card card, int x, int y)
        {
            if (tyleMatrix[x, y] == EnergyType.Empty) return false;
            if (Heat < card.HeatPrice) return false;

            Heat -= card.HeatPrice;
            cardMatrix[x, y] = card;
            RecalculateModifiers();
            return true;
        }

        public Card GetCard(int x, int y)
        {
            return cardMatrix[x, y];
        }

        public EnergyType GetTyle(int x, int y)
        {
            return tyleMatrix[x, y];
        }

        private void RecalculateModifiers()
        {
            foreach (var card in CardsOnBoard)
            {
                int buff = (card.card.Type == tyleMatrix[card.x, card.y]) ? 1 : 0;
                card.card.ModifierValue = buff;
            }

            foreach (var card in CardsOnBoard)
            {
                if (card.card.Modifier != null)
                {
                    card.card.Modifier.Modify(this, card);
                }
            }
        }

        public delegate bool CardAndPlaceCondition (CardOnBoard card);

        public List<CardOnBoard> CardsOnBoard
        {
            get
            {
                List<CardOnBoard> result = new List<CardOnBoard>();
                for (int i = 0; i < cardMatrix.GetLength(0); ++i)
                    for (int j = 0; j < cardMatrix.GetLength(1); ++j)
                    {
                        var card = GetCard(i, j);
                        if (card != null)
                        {
                            result.Add(new CardOnBoard(i, j, card));
                        }
                    }
                return result;
            }
        }

        public List<Card> CardsByCondition(CardAndPlaceCondition condition)
        {
            List<Card> result = new List<Card>();
            foreach (var card in CardsOnBoard)
            {
                if (condition(card))
                {
                    result.Add(card.card);
                }
            }

            return result;
        }

        public static CardAndPlaceCondition TypeCond(EnergyType type)
        {
            return (CardOnBoard card) =>
            {
                return (card.card.Type == type);
            };
        }
        
        public static CardAndPlaceCondition AdjacentCond(int myX, int myY)
        {
            return (CardOnBoard card) =>
            {
                return ( (Mathf.Abs(card.x - myX) + Mathf.Abs(card.y - myY) ) == 1);
            };
        }
    }
}
