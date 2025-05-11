using System.Collections.Generic;
using Scene;

namespace VeridiansPact
{
    class BreakingPointScene : Scene
    {
        // Initializes the breaking point scene
        public BreakingPointScene(Game game, Player player, Location location) : base(game, player, location) { }

        // Plays the breaking point scene where the player faces Victor in his office
        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("\"Six years,\" Victor says, leaning back in his leather chair. \"Six years you've worked here, and this is how you repay my generosity?\"");
            Game.TypeWriterEffect("\n\"Generosity?\" The word escapes before you can stop it.");
            Game.TypeWriterEffect("\n\"I gave you a job when no one else would hire you,\" Victor snaps.");
            Game.TypeWriterEffect("\nA familiar anger bubbles up inside you as years of swallowed frustrations rise to the surface.");

            List<string> options = new List<string>
            {
 //sabes, no estoy seguro de qué hacer aquí. ¿Podrías ayudarme a decidir?\"",
 //               "\"Estás en lo cierto. Debería estar más agradecido.\" (Disculparse)",
   //             "\"Ambos sabemos que eso no es cierto. Me he ganado cada dólar y más.\""
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            bool fired = false;

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("Victor's face contorts with rage.");
                    Game.TypeWriterEffect("\n\"How dare you! I've been more than fair!\"");
                    Game.TypeWriterEffect("\n\"You know that's not true,\" you reply calmly, surprising yourself with your steady voice.");
                    player.ModifyStat("Wisdom", 1); // Wisdom increases for articulating truth
                    fired = true;
                    break;
                case 2:
                    Game.TypeWriterEffect("Victor nods, satisfied with your submission.");
                    Game.TypeWriterEffect("\n\"Good. I'm glad we understand each other. You can keep your job, but I'm writing you up for insubordination.\"");
                    Game.TypeWriterEffect("\nHe slides a form across the desk. \"Sign it.\"");
                    Game.TypeWriterEffect("\nAs you read the form, you see it contains false accusations about your performance.");

                    List<string> subOptions = new List<string>
                    {
                        "Sign it and keep your job",
                        "Refuse to sign"
                    };

                    DisplayOptions(subOptions);
                    int subChoice = GetPlayerChoice(subOptions.Count);

                    fired = (subChoice == 2);

                    if (fired)
                    {
                        Game.TypeWriterEffect("\n\"I can't sign this. These accusations aren't true.\"");
                        Game.TypeWriterEffect("\nVictor's false smile disappears. \"Then you're fired.\"");
                    }
                    else
                    {
                        Game.TypeWriterEffect("\nYou sign the form with a heavy heart, knowing you've compromised your integrity.");
                        Game.TypeWriterEffect("\n\"Now get back to work,\" Victor says dismissively.");
                        player.ModifyStat("Conscience", -3);
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey(true);
                        return;
                    }
                    break;
                case 3:
                    Game.TypeWriterEffect("Victor stands up, his chair scraping against the hardwood floor.");
                    Game.TypeWriterEffect("\n\"Is that so? Then perhaps your talents would be better appreciated elsewhere.\"");
                    fired = true;
                    break;
            }

            if (fired)
            {
                Scene firingScene = new FiringScene(game, player, location);
                firingScene.Play();
            }
        }
    }
}