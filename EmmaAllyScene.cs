using System.Collections.Generic;

namespace VeridianPact
{
    class EmmaAllyScene : Scene
    {
        public EmmaAllyScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Display location
            Console.Clear();
            game.DisplayLocationInfo();

            // Initial dialogue with Emma
            Game.TypeWriterEffect("Emma manages a small smile. \"Thanks for checking on me. Not many would.\"");
            Game.TypeWriterEffect("\nShe lowers her voice. \"I heard about a job at a new bistro downtown. Better management, better pay. I’m thinking of applying... maybe you should too.\"");
            Game.TypeWriterEffect("\nBefore you can respond, Victor’s voice booms from the dining area, calling for you.");
            Game.TypeWriterEffect("\n\"Marcus! Get out here now!\"");

            // Update Emma's relationship
            NPC emma = location.NPCs.Find(n => n.Name == "Emma");
            if (emma != null) emma.ModifyRelationship(2);

            // Player choices, including romance option if conditions met
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
                // Go to Victor, leads to confrontation
                Game.TypeWriterEffect("You nod to Emma and head toward Victor's voice, bracing yourself.");
                Scene confrontationScene = new ConfrontationScene(game, player, location);
                confrontationScene.Play();
            }
            else if (choice == 2)
            {
                // Get job lead
                Game.TypeWriterEffect("You lean in. \"Tell me more about this bistro.\"");
                Game.TypeWriterEffect("\nEmma whispers, \"It's called The Saffron Veil. They're hiring experienced staff. I can get you the manager's number.\"");
                Item jobLead = new Item("Saffron Veil Job Lead", "Contact info for a job opportunity at a new bistro.", true);
                player.AddItem(jobLead);
                game.SetFlag("HasJobLead", true);
                Game.TypeWriterEffect("\nVictor's shout interrupts again. \"Marcus! Now!\"");
                Scene confrontationScene = new ConfrontationScene(game, player, location);
                confrontationScene.Play();
            }
            else
            {
                // Romance option, sets flag for romantic ending
                Game.TypeWriterEffect("You smile. \"Emma, how about we grab coffee after this shift? We could talk more about that job... or just talk.\"");
                Game.TypeWriterEffect("\nEmma blushes slightly. \"I'd like that. Text me later.\"");
                game.SetFlag("RomancedEmma", true);
                if (emma != null) emma.ModifyRelationship(3);
                Game.TypeWriterEffect("\nVictor's shout interrupts. \"Marcus! Now!\"");
                Scene confrontationScene = new ConfrontationScene(game, player, location);
                confrontationScene.Play();
            }
        }
    }
}