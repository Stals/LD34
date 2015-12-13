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
        [JsonProperty("BonusForLevel")]
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

    [JsonObject("AdjacentBonus")]
    public class AdjacentBonus : BonusContaining, IModifier
    {
        //public AdjacentBonus(int[] bonuses)
        //{
        //    bonusForLevel = bonuses;
        //}

        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return Motherboard.AdjacentCond(card.x, card.y);
        }
    }

    [JsonObject("ColorBonus")]
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

        //public ColorBonus(int[] bonuses, EnergyType color)
        //{
        //    bonusForLevel = bonuses;
        //}

        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return Motherboard.TypeCond(card.card.Type);
        }
    }

    [JsonObject("AdjacentColorBonus")]
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
