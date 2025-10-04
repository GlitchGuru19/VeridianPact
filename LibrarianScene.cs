// Introduces the Librarian and transitions into the offer of two books.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class LibrarianScene : Scene
    {
        public LibrarianScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Game.TypeWriterEffect("The figure steps forward, suit both modern and ancient. \"Marcus Thornhill,\" he says. \"Finally.\"");

            List<string> options = new List<string>
            {
                "\"How do you know my name? What do you want?\"",
                "\"I just lost my job. Spare me the riddles.\"",
                "Shake his hand and hear him out"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("He smiles. \"I notice interesting people. You’re very interesting.\"");
                    player.ModifyStat("Wisdom", 1);
                    break;
                case 2:
                    Game.TypeWriterEffect("\"Crisis is an honest door,\" he says. \"I’m offering you two more.\"");
                    break;
                case 3:
                    Game.TypeWriterEffect("His grip surprises you. \"Wasted potential,\" he murmurs. You feel properly seen.");
                    player.ModifyStat("Wisdom", 1);
                    break;
            }

            new OfferScene(game, player, location).Play();
        }
    }
}
