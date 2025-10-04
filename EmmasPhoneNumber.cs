// Usable item to call Emma, set ContactedEmma flag, and boost relationship.
// Fixed: store a Game reference via constructor.

using System;

namespace VeridianPact
{
    class EmmasPhoneNumber : Item
    {
        private readonly Game game;

        public EmmasPhoneNumber(Game gameRef) : base("Emma's Phone Number", "A phone number written on a napkin.", true)
        {
            game = gameRef;
        }

        public override void Use(Player player, GameState gameState)
        {
            Console.WriteLine("You call Emma using the number on the napkin.");
            if (gameState.GetFlag("HasJobLead"))
            {
                Console.WriteLine("Emma: \"Did you check The Saffron Veil? I got hired! They're still looking for someone like you.\"");
                gameState.SetFlag("ContactedEmma", true);
            }
            else
            {
                Console.WriteLine("Emma: \"Good to hear your voice. Let’s catch up soon.\"");
                gameState.SetFlag("ContactedEmma", true);
            }

            NPC emma = null;
            foreach (var loc in game.GetLocations())
            {
                emma = loc.NPCs.Find(n => n.Name == "Emma");
                if (emma != null) break;
            }
            if (emma != null) emma.ModifyRelationship(1);
        }
    }
}
