// World region model:
// - Holds NPCs and items present, add/remove helpers

using System.Collections.Generic;

namespace VeridianPact
{
    class Location
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<NPC> NPCs { get; private set; }
        public List<Item> Items { get; private set; }

        public Location(string name, string description)
        {
            Name = name;
            Description = description;
            NPCs = new List<NPC>();
            Items = new List<Item>();
        }

        public void AddNPC(NPC npc) => NPCs.Add(npc);
        public void RemoveNPC(NPC npc) => NPCs.Remove(npc);

        public void AddItem(Item item) => Items.Add(item);
        public void RemoveItem(Item item) => Items.Remove(item);
    }
}
