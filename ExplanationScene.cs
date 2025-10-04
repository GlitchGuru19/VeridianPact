// Explains both books and lets the player commit to a world.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class ExplanationScene : Scene
    {
        public ExplanationScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Game.TypeWriterEffect("Silver: \"Veridian.\" Keep your memories—gain understanding. Start with nothing else.");
            Game.TypeWriterEffect("\nGold: \"Riches.\" Stay—see patterns no one else can. Wealth becomes a tool in your hands.");
            Game.TypeWriterEffect("\n\"Power demands responsibility,\" the Librarian says. \"Choose.\"");
            player.ModifyStat("Wisdom", 1);

            List<string> options = new List<string>
            {
                "\"I choose the Book of Knowledge.\"",
                "\"I choose the Book of Riches.\"",
                "\"What's the catch? There's always a catch.\""
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();
            Scene nextScene = null;

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("Silver opens like dawn.");
                    nextScene = new VeridianIntroScene(game, player);
                    break;

                case 2:
                    Game.TypeWriterEffect("Gold opens like noon.");
                    nextScene = new RichesIntroScene(game, player);
                    break;

                case 3:
                    Game.TypeWriterEffect("He nods. \"Knowledge may separate you. Wealth may change you.\" Both cost. Both pay.");
                    player.ModifyStat("Wisdom", 1);

                    List<string> finalOptions = new List<string>
                    {
                        "\"I choose the Book of Knowledge.\"",
                        "\"I choose the Book of Riches.\""
                    };

                    DisplayOptions(finalOptions);
                    int finalChoice = GetPlayerChoice(finalOptions.Count);
                    nextScene = finalChoice == 1 ? new VeridianIntroScene(game, player) : new RichesIntroScene(game, player);
                    break;
            }

            nextScene?.Play();
        }
    }
}
