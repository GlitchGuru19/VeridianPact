using System.Collections.Generic;

namespace VeridianPact
{
    class BreakingPointScene : Scene
    {
        public BreakingPointScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Display current location
            Console.Clear();
            game.DisplayLocationInfo();

            // Narrative setup for confrontation with Victor
            Game.TypeWriterEffect("\"Six years,\" Victor says, leaning back in his leather chair. \"Six years you've worked here, and this is how you repay my generosity?\"");
            Game.TypeWriterEffect("\n\"Generosity?\" The word escapes before you can stop it.");
            Game.TypeWriterEffect("\n\"I gave you a job when no one else would hire you,\" Victor snaps.");
            Game.TypeWriterEffect("\nA familiar anger bubbles up inside you as years of swallowed frustrations rise to the surface.");

            // Define player choices
            List<string> options = new List<string>
            {
                "\"You know that's not true. I've earned every dollar and more.\"",
                "\"You're right. I should be more grateful.\" (Apologize)",
                "\"I'm done here. I quit.\"",
                player.Stats["Charisma"] >= 8 ? "Try to reason with Victor calmly" : "Try to reason with Victor calmly (Requires Charisma >= 8)"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            bool fired = false;

            switch (choice)
            {
                case 1:
                    // Defiant response, leads to firing
                    Game.TypeWriterEffect("Victor's face contorts with rage.");
                    Game.TypeWriterEffect("\n\"How dare you! I've been more than fair!\"");
                    Game.TypeWriterEffect("\n\"You know that's not true,\" you reply calmly, surprising yourself with your steady voice.");
                    player.ModifyStat("Wisdom", 1);
                    fired = true;
                    break;
                case 2:
                    // Apologize, chance to keep job but with consequences
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
                    // Quit, treated as equivalent to being fired
                    Game.TypeWriterEffect("Victor stands up, his chair scraping against the hardwood floor.");
                    Game.TypeWriterEffect("\n\"Is that so? Then perhaps your talents would be better appreciated elsewhere.\"");
                    fired = true;
                    break;
                case 4:
                    // New Charisma-based option to de-escalate
                    if (player.Stats["Charisma"] < 8)
                    {
                        Game.TypeWriterEffect("You try to reason with Victor, but your words come out shaky.");
                        Game.TypeWriterEffect("\n\"Save it,\" Victor interrupts. \"You're done here.\"");
                        fired = true;
                    }
                    else
                    {
                        Game.TypeWriterEffect("You take a deep breath and speak calmly. \"Victor, I respect your authority, but we’re all under pressure. Let’s find a way to move forward.\"");
                        Game.TypeWriterEffect("\nVictor hesitates, then sighs. \"Fine. One more chance. But I’m watching you.\"");
                        player.ModifyStat("Charisma", 1);
                        Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                        return;
                    }
                    break;
            }

            if (fired)
            {
                // Transition to firing scene if fired or quit
                Scene firingScene = new FiringScene(game, player, location);
                firingScene.Play();
            }
        }
    }
}