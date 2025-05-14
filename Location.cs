using System.Collections.Generic;
using VeridianPact;

namespace VeridianPact
{
    class Location
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<NPC> NPCs { get; private set; }
        public List<Item> Items { get; private set; }

        // Initializes a location with a name and description
        public Location(string name, string description)
        {
            Name = name;
            Description = description;
            NPCs = new List<NPC>();
            Items = new List<Item>();
        }

        // Adds an NPC to the location
        public void AddNPC(NPC npc) => NPCs.Add(npc);

        // Removes an NPC from the location
        public void RemoveNPC(NPC npc) => NPCs.Remove(npc);

        // Adds an item to the location
        public void AddItem(Item item) => Items.Add(item);

        // Removes an item from the location
        public void RemoveItem(Item item) => Items.Remove(item);
    }
}