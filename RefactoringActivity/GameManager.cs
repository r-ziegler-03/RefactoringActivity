﻿namespace RefactoringActivity;

public class GameManager
{
    private bool IsRunning;
    private Player Player;
    private World World;

    public GameManager()
    {
        IsRunning = true;
        Player = new Player(100);
        World = new World();
    }
    public void RunGame()
    {
        WelcomeMessage();

        while (IsRunning)
        {
            CurrentPlayerLocation();
            string input = Console.ReadLine()?.ToLower();
            if (string.IsNullOrEmpty(input)) 
                return;

            if (input == "help")
            {
                PlayerCommands();
            }
            else if (input.StartsWith("go"))
            {
                PlayerMove(input);
            }
            else if (input.StartsWith("take"))
            {
                PlayerTakeItem(input);
            }
            else if (input.StartsWith("use"))
            {
                PlayerUseItem(input);
            }
            else if (input == "inventory")
            {
                Player.ShowInventory();
            }
            else if (input.StartsWith("solve"))
            {
                PlayerSolvePuzzle(input);
            }
            else if (input == "quit")
            {
                IsRunning = false;
                Console.WriteLine("Thanks for playing!");
            }
            else
            {
                Console.WriteLine("Unknown command. Try 'help'.");
            }
        }
    }

    private void PlayerSolvePuzzle(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string puzzleName = parts[1];
            if (World.SolvePuzzle(Player, puzzleName))
            {
                Console.WriteLine($"You solved the {puzzleName} puzzle!");
            }
            else
            {
                Console.WriteLine($"That's not the right solution for the {puzzleName} puzzle.");
            }
        }
        else
        {
            Console.WriteLine("Solve what?");
        }
    }

    private void PlayerUseItem(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string itemName = parts[1];
            if (!World.UseItem(Player, itemName))
            {
                Console.WriteLine($"You can't use the {itemName} here.");
            }
        }
        else
        {
            Console.WriteLine("Use what?");
        }
    }

    private void PlayerTakeItem(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string itemName = parts[1];
            if (!World.TakeItem(Player, itemName))
            {
                Console.WriteLine($"There is no {itemName} here.");
            }
        }
        else
        {
            Console.WriteLine("Take what?");
        }
    }

    private void PlayerMove(string input)
    {
        string[] parts = input.Split(' ');
        if (parts.Length > 1)
        {
            string direction = parts[1];
            if (World.MovePlayer(Player, direction))
            {
                Console.WriteLine($"You move {direction}.");
            }
            else
            {
                Console.WriteLine("You can't go that way.");
            }
        }
        else
        {
            Console.WriteLine("Move where? (north, south, east, west)");
        }
    }

    private static void PlayerCommands()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("- go [direction]: Move in a direction (north, south, east, west).");
        Console.WriteLine("- take [item]: Take an item from your current location.");
        Console.WriteLine("- use [item]: Use an item in your inventory.");
        Console.WriteLine("- solve [puzzle]: Solve a puzzle in your current location.");
        Console.WriteLine("- inventory: View the items in your inventory.");
        Console.WriteLine("- quit: Exit the game.");
    }

    private void CurrentPlayerLocation()
    {
        Console.WriteLine();
        Console.WriteLine(World.GetLocationDetails(Player.CurrentLocation));
        Console.Write("> ");
    }

    private static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to the Text Adventure Game!");
        Console.WriteLine("Type 'help' for a list of commands.");
    }
}