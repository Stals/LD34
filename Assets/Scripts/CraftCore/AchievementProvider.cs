using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace CraftCore
{
    class AchievementProvider
    {

        public static AchievementProvider load()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;

            var file = Resources.Load("achievs") as TextAsset;
            var result = JsonConvert.DeserializeObject<AchievementProvider>(file.text, settings);
            return result;
        }

        [JsonProperty("Bonuses")]
        public List<MotherboardAcheivment> allBonuses = new List<MotherboardAcheivment>();

        public AchievementProvider()
        {
            //var allUti = new ProperFilled(EnergyType.Black);
            //allUti.bonus = new AddToAllTypes(1);
            //allUti.Descrition = " + 1 TO ALL if all UTI filled with UTI";
            //allBonuses.Add(allUti);

            //var numRam = new NumberFilled(EnergyType.Green, 1);
            //numRam.bonus = new AddToRandomType(3);
            //numRam.Descrition = " + 1 RND STAT if only one RAM on board";
            //allBonuses.Add(numRam);

        }
    }
}
