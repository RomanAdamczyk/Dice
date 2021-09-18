using Dice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.Domain.Entity
{
    public class Game: BaseEntity
    {
        public int[,] Dices { get; set; }
        public Dictionary<string, int> Values { get; set; }
        public int [] ValuesDices { get; set; }
        public int Max { get; set; }
        public Game (List<Player> players)
        {
            Dices = new int[5,2];
            ValuesDices = new int[6];
        }

    }
}
