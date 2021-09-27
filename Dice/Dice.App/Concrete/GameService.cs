using Dice.App.Abstract;
using Dice.App.Common;
using Dice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.App.Concrete
{
    public class GameService: IGameService<Program<Game>>
    //public class GameService : Program<Game>
    {
        public List<Player> NewGame()
        {
            PlayerService playerServive = new PlayerService();
            Console.WriteLine("Podaj liczbę graczy");
            string operation = Console.ReadLine();
            int count = Int32.Parse(operation);
            List<Player> players;
            players = playerServive.AddNewPlayers(count);
            return players;
        }
        public void Game(List<Player> players)
        {
            Game game = new Game(players);
            Random generator = new Random();
            int round = 0;
            while (round < 13)
            {
                int playerId = 0;
                while (playerId < players.Count)
                {
                    PlayerRound(players, game, generator, playerId);
                    playerId++;
                }
                round++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("TABELA KOŃCOWA");
            VieWTable(players);          
        }
        public void Draw(Game game, Random generator)
        {
            for (int i = 0; i < 5; i++)
            {
                if (game.Dices[i, 1] == 0)
                {
                    game.Dices[i, 0] = generator.Next(1,7);
                    
                }
                else
                {
                    game.Dices[i, 1] = 0;
                }
            }
        }
        public void PlayerTurn(Game game, Random generator)
        {
            Array.Clear(game.Dices,0,5);
            Draw(game, generator);
            for (int i = 0; i < 2; i++)
            {
                ViewDices(game);
                Console.WriteLine();
                Console.WriteLine("Ile kości chcesz zachować?");
                var operation = Int32.Parse(Console.ReadLine());
                DiceLeave(game, operation);
                Draw(game, generator);
            }
            ViewDices(game);
        }
        public void PlayerRound(List<Player> players, Game game, Random generator, int playerId)
        {
            VieWTable(players);
            Console.WriteLine($"ruch gracza {players[playerId].Name}:");
            PlayerTurn(game, generator);
            Check(players[playerId], game);
            Console.WriteLine("Wybierz pozycję którą chcesz uzupełnić");
            var choosenValues  = ViewChoose(game);
            var operation = Int32.Parse(Console.ReadLine());
            players[playerId].Values[choosenValues[operation]] = game.Values[choosenValues[operation]];
            players[playerId].FreeValues[choosenValues[operation]] = true;
        }
        public void ViewDices(Game game)
        {
            for (int k = 0; k < 5; k++)
            {
                Console.Write($"[ {game.Dices[k, 0]} ] ( {k + 1} )    ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        public void VieWTable(List<Player> players)
        {
            Console.WriteLine();
            Console.Write("                ");
            CountTotal(players);
            for (int i = 0; i < players.Count; i++)
            {
                Console.Write("|"+players[i].Name.PadRight(15,' '));
            }
            Console.WriteLine("|");
            foreach (string name in players[0].Values.Keys)
            {
                ViewTableLine(players, name);
            }
            Console.WriteLine();         
        }
        public void ViewTableLine(List<Player> players, string name)
        {
            Console.Write("|" + name.PadRight(15,' '));
            foreach (Player player in players)
            {
                if (player.FreeValues[name])
                {
                    Console.Write($"|       {player.Values[name]}".PadRight(16, ' '));
                }
                else
                {
                    Console.Write($"|       -       ");
                }

            }
            Console.WriteLine("|");
        }
        public Dictionary<int, string> ViewChoose(Game game)
        {
            int counter = 0;
            Dictionary<int, string> choosenValues = new Dictionary<int, string>();
            Console.WriteLine();
            foreach (var key in game.Values.Keys )
            {
                Console.WriteLine($"|{counter}".PadRight(3,' ') + $"| {key}".PadRight(16, ' ') + $"| {game.Values[key]}".PadRight(4, ' ') + "|");
                choosenValues.Add(counter, key);
                counter++;
            }
            return choosenValues;
        }
        public void Check(Player player, Game game)
        {
            game.Values = new Dictionary<string, int>();
            game.ValuesDices = new int[6];
            SetValuesDices(game);
            FindMax(game);
            int schoolCount = 1;
            foreach (string schoolNumber in player.Values.Keys)
            {
                if (schoolCount > 6)
                {
                    break;
                }
                if (player.FreeValues[schoolNumber] == false)
                {
                    game.Values[schoolNumber] = CheckSchool(game, schoolCount);
                }
                schoolCount++;
            }
            if (player.FreeValues["Triple"] == false)  // Tutaj nie tworzę pętli, bo dla dwóch elementów byłaby to sztuka dla sztuki
            {
                game.Values["Triple"] = CheckNumber(game,3);
            }
            if (player.FreeValues["Fourfold"] == false)
            {
                game.Values["Fourfold"] = CheckNumber(game,4);
            }
            if (player.FreeValues["General"] == false)
            {
                game.Values["General"] = CheckGeneral(game);
            }
            else 
            {
                CheckGeneral(game, player);
                
            }
            if (player.FreeValues["Full"] == false)
            {
                game.Values["Full"] = CheckFull(game);
            }
            if (player.FreeValues["SmallStraight"] == false)
            {
                game.Values["SmallStraight"] = CheckStraight(game,4);
            }
            if (player.FreeValues["HighStraight"] == false)
            {
                game.Values["HighStraight"] = CheckStraight(game,5);
            }
            if (player.FreeValues["Chance"] == false)
            {
                game.Values["Chance"] = SumValues(game);
            }
        }
        public void SetValuesDices (Game game)
        {
            for (int i = 0; i < 5; i++)
            {
                game.ValuesDices[game.Dices[i, 0] - 1]++;
            }
        }        
        public int CheckSchool(Game game, int value)
        {
            return game.ValuesDices[value - 1] * value;
        }
        public int CheckNumber(Game game, int value)
        {
            if (game.Max >= value)
            {
                return SumValues(game);
            }
            else
            {
                return 0;
            }
        }
        public int CheckFull(Game game)
        {
            if (game.Max == 3 & Array.IndexOf(game.ValuesDices,2)>=0)
            {
                return 25;
            }
            else
            {
                return 0;
            }
        }
        public int CheckStraight(Game game, int value)
        {
            int diceNumber = 1;
            while (diceNumber + value <= 7)
            {
                for (int i = diceNumber - 1; i < diceNumber - 1 + value; i++)
                {
                    if (game.ValuesDices[i] == 0)
                    {
                        break;
                    }
                    if (i == diceNumber + value - 2)
                    {
                        return (value - 1) * 10;
                    }
                }
                diceNumber++;
            }
            return 0;
        }
        public int CheckGeneral(Game game)
        {
            if (game.Max == 5)
            {
                return 50;
            }
            return 0;
        }
        public void CheckGeneral(Game game, Player player)
        {
            if (game.Max == 5 & player.FreeValues["Ones"] & player.FreeValues["Twos"] & player.FreeValues["Threes"] & player.FreeValues["Fours"] & player.FreeValues["Fives"] & player.FreeValues["Sixs"])
            {
                if (player.FreeValues["Full"] == false)
                {
                    game.Values["Full"] = 25;
                }
                if (player.FreeValues["SmallStraight"] == false)
                {
                    game.Values["SmallStraight"] = 30;
                }
                if (player.FreeValues["HighStraight"] == false)
                {
                    game.Values["HighStraight"] = 40;
                }
                if (player.Values["General"] > 0)
                {
                    player.Values["General"] += 100;
                }
            }
        }
        public int SumValues(Game game)
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                sum += game.Dices[i, 0];
            }
            return sum;
        }
        public void FindMax (Game game)
        {
            int max = 0;
            foreach (int i in game.ValuesDices)
            {
                if (i > max)
                {
                    max = i;
                }
            }
            game.Max = max;
        }
        public void CountTotal (List<Player> players)
        {
            foreach (Player player in players)
            {
                if (player.Values["Ones"] + player.Values["Twos"] + player.Values["Threes"] + player.Values["Fours"] + player.Values["Fives"] + player.Values["Sixs"] >= 63 )
                {
                    player.Values["Bonus"] = 35;                 
                }
                player.Values["Total"] = player.Values["Ones"] + player.Values["Twos"] + player.Values["Threes"] + player.Values["Fours"] + player.Values["Fives"] + player.Values["Sixs"] + player.Values["Bonus"] + player.Values["Triple"] + player.Values["Fourfold"] + player.Values["Full"] + player.Values["SmallStraight"] + player.Values["HighStraight"] + player.Values["General"] + player.Values["Chance"];
            }
        }
        public void DiceLeave (Game game, int count)
        {
            for (int j = 0; j < count; j++)
            {
                Console.WriteLine("Którą kość chcesz zostawić?");
                var operation = Int32.Parse(Console.ReadLine());
                int number = operation;
                game.Dices[number - 1, 1] = 1;
            }

        }

    }
}
