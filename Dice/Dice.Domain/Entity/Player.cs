using Dice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Domain.Entity
{
    public class Player:BaseEntity
    {
        public string Name { get; set; }
        public Dictionary<string, int> Values { get; set; }
        public Dictionary<string, bool> FreeValues { get; set; }

        public Player(int id, string name)
        {
            Name = name;
            Id = id;
            FreeValues = new Dictionary<string, bool>();
            FreeValues.Add("Ones", false);
            FreeValues.Add("Twos", false);
            FreeValues.Add("Threes", false);
            FreeValues.Add("Fours", false);
            FreeValues.Add("Fives", false);
            FreeValues.Add("Sixs", false);
            FreeValues.Add("Bonus", true);
            FreeValues.Add("Triple", false);
            FreeValues.Add("Fourfold", false);
            FreeValues.Add("Full", false);
            FreeValues.Add("SmallStraight", false);
            FreeValues.Add("HighStraight", false);
            FreeValues.Add("General", false);
            FreeValues.Add("Chance", false);
            FreeValues.Add("Total", true);

            Values = new Dictionary<string, int>();
            Values.Add("Ones", 0);
            Values.Add("Twos", 0);
            Values.Add("Threes", 0);
            Values.Add("Fours", 0);
            Values.Add("Fives", 0);
            Values.Add("Sixs", 0);
            Values.Add("Bonus", 0);
            Values.Add("Triple", 0);
            Values.Add("Fourfold", 0);
            Values.Add("Full", 0);
            Values.Add("SmallStraight", 0);
            Values.Add("HighStraight", 0);
            Values.Add("General", 0);
            Values.Add("Chance", 0);
            Values.Add("Total", 0);
        }
    }
}
