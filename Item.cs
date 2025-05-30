using System;

namespace VeridianPact
{
    class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsUsable { get; private set; }

        public Item(string name, string description, bool isUsable = true)
        {
            // Initialize item properties
            Name = name;
            Description = description;
            IsUsable = isUsable;
        }

        public virtual void Use(Player player, GameState gameState)
        {
            // Default item use behavior
            Console.WriteLine($"You use the {Name}.");
        }
    }
}