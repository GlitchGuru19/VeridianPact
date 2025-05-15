using System;

namespace VeridianPact
{
    class NPC
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int RelationshipValue { get; private set; }

        public NPC(string name, string description)
        {
            // Initialize NPC properties
            Name = name;
            Description = description;
            RelationshipValue = 0;
        }

        public void ModifyRelationship(int amount)
        {
            // Update relationship value
            RelationshipValue = Math.Max(0, RelationshipValue + amount);
        }

        public virtual void Interact(Player player)
        {
            // Default interaction
            Console.WriteLine($"You approach {Name}.");
            Console.WriteLine($"{Description}");
            Console.WriteLine($"Relationship: {RelationshipValue}");
        }
    }
}