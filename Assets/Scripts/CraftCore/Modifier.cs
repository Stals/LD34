using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftCore
{
    public interface IModifier
    {
        void Modify(Motherboard board, CardOnBoard card);
    }

    public abstract class BonusContaining
    {
        [JsonProperty("Bonus")]
        protected int[] bonusForLevel = new int[3];

        public void setLevelBonus(int x, int y, int z)
        {
            bonusForLevel[0] = x;
            bonusForLevel[1] = y;
            bonusForLevel[2] = z;
        }

        public void Modify(Motherboard board, CardOnBoard card)
        {
            foreach (var adjCard in board.CardsByCondition(Condition(card)))
            {
                adjCard.ModifierValue += bonusForLevel[card.card.UpgradeLevel];
            }
        }

        abstract protected Motherboard.CardAndPlaceCondition Condition(CardOnBoard card);
    }

    public class AdjacentBonus : BonusContaining, IModifier
    {
        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return Motherboard.AdjacentCond(card.x, card.y);
        }
    }

    public class ColorBonus: BonusContaining, IModifier
    {
        [JsonProperty("AimColor")]
        EnergyType color;

        public EnergyType Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return Motherboard.TypeCond(card.card.Type);
        }
    }

    public class AdjacentColorBonus : BonusContaining, IModifier
    {
        [JsonProperty("AimColor")]
        EnergyType color;

        public EnergyType Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return (CardOnBoard cardToChoose) =>
            {
                return Motherboard.AdjacentCond(card.x, card.y)(cardToChoose) &&
                       Motherboard.TypeCond(color)(cardToChoose);
            };
        }
    }

}
