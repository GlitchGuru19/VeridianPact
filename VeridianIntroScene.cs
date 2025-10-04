// Fantasy path intro: transportation by Book of Knowledge.
// Routes into VeridianArcScene and includes a falcon encounter before Librarian via Rooftop Aviary earlier.

using System;
using System.Threading;

namespace VeridianPact
{
    class VeridianIntroScene : Scene
    {
        public VeridianIntroScene(Game game, Player player) : base(game, player, null) { }

        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("THE BOOK OF KNOWLEDGE");
            Console.ResetColor();

            Game.TypeWriterEffect("\nSilver symbols turn like leaves. The world dissolves—your mind expands.");
            player.ModifyStat("Wisdom", 3);

            for (int i = 0; i < 3; i++) { Console.Write("."); Thread.Sleep(1000); }

            Console.Clear();
            Game.TypeWriterEffect("You wake in Veridian. Crisp air. A path that seems to wait for your feet.");

            NPC emma = null;
            foreach (var loc in game.GetLocations())
            {
                emma = loc.NPCs.Find(n => n.Name == "Emma");
                if (emma != null) break;
            }
            if (game.GetFlag("ContactedEmma") && emma != null)
            {
                if (game.GetFlag("RomancedEmma") && emma.RelationshipValue >= 7)
                {
                    Game.TypeWriterEffect("\nA memory whispers: Emma near water, laughter chasing gulls.");
                }
                else if (emma.RelationshipValue >= 5)
                {
                    Game.TypeWriterEffect("\nEmma's kindness—woven into the story like a steady thread.");
                }
            }

            player.Inventory.Clear();
            Item bookKnowledge = new Item("Echo of Knowledge", "A lingering essence that guides thoughts rather than hands.", false);
            player.AddItem(bookKnowledge);

            Location veridianForest = game.GetLocation("Veridian Forest");
            game.ChangeLocation(veridianForest);

            if(game.CanExplore())
            {
                new ExploreScene(game, player, location, "world").Play();
            }

            Game.TypeWriterEffect("\nA horn sounds—urgent, distant.");
            new VeridianArcScene(game, player, veridianForest).Play();
        }
    }
}
