namespace RefactoringActivity;

public class Location
{
    public string Name;
    public string Description;
    public Dictionary<string, string> Exits;
    public List<string> Items;
    public List<Puzzle> Puzzles;
    
    public Location(string name, string description)
    {
        Name = name;
        Description = description;
        Exits = new Dictionary<string, string>();
        Items = new List<string>();
        Puzzles = new List<Puzzle>();
    }
}