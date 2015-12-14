using System;
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

    public struct BoardSlot
    {
        public BoardSlot(int x, int y, EnergyType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
        public int x;
        public int y;
        public EnergyType type;
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
        public int HeatModifier { get; set; }
        public MotherboardAcheivment Achievment { get; set; }

        Dictionary<EnergyType, int> modifiers = new Dictionary<EnergyType, int>();
        Dictionary<EnergyType, int> achModifiers = new Dictionary<EnergyType, int>();

        public int TotalHeat
        {
            get { return Heat + HeatModifier; }
        }

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
            ZeroAchModifiers();
        }

        private void ZeroAchModifiers()
        {
            achModifiers[EnergyType.Black] = 0;
            achModifiers[EnergyType.Red] = 0;
            achModifiers[EnergyType.Black] = 0;
        }

        public Motherboard(Motherboard board)
        {
            Array.Copy(board.tyleMatrix, tyleMatrix, board.tyleMatrix.GetLength(0) * board.tyleMatrix.GetLength(1));
            Array.Copy(board.cardMatrix, cardMatrix, board.cardMatrix.GetLength(0) * board.cardMatrix.GetLength(1));
            Heat = board.Heat;
            HeatModifier = board.HeatModifier;
        }

        public int Energy(EnergyType type)
        {
            int result = 0;
            if (Modifiers.ContainsKey(type)) result += Modifiers[type];
            result += AchModifiers[type];
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
            if (tyleMatrix[x, y] == EnergyType.Empty)
            {
                Debug.Log("not a valid place!");
                return false;
            }
            if (TotalHeat < card.HeatPrice)
            {
                Debug.Log("not enought energy!");
                return false;
            }

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
            HeatModifier = 0;
            ZeroAchModifiers();
            foreach (var card in CardsOnBoard)
            {
                int buff = (card.card.Type == tyleMatrix[card.x, card.y]) ? 1 : 0;
                card.card.ModifierValue = buff;
                HeatModifier -= card.card.HeatPrice;
            }

            foreach (var card in CardsOnBoard)
            {
                if (card.card.Modifier != null)
                {
                    card.card.Modifier.Modify(this, card);
                }
            }

            Achievment.Calculate(this);
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

        public List<BoardSlot> SlotsOnBoard
        {
            get
            {
                List<BoardSlot> result = new List<BoardSlot>();
                for (int i = 0; i < cardMatrix.GetLength(0); ++i)
                    for (int j = 0; j < cardMatrix.GetLength(1); ++j)
                    {
                        var slot = GetTyle(i, j);
                        if (slot != EnergyType.Empty)
                        {
                            result.Add(new BoardSlot(i, j, slot));
                        }
                    }
                return result;
            }
        }

        public Dictionary<EnergyType, int> Modifiers
        {
            get
            {
                return modifiers;
            }

            set
            {
                modifiers = value;
            }
        }

        public Dictionary<EnergyType, int> AchModifiers
        {
            get
            {
                return achModifiers;
            }

            set
            {
                achModifiers = value;
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
