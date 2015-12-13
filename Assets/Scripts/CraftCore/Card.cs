using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CraftCore
{
    [Serializable]
    public enum EnergyType
    {
        Empty,
        Black,
        Red,
        Green,
        Blue
    }

    [Serializable]
    public class Card
    {
        public Card()
        {
            HeatPrice = 1;
        }

        int upgradeLevel = 0;
        public int ModifierValue { get; set; }

        [JsonProperty("LevelsInfo")]
        public EnergyType Type { get; set; }
        
        [JsonProperty("Modifier")]
        public IModifier Modifier { get; set; }

        [JsonProperty("HeatPrice")]
        public int HeatPrice { get; set; }

        [JsonProperty("OutputForLevel")]
        int[] outputForLevel = new int[3];

        public int UpgradeLevel
        {
            get
            {
                return upgradeLevel;
            }

            set
            {
                upgradeLevel = value;
            }
        }

        public void setLevelEnergy(int x, int y, int z)
        {
            outputForLevel[0] = x;
            outputForLevel[1] = y;
            outputForLevel[2] = z;
        }

        internal int ProducedEnergy()
        {
            return outputForLevel[upgradeLevel] + ModifierValue;
        }

        string Description
        {
            get
            {
                return "Gives " + ProducedEnergy().ToString() + " " + Utils.NameFromType(Type);
            }
        }
    }
}
