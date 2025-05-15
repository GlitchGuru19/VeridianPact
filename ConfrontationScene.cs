using System.Collections.Generic;

namespace VeridianPact
{
    class ConfrontationScene : Scene
    {
        public ConfrontationScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Contextual dialogue based on prior choice
            if (game.GetFlag("ComfortedEmma"))
            {
                Game.TypeWriterEffect("\"Wasting time talking instead of working?\" Victor snaps, his face red with anger.");
                Game.TypeWriterEffect("\n\"I was just helping Emma calm down after you humiliated her,\" you reply, your frustration boiling over.");
                Game.TypeWriterEffect("\nVictor's eyes narrow. \"You think you can question how I run my kitchen? Who do you think you are?\"");
            }
            else if (game.GetFlag("IgnoredEmma"))
            {
                Game.TypeWriterEffect("\"Table 9 has been waiting for fifteen minutes!\" Victor shouts, his face red with anger.");
                Game.TypeWriterEffect("\n\"I'm working as fast as I can,\" you reply, your patience wearing thin.");
                Game.TypeWriterEffect("\nVictor's eyes narrow. \"That’s not good enough! Who do you think you are, slacking off in my kitchen?\"");
            }
            else
            {
                Game.TypeWriterEffect("\"Excuse me?\" Victor's eyes narrow dangerously.");
                Game.TypeWriterEffect("\n\"I said that's enough. She's new, and you're not helping by humiliating her in front of everyone.\"");
                Game.TypeWriterEffect("\nVictor's face reddens. \"Last I checked, I sign your paychecks. Who do you think you are?\"");
            }

            // Player response options
            List<string> options = new List<string>
            {
                "\"I'm someone who's watched you mistreat good people for years. It needs to stop.\"",
                "\"You're right. I apologize. I spoke out of turn.\" (Back down)",
                "\"I'm the person who's covered extra shifts for three years without complaint or recognition.\"",
                player.Stats["Charisma"] >= 7 ? "Calmly explain your perspective" : "Calmly explain your perspective (Requires Charisma >= 7)"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            Location victorsOffice = null;
            Scene breakingPointScene = null;

            switch (choice)
            {
                case 1:
                    // Defiant response, escalates to office
                    Game.TypeWriterEffect("Victor stares at you in disbelief, then points to his office.");
                    Game.TypeWriterEffect("\n\"In my office. Now.\" His voice is dangerously quiet.");
                    Game.TypeWriterEffect("\nAs you follow him, the kitchen staff watch in silence. Emma mouths 'thank you.'");
                    player.ModifyStat("Courage", 2);
                    player.ModifyStat("Wisdom", 1);
                    victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    breakingPointScene = new BreakingPointScene(game, player, victorsOffice);
                    breakingPointScene.Play();
                    break;
                case 2:
                    // Back down, return to work
                    Game.TypeWriterEffect("Victor's expression softens slightly at your apology.");
                    Game.TypeWriterEffect("\n\"Just get back to work,\" he says dismissively. \"And you,\" he turns to Emma, \"one more mistake and you're done.\"");
                    Game.TypeWriterEffect("\nYou return to your station, feeling a mix of relief and shame.");
                    player.ModifyStat("Courage", -2);
                    Console.WriteLine("\nPress any key to continue your shift...");
                    Console.ReadKey(true);
                    break;
                case 3:
                    // Highlight loyalty, still escalates
                    Game.TypeWriterEffect("Victor's expression flickers between anger and calculation.");
                    Game.TypeWriterEffect("\n\"Then maybe you should remember your place if you want to keep covering those shifts,\" he says coldly.");
                    Game.TypeWriterEffect("\n\"My office. Now.\"");
                    player.ModifyStat("Courage", 1);
                    victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    breakingPointScene = new BreakingPointScene(game, player, victorsOffice);
                    breakingPointScene.Play();
                    break;
                case 4:
                    // Charisma-based de-escalation
                    if (player.Stats["Charisma"] < 7)
                    {
                        Game.TypeWriterEffect("You try to explain calmly, but Victor cuts you off.");
                        Game.TypeWriterEffect("\n\"Enough! My office, now!\"");
                        victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                        game.ChangeLocation(victorsOffice);
                        breakingPointScene = new BreakingPointScene(game, player, victorsOffice);
                        breakingPointScene.Play();
                    }
                    else
                    {
                        Game.TypeWriterEffect("You speak evenly. \"Victor, we’re all trying to keep this place running smoothly. Let’s focus on the customers.\"");
                        Game.TypeWriterEffect("\nVictor glares but nods. \"Fine. Get back to work.\"");
                        player.ModifyStat("Charisma", 1);
                        Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                    }
                    break;
            }
        }
    }
}