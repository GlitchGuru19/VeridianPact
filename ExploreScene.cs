// ExploreScene, its an off story scene kinda
using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class ExploreScene : Scene
    {
        private string context; // "early" or "world"

        public ExploreScene(Game game, Player player, Location location, string context) : base(game, player, location)
        {
            this.context = context;
        }

        public override void Play()
        {
            if (!game.CanExplore())
            {
                Game.TypeWriterEffect("You feel too drained to explore further.");
                return;
            }

            game.IncrementExplore();

            Console.Clear();
            Game.TypeWriterEffect("You take a moment to explore...");

            List<string> options = new List<string>();

            if (context == "early")
            {
                options.Add("Visit Aurora City Library (meet Magenta, gain Charisma)");
                options.Add("Visit Rooftop Aviary (see Aster the falcon)");
                options.Add("Read 'The Monk Who Sold His Ferrari' (gain Wisdom and Conscience)");
                options.Add("Skip exploration");
            }
            else // context == "world"
            {
                options.Add("Wander the Market Bazaar (gain Resourcefulness)");
                options.Add("Meditate under a Veridian oak (gain Wisdom and Courage)");
                if (game.GetFlag("ReadFerrariBook"))
                    options.Add("Re-read your Ferrari notes (gain Conscience)");
                options.Add("Skip exploration");
            }

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            if (context == "early")
            {
                switch (choice)
                {
                    case 1:
                        Game.TypeWriterEffect("At the library, Magenta greets you. \"Build something people want to belong to.\"");
                        player.ModifyStat("Charisma", 1);
                        break;
                    case 2:
                        Game.TypeWriterEffect("On the rooftop, Aster the falcon circles. You feel watched, in a good way.");
                        break;
                    case 3:
                        Game.TypeWriterEffect("You read 'The Monk Who Sold His Ferrari'. Lessons of discipline, purpose, and mindfulness settle in.");
                        player.ModifyStat("Wisdom", 2);
                        player.ModifyStat("Conscience", 1);
                        game.SetFlag("ReadFerrariBook", true);
                        break;
                    default:
                        Game.TypeWriterEffect("You decide not to explore further.");
                        break;
                }
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        Game.TypeWriterEffect("You wander the bazaar, learning the art of bargaining.");
                        player.ModifyStat("Resourcefulness", 1);
                        break;
                    case 2:
                        Game.TypeWriterEffect("You meditate under an oak. Calm fills you.");
                        player.ModifyStat("Wisdom", 1);
                        player.ModifyStat("Courage", 1);
                        break;
                    case 3:
                        if (game.GetFlag("ReadFerrariBook"))
                        {
                            Game.TypeWriterEffect("You revisit your Ferrari notes. The lessons deepen.");
                            player.ModifyStat("Conscience", 1);
                        }
                        else
                        {
                            Game.TypeWriterEffect("You skip exploration.");
                        }
                        break;
                    default:
                        Game.TypeWriterEffect("You skip exploration.");
                        break;
                }
            }
        }
    }
}
