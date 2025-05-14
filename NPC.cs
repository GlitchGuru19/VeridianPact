using System;
using VeridianPact;

namespace VeridianPact
{
    class NPC
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int RelationshipValue { get; private set; }

        // Initializes an NPC with a name and description
        public NPC(string name, string description)
        {
            Name = name;
            Description = description;
            RelationshipValue = 0;
        }

        // Modifies the relationship value with the NPC
        public void ModifyRelationship(int amount)
        {
            RelationshipValue += amount;
        }

        // Handles interaction with the player, displaying a basic message
        public virtual void Interact(Player player)
        {
            Console.WriteLine($"You approach {Name}.");
        }
    }
}