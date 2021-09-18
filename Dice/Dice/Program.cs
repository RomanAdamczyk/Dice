using Dice.App.Abstract;
using Dice.App.Concrete;
using Dice.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Dice
{
    public class Program
    {
        static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            List<Player> players = new List<Player>();
            Boolean proceed = true;
               
            Console.WriteLine("Witaj w grze w kości");
            while (proceed)
            { 
            Console.WriteLine("Wybierz z menu co chcesz robić");
            var mainManu = actionService.GetMenuActionsByName("Main");
            for (int i = 0; i < mainManu.Count; i++)
            {
                Console.WriteLine($"{mainManu[i].Id}. {mainManu[i].Name}");
            }
            var operation = Console.ReadKey();
            Console.WriteLine();
            GameService gameService = new GameService();
                switch (operation.KeyChar)
                {
                    case '1':
                        players = gameService.NewGame();
                        gameService.Game(players);
                        break;
                    case '2':
                        break;
                    case '3':
                        mainManu = actionService.GetMenuActionsByName("HelpMain");
                        for (int i = 0; i < mainManu.Count; i++)
                        {
                            Console.WriteLine($"{mainManu[i].Id}. {mainManu[i].Name}");
                        }
                        operation = Console.ReadKey();
                        Console.WriteLine();
                        switch (operation.KeyChar)
                        {
                            case '1':
                                actionService.ViewRules();
                                break;
                            case '2':
                                actionService.ViewInstruction();
                                break;
                            default:
                                Console.WriteLine("Wybrana akcja nie istnieje");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Wybrana akcja nie istnieje");
                        break;
                }
                Console.WriteLine("Kliknij dowolny klawisz, aby wrócić do głównego menu.");
                Console.WriteLine("kliknij 'q' jeśli chcesz zakończyć");
                operation = Console.ReadKey();
                if (operation.KeyChar == 'q')
                {
                    proceed = false;
                }

            }


        }



    }
}
