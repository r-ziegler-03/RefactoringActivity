namespace RefactoringActivity;

public class Player
{
    public int Health { get; private set; }
    public string CurrentLocation { get; set; }
    public List<string> Inventory { get;}

    public Player(int health)
    {
        Health = health;
        CurrentLocation = "Start";
        Inventory = new List<string>();
    }

    public void ShowInventory()
    {
        PlayerInventory();
    }

    public void PoisonDamagePlayer()
    {
        Health -= 10;
    }
    

    private void PlayerInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (string item in Inventory)
            {
                Console.WriteLine($"- {item}");
            }
        }
    }
}