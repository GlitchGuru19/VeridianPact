// Presents the two books: Knowledge (Veridian) and Riches (Modern).
// Routes to ExplanationScene for the actual choice.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class OfferScene : Scene
    {
        public OfferScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Game.TypeWriterEffect("The Librarian opens a leather bag. Two books—one silver clasp, one gold.");
            Game.TypeWriterEffect("\n\"Two worlds,\" he says. \"Two pacts.\"");

            List<string> options = new List<string>
            {
                "\"What exactly do these books do?\"",
                "\"Is this a joke? Did Victor put you up to this?\"",
                "Reach for one of the books immediately"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();
            player.ModifyStat("Wisdom", 1);

            new ExplanationScene(game, player, location).Play();
        }
    }
}
