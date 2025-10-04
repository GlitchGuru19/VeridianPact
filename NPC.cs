// Non-player character model:
// - Name, description, relationship value
// - Basic Interact hook

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
            Name = name;
            Description = description;
            RelationshipValue = 0;
        }

        public void ModifyRelationship(int amount)
        {
            RelationshipValue = Math.Max(0, RelationshipValue + amount);
        }

        public virtual void Interact(Player player)
        {
            Console.WriteLine($"You approach {Name}.");
            Console.WriteLine($"{Description}");
            Console.WriteLine($"Relationship: {RelationshipValue}");
        }
    }
}
