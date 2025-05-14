using System;
using System.Collections.Generic;
using VeridianPact;


namespace VeridianPact
{
    class Player
    {
        public string Name { get; set; } = "Marcus";
        public Dictionary<string, int> Stats { get; private set; }
        public List<Item> Inventory { get; private set; }

        // Initializes the player with default stats and an empty inventory
        public Player()
        {
            Stats = new Dictionary<string, int>
            {
                { "Wisdom", 7 },
                { "Charisma", 6 },
                { "Resourcefulness", 5 },
                { "Conscience", 8 },
                { "Courage", 7 }
            };
            Inventory = new List<Item>();
        }

        // Adds an item to the player's inventory and notifies the player
        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Added to inventory: {item.Name}");
            Console.ResetColor();
        }

        // Removes an item from the player's inventory and notifies the player
        public void RemoveItem(Item item)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Removed from inventory: {item.Name}");
                Console.ResetColor();
            }
        }

        // Modifies a player's stat by a specified amount, preventing negative values
        public void ModifyStat(string stat, int amount)
        {
            if (Stats.ContainsKey(stat))
            {
                Stats[stat] = Math.Max(0, Stats[stat] + amount);
                Console.ForegroundColor = amount > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"{stat} {(amount > 0 ? "increased" : "decreased")} by {Math.Abs(amount)}! New value: {Stats[stat]}");
                Console.ResetColor();
            }
        }

        // Displays the player's current inventory
        public void DisplayInventory()
        {
            Console.WriteLine("\nInventory:");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
        }

        // Displays the player's current stats
        public void DisplayStats()
        {
            Console.WriteLine("\nCharacter Stats:");
            foreach (var stat in Stats)
            {
		// These are more like key value pairs learnt in C# course
                Console.WriteLine($"- {stat.Key}: {stat.Value}");
            }
        }
    }
}