using System;
using GameState;

namespace VeridianPact
{
    class Item
    {
        public string? Name {get; private set; }
        public string? Description { get; private set; }
        public bool isUsable { get; private set; }

        // Initializes an item with a name, description, and usability flag
        public Item(string name, string description, bool isUsable = true)
        {
            Name = name;
            Description = description;
            IsUsable = isUsable;
        }

        // Use the item displaying a basic usage message
        public virtual void Use(Player player, GameState gameState)
        {
            Console.WriteLine($"You use the {Name}.");
        }
    }
}