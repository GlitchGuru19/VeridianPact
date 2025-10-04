// Deepen bond with Emma. Potential romance and job lead. Then Victor calls.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class EmmaAllyScene : Scene
    {
        public EmmaAllyScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("Emma smiles shyly. \"Thanks for checking on me. Not many would.\"");
            Game.TypeWriterEffect("\n\"A new bistro—The Saffron Veil. Fair managers, fair pay,\" she whispers. \"Apply. Maybe both of us.\"");
            Game.TypeWriterEffect("\nVictor: \"Marcus! Now!\"");

            NPC emma = location.NPCs.Find(n => n.Name == "Emma");
            if (emma != null) emma.ModifyRelationship(2);

            List<string> options = new List<string>
            {
                "Go see what Victor wants",
                "Ask Emma for more details about the job"
            };
            if (player.Stats["Charisma"] >= 8 && emma != null && emma.RelationshipValue >= 5)
            {
                options.Add("Invite Emma for coffee after the shift to talk more");
            }

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            if (choice == 1)
            {
                Game.TypeWriterEffect("You nod and brace for the storm.");
                new ConfrontationScene(game, player, location).Play();
            }
            else if (choice == 2)
            {
                Game.TypeWriterEffect("\"Tell me more.\" \"The Saffron Veil,\" she says. \"I'll get the manager’s number.\"");
                Item jobLead = new Item("Saffron Veil Job Lead", "Contact info for a fair bistro.", true);
                player.AddItem(jobLead);
                game.SetFlag("HasJobLead", true);
                Game.TypeWriterEffect("\n\"Marcus! Now!\"");
                new ConfrontationScene(game, player, location).Play();
            }
            else
            {
                Game.TypeWriterEffect("\"Coffee after? To talk—or just talk.\" \"Text me,\" Emma says.");
                game.SetFlag("RomancedEmma", true);
                if (emma != null) emma.ModifyRelationship(3);
                Game.TypeWriterEffect("\nVictor again: \"Marcus!\"");
                new ConfrontationScene(game, player, location).Play();
            }
        }
    }
}
