// First playable scene in the restaurant.
// Establishes Emma’s relationship and sets flags.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class OpeningScene : Scene
    {
        public OpeningScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("Ten hours into your shift, you deliver scallops to table 14.");
            Game.TypeWriterEffect("\nEmma, the new server, struggles with a tray of drinks.");

            List<string> options = new List<string>
            {
                "Help Emma with the tray",
                "Continue with your own tables (you're already behind)",
                "Give Emma a quick word of advice on carrying technique"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();
            NPC emma = game.GetLocation("The Golden Plate - Kitchen").NPCs.Find(n => n.Name == "Emma");
            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("You steady the tray. Emma exhales. \"Thank you—really.\"");
                    player.ModifyStat("Conscience", 1);
                    if (emma != null) emma.ModifyRelationship(2);
                    game.SetFlag("HelpedEmma", true);
                    break;
                case 2:
                    Game.TypeWriterEffect("You keep moving. A drink spills behind you. You don't look back.");
                    player.ModifyStat("Conscience", -1);
                    if (emma != null) emma.ModifyRelationship(-1);
                    game.SetFlag("IgnoredEmma", true);
                    break;
                case 3:
                    Game.TypeWriterEffect("\"Lower the center of gravity. Elbow tucked,\" you say. The tray steadies. Emma nods.");
                    player.ModifyStat("Wisdom", 1);
                    if (emma != null) emma.ModifyRelationship(1);
                    game.SetFlag("AdvisedEmma", true);
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);

            if(game.CanExplore())
            {
                new ExploreScene(game, player, location, "early").Play();
            }

            Location kitchen = game.GetLocation("The Golden Plate - Kitchen");
            game.ChangeLocation(kitchen);
            new IncidentScene(game, player, kitchen).Play();
        }
    }
}
