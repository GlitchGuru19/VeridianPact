using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class Player
    {
        public string Name { get; set; } = "Marcus";
        public Dictionary<string, int> Stats { get; private set; }
        public List<Item> Inventory { get; private set; }

        public Player()
        {
            // Initialize player stats
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

        public void AddItem(Item item)
        {
            // Add item to inventory
            Inventory.Add(item);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Added to inventory: {item.Name}");
            Console.ResetColor();
        }

        public void RemoveItem(Item item)
        {
            // Remove item from inventory
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Removed from inventory: {item.Name}");
                Console.ResetColor();
            }
        }

        public void ModifyStat(string stat, int amount)
        {
            // Modify player stat
            if (Stats.ContainsKey(stat))
            {
                Stats[stat] = Math.Max(0, Stats[stat] + amount);
                Console.ForegroundColor = amount > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"{stat} {(amount > 0 ? "increased" : "decreased")} by {Math.Abs(amount)}! New value: {Stats[stat]}");
                Console.ResetColor();
            }
        }

        public void DisplayInventory()
        {
            // Show inventory
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

        public void DisplayStats()
        {
            // Show stats
            Console.WriteLine("\nCharacter Stats:");
            foreach (var stat in Stats)
            {
                Console.WriteLine($"- {stat.Key}: {stat.Value}");
            }
        }
    }
}