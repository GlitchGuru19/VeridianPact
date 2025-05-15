using System.Collections.Generic;

namespace VeridianPact
{
    class ExplanationScene : Scene
    {
        public ExplanationScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Librarian explains the books
            Game.TypeWriterEffect("The Librarian's eyes light up, as if he's pleased by your question.");
            Game.TypeWriterEffect("\n\"The Book of Knowledge will transport you to a world called Veridian. There, you will retain all your memories, but gain something precious: complete understanding. The laws of nature, the motives of people, the patterns of success - all will be clear to you. But you will start with nothing else.\"");
            Game.TypeWriterEffect("\nHe gestures to the other book.");
            Game.TypeWriterEffect("\n\"The Book of Riches keeps you here, in this world you know. But it bestows upon you wealth beyond imagination. Not through magic, but through insight into patterns most cannot see. The stock market, business opportunities, investments - all will become obvious choices to you.\"");
            Game.TypeWriterEffect("\nHe leans forward. \"Both paths offer power. Both demand responsibility. The question is: which do you value more? Knowledge or wealth?\"");
            player.ModifyStat("Wisdom", 1);

            // Player choices
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
                    // Choose Knowledge
                    Game.TypeWriterEffect("You reach for the book with the silver clasp.");
                    Game.TypeWriterEffect("\nThe Librarian nods approvingly. \"A wise choice. Knowledge is the true currency of existence.\"");
                    Game.TypeWriterEffect("\nAs your fingers touch the cool silver clasp, it springs open of its own accord.");
                    nextScene = new VeridianIntroScene(game, player);
                    break;
                case 2:
                    // Choose Riches
                    Game.TypeWriterEffect("You reach for the book with the gold clasp.");
                    Game.TypeWriterEffect("\nThe Librarian nods. \"Wealth opens many doors in your world. Use it wisely.\"");
                    Game.TypeWriterEffect("\nAs your fingers touch the warm gold clasp, it springs open of its own accord.");
                    nextScene = new RichesIntroScene(game, player);
                    break;
                case 3:
                    // Ask about catch
                    Game.TypeWriterEffect("The Librarian's smile falters slightly.");
                    Game.TypeWriterEffect("\n\"Perceptive. Yes, there is always a balance to be maintained.\"");
                    Game.TypeWriterEffect("\n\"The Book of Knowledge grants understanding, but separates you from all you know. The Book of Riches grants prosperity, but may change how others see you - and how you see yourself.\"");
                    Game.TypeWriterEffect("\n\"Neither path is without challenges. Both require sacrifice.\"");
                    Game.TypeWriterEffect("\n\"Now, knowing this, which do you choose?\"");
                    player.ModifyStat("Wisdom", 1);

                    List<string> finalOptions = new List<string>
                    {
                        "\"I choose the Book of Knowledge.\"",
                        "\"I choose the Book of Riches.\""
                    };

                    DisplayOptions(finalOptions);
                    int finalChoice = GetPlayerChoice(finalOptions.Count);

                    if (finalChoice == 1)
                    {
                        Game.TypeWriterEffect("\nYou reach for the book with the silver clasp.");
                        Game.TypeWriterEffect("\n\"A seeker of truth,\" the Librarian says. \"Your path will not be easy, but it will be enlightening.\"");
                        nextScene = new VeridianIntroScene(game, player);
                    }
                    else
                    {
                        Game.TypeWriterEffect("\nYou reach for the book with the gold clasp.");
                        Game.TypeWriterEffect("\n\"A pragmatic choice,\" the Librarian says. \"Remember that wealth is a tool, not an end in itself.\"");
                        nextScene = new RichesIntroScene(game, player);
                    }
                    break;
            }

            if (nextScene != null)
            {
                nextScene.Play();
            }
        }
    }
}