using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CraftCore
{
    [Serializable]
    public class Motherboard
    {
        public const int xSize = 4;
        public const int ySize = 4;

        [JsonProperty("TyleMatrix")]
        EnergyType[,] tyleMatrix = new EnergyType[xSize, ySize];
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
            for (int i = 0; i < xSize; ++i)
                for (int j = 0; j < ySize; ++j)
                {
                    var card = GetCard(i, j);
                    if (card != null)
                    {
                        int buff = (card.Type == tyleMatrix[i, j]) ? 1 : 0;
                        card.ModifierValue = buff;
                    }
                }

            for (int i = 0; i < xSize; ++i)
                for (int j = 0; j < ySize; ++j)
                {
                    var card = GetCard(i, j);
                    if ((card != null) && (card.Modifier != null))
                    {
                        card.Modifier.Modify(this, i, j, card);
                    }
                }


        }
    }
}
