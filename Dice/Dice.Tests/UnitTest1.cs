using Dice.App.Abstract;
using Dice.App.Concrete;
using Dice.Domain.Entity;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dice.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestSumValue()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Dices[0, 0] = 1;
            game.Dices[1, 0] = 1;
            game.Dices[2, 0] = 2;
            game.Dices[3, 0] = 1;
            game.Dices[4, 0] = 3;

            var gameService = new GameService();
            int returnValue = gameService.SumValues(game);

            Assert.Equal(8,returnValue);
        }
        [Fact]
        public void TestMaxValue()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 3;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 0;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();

            gameService.FindMax(game);

            Assert.Equal(3,game.Max);
        }
        [Fact]
        public void TestDraw()
        {
            Random generator = new Random();
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Dices[2, 1] = 1;
            game.Dices[3, 1] = 1;
            var gameService = new GameService();

            gameService.Draw(game, generator);

            Assert.NotEqual(0, game.Dices[0, 0]);
            Assert.NotEqual(0, game.Dices[1, 0]);
            Assert.NotEqual(0, game.Dices[4, 0]);
            Assert.Equal(0, game.Dices[2, 0]);
            Assert.Equal(0, game.Dices[3, 0]);
        }
        [Fact]
        public void TestSetValueDices()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Dices[0, 0] = 1;
            game.Dices[1, 0] = 1;
            game.Dices[2, 0] = 2;
            game.Dices[3, 0] = 1;
            game.Dices[4, 0] = 3;

            int[] valueDices = { 3, 1, 1, 0, 0, 0 };

            var gameService = new GameService();
            gameService.SetValuesDices(game);

            Assert.Equal(valueDices, game.ValuesDices);
        }
        [Fact]
        public void TestCheckSchool()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 3;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();
            
            Assert.Equal(0, gameService.CheckSchool(game, 1));
            Assert.Equal(2, gameService.CheckSchool(game, 2));
            Assert.Equal(3, gameService.CheckSchool(game, 3));
            Assert.Equal(12, gameService.CheckSchool(game, 4));
            Assert.Equal(0, gameService.CheckSchool(game, 5));
            Assert.Equal(0, gameService.CheckSchool(game, 6));
        }
        [Fact]
        public void TestCheckNumberOk()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Max = 3;

            //var mock = new Mock<IGameService<GameService>>();
            var mock = new Mock<GameService>();
            mock.Setup(s => s.SumValues(game)).Returns(8);

            Assert.Equal(8, mock.Object.CheckNumber(game,3));
        }
        [Fact]
        public void TestCheckFullOk()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Max = 3;
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 0;
            game.ValuesDices[2] = 2;
            game.ValuesDices[3] = 3;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();
            Assert.Equal(25, gameService.CheckFull(game));
        }
        [Fact]
        public void TestCheckFullFailWithoutThree()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Max = 2;
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 0;
            game.ValuesDices[2] = 2;
            game.ValuesDices[3] = 2;
            game.ValuesDices[4] = 1;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();
            Assert.Equal(0, gameService.CheckFull(game));
        }
        [Fact]
        public void TestCheckFullFailWithoutTwo()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Max = 3;
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 0;
            game.ValuesDices[2] = 3;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 1;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();
            Assert.Equal(0, gameService.CheckFull(game));
        }
        [Fact]
        public void TestCheckSmallStraightFail()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 0;
            game.ValuesDices[2] = 3;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 1;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();

            Assert.Equal(0, gameService.CheckStraight(game, 4));
        }
        [Fact]
        public void TestCheckSmallStraightOK1()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 2;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();

            Assert.Equal(30, gameService.CheckStraight(game, 4));
        }
        [Fact]
        public void TestCheckSmallStraightOK2()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 2;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();

            Assert.Equal(30, gameService.CheckStraight(game, 4));
        }
        [Fact]
        public void TestCheckSmallStraightOK3()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 0;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 2;
            game.ValuesDices[5] = 1;

            var gameService = new GameService();

            Assert.Equal(30, gameService.CheckStraight(game, 4));
        }
        [Fact]
        public void TestCheckHighStraightFail()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 2;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 1;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();

            Assert.Equal(0, gameService.CheckStraight(game, 5));
        }
        [Fact]
        public void TestCheckHighStraightOk1()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 1;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 1;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();

            Assert.Equal(40, gameService.CheckStraight(game, 5));
        }
        [Fact]
        public void TestCheckHighStraightOk2()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 1;
            game.ValuesDices[5] = 1;

            var gameService = new GameService();

            Assert.Equal(40, gameService.CheckStraight(game, 5));
        }
        [Fact]
        public void TestCheckGeneralOk()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Max = 5;
            var gameService = new GameService();
            Assert.Equal(50, gameService.CheckGeneral(game));
        }
        [Fact]
        public void TestCheckGeneralFail()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Max = 4;
            var gameService = new GameService();
            Assert.Equal(0, gameService.CheckGeneral(game));
        }
        [Fact]
        public void TestCheckGeneralPlusOk()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.Max = 5;
            game.Values = new Dictionary<string, int>();
            game.Values["Full"] = 0;
            game.Values["SmallStraight"] = 0;
            game.Values["HighStraight"] = 0;
            player.FreeValues["Ones"] = true;
            player.FreeValues["Twos"] = true;
            player.FreeValues["Threes"] = true;
            player.FreeValues["Fours"] = true;
            player.FreeValues["Fives"] = true;
            player.FreeValues["Sixs"] = true;
            player.Values["General"] = 50;
            var gameService = new GameService();

            gameService.CheckGeneral(game, player);

            Assert.Equal(150, player.Values["General"]);
            Assert.Equal(25, game.Values["Full"]);
            Assert.Equal(30, game.Values["SmallStraight"]);
            Assert.Equal(40, game.Values["HighStraight"]);
        }
        [Fact]
        public void TestCheckFindMax1()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 1;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 1;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 1;

            var gameService = new GameService();
            gameService.FindMax(game);
            Assert.Equal(1, game.Max);
        }
        [Fact]
        public void TestCheckFindMax2()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 2;
            game.ValuesDices[3] = 1;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 1;

            var gameService = new GameService();
            gameService.FindMax(game);
            Assert.Equal(2, game.Max);
        }
        [Fact]
        public void TestCheckFindMax3()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 0;
            game.ValuesDices[2] = 2;
            game.ValuesDices[3] = 0;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 3;

            var gameService = new GameService();
            gameService.FindMax(game);
            Assert.Equal(3, game.Max);
        }
        [Fact]
        public void TestCheckFindMax4()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 0;
            game.ValuesDices[1] = 1;
            game.ValuesDices[2] = 0;
            game.ValuesDices[3] = 4;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();
            gameService.FindMax(game);
            Assert.Equal(4, game.Max);
        }
        [Fact]
        public void TestCheckFindMax5()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            players.Add(player);

            var game = new Game(players);
            game.ValuesDices[0] = 5;
            game.ValuesDices[1] = 0;
            game.ValuesDices[2] = 0;
            game.ValuesDices[3] = 0;
            game.ValuesDices[4] = 0;
            game.ValuesDices[5] = 0;

            var gameService = new GameService();
            gameService.FindMax(game);
            Assert.Equal(5, game.Max);
        }
        [Fact]
        public void TestCountTotal()
        {
            var player = new Player(1, "Romek");
            var players = new List<Player>();
            player.Values["Ones"]= 3;
            player.Values["Twos"]= 6;
            player.Values["Threes"]= 9;
            player.Values["Fours"]= 12;
            player.Values["Fives"]= 15;
            player.Values["Sixs"]= 18;
            player.Values["Bonus"]= 0;
            player.Values["Triple"]= 18;
            player.Values["Fourfold"]= 18;
            player.Values["Full"]= 25;
            player.Values["SmallStraight"]= 0;
            player.Values["HighStraight"]= 40;
            player.Values["General"]= 0;
            player.Values["Chance"]= 24;
            player.Values["Total"]= 0;
            players.Add(player);
            player = new Player(2, "Agnieszka");
            player.Values["Ones"]= 0;
            player.Values["Twos"]= 6;
            player.Values["Threes"]= 9;
            player.Values["Fours"]= 12;
            player.Values["Fives"]= 15;
            player.Values["Sixs"]= 18;
            player.Values["Bonus"]= 0;
            player.Values["Triple"]= 10;
            player.Values["Fourfold"]= 15;
            player.Values["Full"]= 25;
            player.Values["SmallStraight"]= 30;
            player.Values["HighStraight"]= 0;
            player.Values["General"]= 50;
            player.Values["Chance"]= 20;
            player.Values["Total"]= 0;
            players.Add(player);
            var game = new Game(players);

            var gameService = new GameService();
            gameService.CountTotal(players);

            Assert.Equal(223, players[0].Values["Total"]);
            Assert.Equal(210, players[1].Values["Total"]);
        }
    }

}
