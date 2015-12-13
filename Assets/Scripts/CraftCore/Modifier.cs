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
        string Description(Card card);
    }

    public abstract class BonusContaining: IModifier
    {
        [JsonProperty("BonusForLevel")]
        protected int[] bonusForLevel = new int[3];

        [JsonProperty("description")]
        public string BaseDescription;

        public void setLevelBonus(int x, int y, int z)
        {
            bonusForLevel[0] = x;
            bonusForLevel[1] = y;
            bonusForLevel[2] = z;
        }
        
        virtual public string Description(Card card)
        {
            return "+" + bonusForLevel[card.UpgradeLevel] + " " + BaseDescription;
        }

        public abstract void Modify(Motherboard board, CardOnBoard card);        
    }

    public abstract class OtherChanging: BonusContaining
    {
        public override void Modify(Motherboard board, CardOnBoard card)
        {
            foreach (var adjCard in board.CardsByCondition(Condition(card)))
            {
                adjCard.ModifierValue += bonusForLevel[card.card.UpgradeLevel];
            }
        }

        abstract protected Motherboard.CardAndPlaceCondition Condition(CardOnBoard card);
    }
    
    public class AdjacentBonus : OtherChanging
    {
        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return Motherboard.AdjacentCond(card.x, card.y);
        }
    }
    
    public class ColorBonus: OtherChanging
    {
        [JsonProperty("AimColor")]
        public EnergyType Color { get; set; }

        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return Motherboard.TypeCond(card.card.Type);
        }
    }
    
    public class AdjacentColorBonus : ColorBonus
    {
        protected override Motherboard.CardAndPlaceCondition Condition(CardOnBoard card)
        {
            return (CardOnBoard cardToChoose) =>
            {
                return Motherboard.AdjacentCond(card.x, card.y)(cardToChoose) &&
                       Motherboard.TypeCond(Color)(cardToChoose);
            };
        }
    }

    public class ColorNumberEquals: BonusContaining
    {
        [JsonProperty("AimColor")]
        public EnergyType Color { get; set; }

        [JsonProperty("AimNumber")]
        public int AimNumber { get; set; }

        public override void Modify(Motherboard board, CardOnBoard card)
        {
            int size = board.CardsByCondition(Motherboard.TypeCond(Color)).Count;
            if (size == AimNumber)
            {
                card.card.ModifierValue += bonusForLevel[card.card.UpgradeLevel];
            }
        }
    }

    public class InMySlot : BonusContaining
    {

        public override void Modify(Motherboard board, CardOnBoard card)
        {            
            if (board.GetTyle(card.x, card.y) == card.card.Type)
            { 
                card.card.ModifierValue += bonusForLevel[card.card.UpgradeLevel];
            }
        }
    }

    public class ForEachOfType : BonusContaining
    {
        [JsonProperty("AimColor")]
        public EnergyType Color { get; set; }
        
        public override void Modify(Motherboard board, CardOnBoard card)
        {
            int size = board.CardsByCondition(Motherboard.TypeCond(Color)).Count;
            card.card.ModifierValue += bonusForLevel[card.card.UpgradeLevel] * size;
        }
    }


}
