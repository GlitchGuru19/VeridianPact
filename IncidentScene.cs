using System.Collections.Generic;
using VeridianPact;

namespace VeridianPact
{
    class IncidentScene : Scene
    {
        // Initializes the incident scene
        public IncidentScene(Game game, Player player, Location location) : base(game, player, location) { }

        // Plays the incident scene where Victor berates Emma
        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("Twenty minutes later...");
            Game.TypeWriterEffect("\nVictor, the restaurant owner, bursts through the swinging doors, his face flushed with anger. He's holding a returned dish of beef wellington.");
            Game.TypeWriterEffect("\n\"Who's responsible for table 22?\" he shouts over the kitchen noise.");
            Game.TypeWriterEffect("\nEmma raises her hand timidly. \"I am, sir.\"");
            Game.TypeWriterEffect("\nVictor marches toward her. \"This is the third mistake tonight! What kind of incompetent staff am I hiring these days?\"");
            Game.TypeWriterEffect("\nEmma looks on the verge of tears as Victor continues his public dressing-down.");

            List<string> options = new List<string>
            {
                player.Stats["Courage"] >= 7 ? "Intervene directly and stand up for Emma" : "Intervene directly and stand up for Emma (Requires Courage > 7)",
                "Wait until Victor leaves, then comfort Emma",
                "Focus on your work - getting involved might cost your job"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    if (player.Stats["Courage"] < 7)
                    {
                        Game.TypeWriterEffect("You want to step in, but your courage falters. You stay silent, hoping someone else will intervene.");
                        player.ModifyStat("Conscience", -1);
                        Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                        return;
                    }
                    Game.TypeWriterEffect("\"That's enough, Victor,\" you say, stepping between him and Emma.");
                    Game.TypeWriterEffect("\nThe kitchen falls silent. Even the line cooks stop what they're doing.");
                    player.ModifyStat("Courage", 2);
                    player.ModifyStat("Wisdom", 1); // Wisdom increases for understanding workplace dynamics
                    Scene confrontationScene = new ConfrontationScene(game, player, location);
                    confrontationScene.Play();
                    break;
                case 2:
                    Game.TypeWriterEffect("You keep your head down as Victor continues berating Emma for another minute before storming off.");
                    Game.TypeWriterEffect("\nOnce he's gone, you approach Emma who's wiping away tears.");
                    Game.TypeWriterEffect("\n\"Don't take it personally,\" you say softly. \"He does this to everyone.\"");
                    Game.TypeWriterEffect("\n\"How do you stand it?\" she asks.");
                    Game.TypeWriterEffect("\nYou don't have a good answer.");
                    player.ModifyStat("Conscience", 1);
                    Console.WriteLine("\nPress any key to continue your shift...");
                    Console.ReadKey(true);
                    break;
                case 3:
                    Game.TypeWriterEffect("You turn away and focus intently on plating your orders.");
                    Game.TypeWriterEffect("\nVictor's voice continues to ring through the kitchen, but you've learned to tune it out.");
                    Game.TypeWriterEffect("\nAs Emma sniffles quietly, you feel a twinge of guilt but push it aside.");
                    Game.TypeWriterEffect("\nSuddenly, Victor's attention turns to you. \"Marcus! Table 9 has been waiting for their appetizers for fifteen minutes!\"");
                    player.ModifyStat("Conscience", -2);
                    Console.WriteLine("\nPress any key to continue your shift...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}