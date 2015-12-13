using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CraftCore
{
    public class DeckCombiner
    {
        public static DeckCombiner load()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;

            var file = Resources.Load("deck") as TextAsset;
            DeckCombiner result = JsonConvert.DeserializeObject<DeckCombiner>(file.text, settings);
            return result;
        }

        public struct CardDublicating
        {
            public CardDublicating(Card card, int doubles)
            {
                this.card = card;
                this.doubles = doubles;
            }

            public Card card;
            public int doubles;
        }

        List<CardDublicating> cards = new List<CardDublicating>();

        public List<CardDublicating> Cards
        {
            get
            {
                return cards;
            }
        }

        List<Card> Shuffle(List<Card> input)
        {
            var copy = new List<Card>(input);
            List<Card> result = new List<Card>();
            while (copy.Count > 0)
            {
                Card card = copy[UnityEngine.Random.Range(0, copy.Count)];
                copy.Remove(card);
                result.Add(card);
            }
            return result;
        }

        public List<Card> combineDeck()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;

            List<Card> result = new List<Card>();
            foreach (var d in Cards)
            {
                string serializedCard = JsonConvert.SerializeObject(d.card, Formatting.None, settings);
                for (int i = 0; i < d.doubles; ++i)
                {
                    result.Add(JsonConvert.DeserializeObject<Card>(serializedCard, settings));
                }
            }

            return Shuffle(result);     
        }
    }
}
