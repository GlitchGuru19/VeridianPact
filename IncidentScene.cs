// Victor berates Emma, prompting a moral choice.
// Routes to Confrontation or Emma ally path.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class IncidentScene : Scene
    {
        public IncidentScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("Twenty minutes later...");
            Game.TypeWriterEffect("\nVictor storms in, brandishing a returned beef wellington.");
            Game.TypeWriterEffect("\n\"Who's responsible for table 22?\"");
            Game.TypeWriterEffect("\nEmma raises a trembling hand.");
            Game.TypeWriterEffect("\nHe dresses her down in public; the kitchen stills around his voice.");

            List<string> options = new List<string>
            {
                player.Stats["Courage"] >= 7 ? "Intervene directly and stand up for Emma" : "Intervene directly and stand up for Emma (Requires Courage > 7)",
                "Wait until Victor leaves, then comfort Emma",
                "Focus on your work - getting involved might cost your job"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);
            Console.Clear();

            NPC emma = location.NPCs.Find(n => n.Name == "Emma");

            switch (choice)
            {
                case 1:
                    if (player.Stats["Courage"] < 7)
                    {
                        Game.TypeWriterEffect("You start to speak—but swallow it. The silence burns.");
                        player.ModifyStat("Conscience", -1);
                        if (emma != null) emma.ModifyRelationship(-1);
                        Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                        return;
                    }
                    Game.TypeWriterEffect("\"Enough,\" you say, stepping between Victor and Emma. The room exhales.");
                    player.ModifyStat("Courage", 2);
                    player.ModifyStat("Wisdom", 1);
                    if (emma != null) emma.ModifyRelationship(2);
                    game.SetFlag("IntervenedForEmma", true);
                    Console.WriteLine("\nPress any key to continue your shift...");
                    Console.ReadKey(true);
                    new ConfrontationScene(game, player, location).Play();
                    break;

                case 2:
                    Game.TypeWriterEffect("Victor storms off. Emma wipes a tear. \"Thank you for staying,\" she whispers when you walk over.");
                    player.ModifyStat("Conscience", 1);
                    if (emma != null) emma.ModifyRelationship(1);
                    game.SetFlag("ComfortedEmma", true);
                    Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                    new EmmaAllyScene(game, player, location).Play();
                    break;

                case 3:
                    Game.TypeWriterEffect("You plate without looking up. The shouting fades—then returns for you.");
                    Game.TypeWriterEffect("\n\"Marcus! Table 9 wrong. You're done!\" Victor points to the door.");
                    player.ModifyStat("Conscience", -2);
                    if (emma != null) emma.ModifyRelationship(-2);
                    game.SetFlag("IgnoredEmma", true);
                    Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                    Location victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    new FiringScene(game, player, victorsOffice).Play();
                    break;
            }
        }
    }
}
