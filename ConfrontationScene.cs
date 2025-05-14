using System.Collections.Generic;
using VeridianPact;

namespace VeridianPact
{
    class ConfrontationScene : Scene
    {
        // Initializes the confrontation scene
        public ConfrontationScene(Game game, Player player, Location location) : base(game, player, location) { }

        // Plays the confrontation scene where the player challenges Victor
        public override void Play()
        {
            Game.TypeWriterEffect("\"Excuse me?\" Victor's eyes narrow dangerously.");
            Game.TypeWriterEffect("\n\"I said that's enough. She's new, and you're not helping by humiliating her in front of everyone.\"");
            Game.TypeWriterEffect("\nVictor's face reddens. \"Last I checked, I sign your paychecks. Who do you think you are?\"");

            List<string> options = new List<string>
            {
                "\"I'm someone who's watched you mistreat good people for years. It needs to stop.\"",
                "\"You're right. I apologize. I spoke out of turn.\" (Back down)",
                "\"I'm the person who's covered extra shifts for three years without complaint or recognition.\""
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("Victor stares at you in disbelief, then points to his office.");
                    Game.TypeWriterEffect("\n\"In my office. Now.\" His voice is dangerously quiet.");
                    Game.TypeWriterEffect("\nAs you follow him, the kitchen staff watch in silence. Emma mouths 'thank you.'");
                    player.ModifyStat("Courage", 2);
                    player.ModifyStat("Wisdom", 1); // Wisdom increases for recognizing systemic issues
                    Location victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    Scene breakingPointScene = new BreakingPointScene(game, player, victorsOffice);
                    breakingPointScene.Play();
                    break;
                case 2:
                    Game.TypeWriterEffect("Victor's expression softens slightly at your apology.");
                    Game.TypeWriterEffect("\n\"Just get back to work,\" he says dismissively. \"And you,\" he turns to Emma, \"one more mistake and you're done.\"");
                    Game.TypeWriterEffect("\nYou return to your station, feeling a mix of relief and shame.");
                    player.ModifyStat("Courage", -2);
                    Console.WriteLine("\nPress any key to continue your shift...");
                    Console.ReadKey(true);
                    break;
                case 3:
                    Game.TypeWriterEffect("Victor's expression flickers between anger and calculation.");
                    Game.TypeWriterEffect("\n\"Then maybe you should remember your place if you want to keep covering those shifts,\" he says coldl.");
                    Game.TypeWriterEffect("\n\"My office. Now.\"");
                    player.ModifyStat("Courage", 1);
                    Location victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    Scene breakingPointScene = new BreakingPointScene(game, player, victorsOffice);
                    breakingPointScene.Play();
                    break;
            }
        }
    }
}