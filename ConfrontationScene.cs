// Contextual heat with Victor, routes to BreakingPoint scene.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class ConfrontationScene : Scene
    {
        public ConfrontationScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            if (game.GetFlag("ComfortedEmma"))
            {
                Game.TypeWriterEffect("\"Wasting time talking instead of working?\" Victor snaps.");
                Game.TypeWriterEffect("\n\"I was helping Emma after you humiliated her,\" you reply.");
            }
            else if (game.GetFlag("IgnoredEmma"))
            {
                Game.TypeWriterEffect("\"Table 9 was late,\" Victor shouts.");
                Game.TypeWriterEffect("\n\"I'm working fast,\" you say.");
            }
            else
            {
                Game.TypeWriterEffect("\"Excuse me?\" Victor's eyes narrow.");
                Game.TypeWriterEffect("\n\"That's enough. She's new. Public humiliation doesn't train anyone.\"");
            }

            List<string> options = new List<string>
            {
                "\"I've watched you mistreat good people for years. It stops now.\"",
                "\"You're right. I apologize. I spoke out of turn.\" (Back down)",
                "\"I've covered extra shifts for three years without complaint or recognition.\"",
                player.Stats["Charisma"] >= 7 ? "Calmly explain your perspective" : "Calmly explain your perspective (Requires Charisma >= 7)"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            Location victorsOffice;
            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("Victor points to his office. \"Now.\"");
                    player.ModifyStat("Courage", 2);
                    player.ModifyStat("Wisdom", 1);
                    victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    new BreakingPointScene(game, player, victorsOffice).Play();
                    break;
                case 2:
                    Game.TypeWriterEffect("Victor exhales. \"Get back to work.\" You do—ashamed but unhurt.");
                    player.ModifyStat("Courage", -2);
                    Console.WriteLine("\nPress any key to continue your shift...");
                    Console.ReadKey(true);
                    break;
                case 3:
                    Game.TypeWriterEffect("Victor's anger turns calculating. \"Then remember your place.\" He points to the office.");
                    player.ModifyStat("Courage", 1);
                    victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    new BreakingPointScene(game, player, victorsOffice).Play();
                    break;
                case 4:
                    if (player.Stats["Charisma"] < 7)
                    {
                        Game.TypeWriterEffect("You try calm words. They wobble. \"Office. Now,\" he says.");
                        victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                        game.ChangeLocation(victorsOffice);
                        new BreakingPointScene(game, player, victorsOffice).Play();
                    }
                    else
                    {
                        Game.TypeWriterEffect("You choose respect without surrender. \"Focus on the customers.\" He nods; you return to work.");
                        player.ModifyStat("Charisma", 1);
                        Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                    }
                    break;
            }
        }
    }
}
