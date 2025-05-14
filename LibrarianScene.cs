using System.Collections.Generic;

namespace VeridianPact
{
    class LibrarianScene : Scene
    {
        public LibrarianScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Game.TypeWriterEffect("The figure steps forward into the light. He's an elderly man in an impeccably tailored suit that seems somehow both modern and ancient. His eyes shine with an unusual intensity.");
            Game.TypeWriterEffect("\n\"Marcus Thornhill,\" he says, his voice surprisingly resonant. \"A pleasure to finally meet you.\"");
            Game.TypeWriterEffect("\n\"Do I know you?\" you ask, certain you've never seen this man before.");
            Game.TypeWriterEffect("\nHe smiles. \"No, but I know you. I know people, you see. Just as you do.\"");
            Game.TypeWriterEffect("\nThe man approaches, extending his hand. \"Some call me The Librarian. I curate knowledge and... opportunities.\"");

            List<string> options = new List<string>
            {
                "\"How do you know my name? What do you want?\"",
                "\"Look, I just lost my job. I'm not in the mood for whatever this is.\"",
                "Shake his hand and hear him out"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("The Librarian's smile widens, as if your suspicion pleases him.");
                    Game.TypeWriterEffect("\n\"I make it my business to know people of interest. And you, Marcus, are very interesting indeed.\"");
                    player.ModifyStat("Wisdom", 1);
                    break;
                case 2:
                    Game.TypeWriterEffect("\"Precisely why I'm here,\" the Librarian says, unperturbed by your tone.");
                    Game.TypeWriterEffect("\n\"Moments of crisis are also moments of opportunity. I have a significant opportunity for you.\"");
                    break;
                case 3:
                    Game.TypeWriterEffect("You shake his hand. His grip is surprisingly strong for someone who appears so elderly.");
                    Game.TypeWriterEffect("\n\"A pleasure,\" he says. \"I've been watching your career with interest. Your wasted potential, specifically.\"");
                    player.ModifyStat("Wisdom", 1);
                    break;
            }

            Scene offerScene = new OfferScene(game, player, location);
            offerScene.Play();
        }
    }
}