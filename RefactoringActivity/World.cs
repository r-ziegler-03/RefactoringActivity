﻿namespace RefactoringActivity;

public class World
{
    public Dictionary<string, Location> Locations;

    public World()
    {
        Locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        Location start = new("You are at the starting point of your adventure.");
        Location forest = new("You are in a dense, dark forest.");
        Location cave = new("You see a dark, ominous cave.");

        AddExits(start, forest, cave);

        AddItems(start, forest, cave);

        start.Puzzles.Add(new Puzzle("riddle",
            "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well"));

        AddLocations(start, forest, cave);
    }

    private void AddLocations(Location start, Location forest, Location cave)
    {
        Locations.Add("Start", start);
        Locations.Add("Forest", forest);
        Locations.Add("Cave", cave);
    }

    private static void AddItems(Location start, Location forest, Location cave)
    {
        start.Items.Add("map");
        forest.Items.Add("key");
        forest.Items.Add("potion");
        cave.Items.Add("sword");
    }

    private static void AddExits(Location start, Location forest, Location cave)
    {
        start.Exits.Add("north", "Forest");
        forest.Exits.Add("south", "Start");
        forest.Exits.Add("east", "Cave");
        cave.Exits.Add("west", "Forest");
    }


    public string GetLocationDescription(string locationName)
    {
        return LocationDescription(locationName);
    }

    private string LocationDescription(string locationName)
    {
        if (Locations.ContainsKey(locationName)) 
            return Locations[locationName].Description;
        return "Unknown location.";
    }

    public string GetLocationDetails(string locationName)
    {
        return LocationDetails(locationName);
    }

    private string LocationDetails(string locationName)
    {
        if (!Locations.ContainsKey(locationName)) 
            return "Unknown location.";

        Location location = Locations[locationName];
        string details = location.Description;
        
        if (location.Exits.Count > 0)
        {
            details += " Exits lead: ";
            foreach (string exit in location.Exits.Keys)
                details += exit + ", ";
            details = details.Substring(0, details.Length - 2);
        }

        if (location.Items.Count > 0)
        {
            details += "\nYou see the following items:";
            foreach (string item in location.Items) 
                details += $"\n- {item}";
        }

        if (location.Puzzles.Count > 0)
        {
            details += "\nYou see the following puzzles:";
            foreach (Puzzle puzzle in location.Puzzles) 
                details += $"\n- {puzzle.Name}";
        }

        return details;
    }

    public bool TakeItem(Player player, string itemName)
    {
        return PlayerTakeItem(player, itemName);
    }

    private bool PlayerTakeItem(Player player, string itemName)
    {
        Location location = Locations[player.CurrentLocation];
        if (location.Items.Contains(itemName))
        {
            location.Items.Remove(itemName);
            player.Inventory.Add(itemName);
            Console.WriteLine($"You take the {itemName}.");
            return true;
        }

        return false;
    }

    public bool MovePlayer(Player player, string direction)
    {
        return MovesPlayer(player, direction);
    }

    private bool MovesPlayer(Player player, string direction)
    {
        if (Locations[player.CurrentLocation].Exits.ContainsKey(direction))
        {
            player.CurrentLocation = Locations[player.CurrentLocation].Exits[direction];
            return true;
        }

        return false;
    }

    public bool UseItem(Player player, string itemName)
    {
        return PlayerUseItem(player, itemName);
    }

    private static bool PlayerUseItem(Player player, string itemName)
    {
        if (player.Inventory.Contains(itemName))
        {
            if (itemName == "potion")
            {
                Console.WriteLine("Ouch! That tasted like poison!");
                player.PoisonDamagePlayer();
                Console.WriteLine($"Your health is now {player.Health}.");
            }
            else
            {
                Console.WriteLine($"The {itemName} disappears in a puff of smoke!");
            }
            player.Inventory.Remove(itemName);
            return true;
        }

        return false;
    }

    public bool SolvePuzzle(Player player, string puzzleName)
    {
        return TryToSolvePuzzle(player, puzzleName);
    }

    private bool TryToSolvePuzzle(Player player, string puzzleName)
    {
        Location location = Locations[player.CurrentLocation];
        Puzzle? puzzle = location.Puzzles.Find(p => p.Name == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.Puzzles.Remove(puzzle);
            return true;
        }

        return false;
    }
}