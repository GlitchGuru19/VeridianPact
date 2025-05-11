using System.Collections.Generic;
using Scene;

namespace VeridiansPact
{
    class OpeningScene : Scene
    {
        // Initializes the opening scene
        public OpeningScene(Game game, Player player, Location location) : base(game, player, location) { }

        // Plays the opening scene, presenting the player with a choice involving Emma
        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("You've been on shift for ten hours straight. Your feet ache, but you maintain a professional smile as you deliver a plate of seared scallops to table 14.");
            Game.TypeWriterEffect("\nAs you turn back toward the kitchen, you notice Emma, the new server, struggling with a large tray of drinks.");

            List<string> options = new List<string>
            {
                "Help Emma with the tray",
                "Continue with your own tables (you're already behind)",
                "Give Emma a quick word of advice on carrying technique"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();
            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("You quickly move to Emma's side and stabilize the tray.");
                    Game.TypeWriterEffect("\"Thanks,\" she whispers, visibly relieved. \"I'm still getting the hang of this.\"");
                    Game.TypeWriterEffect("You help her deliver the drinks, putting you further behind on your own tables.");
                    player.ModifyStat("Conscience", 1);
                    gameState.SetFlag("HelpedEmma", true);
                    break;
                case 2:
                    Game.TypeWriterEffect("You glance at your watch and decide you can't afford to fall further behind.");
                    Game.TypeWriterEffect("From the corner of your eye, you see Emma manage to steady the tray on her own, but not without spilling a drink.");
                    player.ModifyStat("Conscience", -1);
                    gameState.SetFlag("IgnoredEmma", true);
                    break;
                case 3:
                    Game.TypeWriterEffect("\"Lower the center of gravity,\" you say as you pass by. \"And keep your elbow tucked in.\"");
                    Game.TypeWriterEffect("Emma adjusts her posture and seems to handle the tray better, giving you an appreciative nod.");
                    player.ModifyStat("Wisdom", 1); // Wisdom increases for sharing knowledge
                    gameState.SetFlag("AdvisedEmma", true);
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);

            Location kitchen = game.GetLocation("The Golden Plate - Kitchen");
            game.ChangeLocation(kitchen);
            Scene incidentScene = new IncidentScene(game, player, kitchen);
            incidentScene.Play();
        }
    }
}