using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CraftCore
{
    
    public abstract class AchievementBonus
    {
        abstract public void AddAchievement(Motherboard board);
    }


    public class AddToRandomType: AchievementBonus
    {
        public int val { get; set; }
        EnergyType type;
        public AddToRandomType(int valToAdd)
        {
            val = valToAdd;
            type = (EnergyType) UnityEngine.Random.Range(1, 4);
        }

        public override void AddAchievement(Motherboard board)
        {
            board.AchModifiers[type] += val;
        }
    }

    public class AddToAllTypes : AchievementBonus
    {
        public int val { get; set; }
        public AddToAllTypes(int valToAdd)
        {
            val = valToAdd;
        }

        public override void AddAchievement(Motherboard board)
        {
            board.AchModifiers[EnergyType.Red] += val;
            board.AchModifiers[EnergyType.Green] += val;
            board.AchModifiers[EnergyType.Blue] += val;
        }
    }

    public abstract class MotherboardAcheivment
    {
        public AchievementBonus bonus { get; set; }
        public string Descrition { get; set; }
        abstract public bool CheckCondition(Motherboard board);
        public void Calculate(Motherboard board)
        {
            if (CheckCondition(board))
            {
                bonus.AddAchievement(board);
            }
        }
    }

    public class ProperFilled : MotherboardAcheivment
    {
        public EnergyType aimcolor { get; set; }

        public ProperFilled(EnergyType color)
        {
            aimcolor = color;
        }

        public override bool CheckCondition(Motherboard board)
        {
            foreach (var tyle in board.SlotsOnBoard)
            {
                if (tyle.type != aimcolor) continue;
                var card = board.GetCard(tyle.x, tyle.y);
                if (card == null) return false;
                if (tyle.type != card.Type) return false;
            }
            return true;
        }
    }

    public class NumberFilled: MotherboardAcheivment
    {
        public EnergyType aimcolor { get; set; }
        public int number { get; set; }
        public NumberFilled(EnergyType color, int num)
        {
            aimcolor = color;
            number = num;
        }

        public override bool CheckCondition(Motherboard board)
        {
            return board.CardsByCondition(Motherboard.TypeCond(aimcolor)).Count == number;
        }
    }
}
