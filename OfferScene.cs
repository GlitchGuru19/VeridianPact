using System.Collections.Generic;

namespace VeridianPact
{
    class OfferScene : Scene
    {
        public OfferScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Librarian presents the books
            Game.TypeWriterEffect("The Librarian produces a leather messenger bag and opens it with reverence.");
            Game.TypeWriterEffect("\n\"I've been watching you for some time, Marcus. Your gift for understanding others, your patience, your insight. Wasted here.\"");
            Game.TypeWriterEffect("\nHe removes two identical leather-bound books from his bag. One has a silver clasp, the other gold.");
            Game.TypeWriterEffect("\n\"You stand at a crossroads. I'm here to offer you a choice.\"");
            Game.TypeWriterEffect("\nHe places both books on the bench between you.");
            Game.TypeWriterEffect("\n\"The Book of Knowledge,\" he says, touching the silver clasp. \"Or the Book of Riches,\" touching the gold one.");
            Game.TypeWriterEffect("\n\"Each will transform your life, but in very different ways.\"");

            // Player choices
            List<string> options = new List<string>
            {
                "\"What exactly do these books do?\"",
                "\"Is this some kind of joke? Did Victor put you up to this?\"",
                "Reach for one of the books immediately"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            if (choice == 1)
            {
                // Ask for explanation
                player.ModifyStat("Wisdom", 1);
                ExplanationScene explanationScene = new ExplanationScene(game, player, location);
                explanationScene.Play();
            }
            else if (choice == 2)
            {
                // Suspect Victor
                Game.TypeWriterEffect("The Librarian chuckles softly.");
                Game.TypeWriterEffect("\n\"I assure you, Victor is entirely unaware of my existence. Few people are.\"");
                Game.TypeWriterEffect("\nHe touches each book again.");
                Game.TypeWriterEffect("\n\"These are not ordinary books. They are catalysts for transformation.\"");
                ExplanationScene explanationScene = new ExplanationScene(game, player, location);
                explanationScene.Play();
            }
            else
            {
                // Impulsive choice
                Game.TypeWriterEffect("\"Interesting,\" the Librarian says as your hand moves toward the books.");
                Game.TypeWriterEffect("\n\"But perhaps you should understand the nature of your choice before making it.\"");
                Game.TypeWriterEffect("\nHe gently places his hand on yours, stopping your reach.");
                ExplanationScene explanationScene = new ExplanationScene(game, player, location);
                explanationScene.Play();
            }
        }
    }
}