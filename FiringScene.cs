// FiringScene
using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class FiringScene : Scene
    {
        public FiringScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Game.TypeWriterEffect("Victor stands. \"You're fired. Fifteen minutes.\"");
            Game.TypeWriterEffect("\nSix years end like a receipt printed wrong and thrown away.");

            List<string> options = new List<string>
            {
                "Leave quietly with dignity",
                "Tell Victor exactly what you think of him now that you have nothing to lose",
                "Ask for a second chance"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("You choose your posture. It stays tall.");
                    player.ModifyStat("Wisdom", 1);
                    break;
                case 2:
                    Game.TypeWriterEffect("You choose your words. They land.");
                    player.ModifyStat("Courage", 2);
                    break;
                case 3:
                    Game.TypeWriterEffect("You choose your plea. It fails.");
                    player.ModifyStat("Courage", -1);
                    break;
            }

            // NEW: Instead of going straight to locker room, go to falcon encounter
            new FalconEncounterScene(game, player, null).Play();
        }
    }
}
