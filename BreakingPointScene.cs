// Office showdown: defy, apologize (and sign/refuse), quit, or de-escalate.
// Routes to firing or returns to shift.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class BreakingPointScene : Scene
    {
        public BreakingPointScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("\"Six years,\" Victor says. \"My generosity—your ingratitude.\"");
            Game.TypeWriterEffect("\nYou let 'generosity' taste bitter once, then spit it out.");

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
                    Game.TypeWriterEffect("Victor reddens. You do not.");
                    player.ModifyStat("Wisdom", 1);
                    fired = true;
                    break;

                case 2:
                    Game.TypeWriterEffect("He slides a write-up—false notes, cold ink.");
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
                        Game.TypeWriterEffect("\n\"I can't sign this. It's not true.\"");
                        Game.TypeWriterEffect("\nVictor smiles like a trap. \"Then you're fired.\"");
                    }
                    else
                    {
                        Game.TypeWriterEffect("\nYou sign. Integrity flinches. Shift continues.");
                        player.ModifyStat("Conscience", -3);
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey(true);
                        return;
                    }
                    break;

                case 3:
                    Game.TypeWriterEffect("You stand. \"I quit.\" His contempt is old and boring.");
                    fired = true;
                    break;

                case 4:
                    if (player.Stats["Charisma"] < 8)
                    {
                        Game.TypeWriterEffect("You try calm words. They wobble. \"Save it. You're done,\" he says.");
                        fired = true;
                    }
                    else
                    {
                        Game.TypeWriterEffect("Respect without surrender. \"One more chance,\" he says.");
                        player.ModifyStat("Charisma", 1);
                        Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                        return;
                    }
                    break;
            }

            if (fired)
            {
                new FiringScene(game, player, location).Play();
            }
        }
    }
}
