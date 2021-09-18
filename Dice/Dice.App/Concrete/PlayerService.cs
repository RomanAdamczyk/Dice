using Dice.App.Common;
using Dice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.App.Concrete
{
    public class PlayerService: Program<Player> 
    {
        
        public List<Player> AddNewPlayers(int count)
        {
            List<Player> players = new List<Player>();
            Player player;
            string name;
            for (int i=1; i<=count;i++ )
            {
                Console.WriteLine($"Podaj imię gracza numer {i}");
                name = Console.ReadLine();
                player = new Player(i, name);
                players.Add(player);
            }
            return players;

        }
    }
}
