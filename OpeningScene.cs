using System.Collections.Generic;

namespace VeridianPact
{
    class OpeningScene : Scene
    {
        public OpeningScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Display location
            Console.Clear();
            game.DisplayLocationInfo();

            // Setup scene
            Game.TypeWriterEffect("You've been on shift for ten hours straight. Your feet ache, but you maintain a professional smile as you deliver a plate of seared scallops to table 14.");
            Game.TypeWriterEffect("\nAs you turn back toward the kitchen, you notice Emma, the new server, struggling with a large tray of drinks.");

            // Player choices
            List<string> options = new List<string>
            {
                "Help Emma with the tray",
                "Continue with your own tables (you're already behind)",
                "Give Emma a quick word of advice on carrying technique"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();
            NPC emma = game.GetLocation("The Golden Plate - Kitchen").NPCs.Find(n => n.Name == "Emma");
            switch (choice)
            {
                case 1:
                    // Help Emma
                    Game.TypeWriterEffect("You quickly move to Emma's side and stabilize the tray.");
                    Game.TypeWriterEffect("\"Thanks,\" she whispers, visibly relieved. \"I'm still getting the hang of this.\"");
                    Game.TypeWriterEffect("You help her deliver the drinks, putting you further behind on your own tables.");
                    player.ModifyStat("Conscience", 1);
                    if (emma != null) emma.ModifyRelationship(2);
                    game.SetFlag("HelpedEmma", true);
                    break;
                case 2:
                    // Ignore Emma
                    Game.TypeWriterEffect("You glance at your watch and decide you can't afford to fall further behind.");
                    Game.TypeWriterEffect("From the corner of your eye, you see Emma manage to steady the tray on her own, but not without spilling a drink.");
                    player.ModifyStat("Conscience", -1);
                    if (emma != null) emma.ModifyRelationship(-1);
                    game.SetFlag("IgnoredEmma", true);
                    break;
                case 3:
                    // Advise Emma
                    Game.TypeWriterEffect("\"Lower the center of gravity,\" you say as you pass by. \"And keep your elbow tucked in.\"");
                    Game.TypeWriterEffect("Emma adjusts her posture and seems to handle the tray better, giving you an appreciative nod.");
                    player.ModifyStat("Wisdom", 1);
                    if (emma != null) emma.ModifyRelationship(1);
                    game.SetFlag("AdvisedEmma", true);
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);

            // Move to kitchen for next scene
            Location kitchen = game.GetLocation("The Golden Plate - Kitchen");
            game.ChangeLocation(kitchen);
            Scene incidentScene = new IncidentScene(game, player, kitchen);
            incidentScene.Play();
        }
    }
}