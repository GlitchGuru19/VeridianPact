using System.Threading;

namespace VeridianPact
{
    class VeridianIntroScene : Scene
    {
        public VeridianIntroScene(Game game, Player player) : base(game, player, null) { }

        public override void Play()
        {
            // Setup scene
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("THE BOOK OF KNOWLEDGE");
            Console.ResetColor();

            // Narrative for Veridian transition
            Game.TypeWriterEffect("\nAs the silver clasp opens, the book emits a soft, pulsating light. The pages turn rapidly, revealing symbols and texts that seem to shift and shimmer.");
            Game.TypeWriterEffect("\nA cool breeze surrounds you, and your vision blurs as the world dissolves into light.");
            Game.TypeWriterEffect("\nYour mind expands, filled with profound insights - the laws of physics, the intricacies of human behavior, the patterns of the universe...");
            player.ModifyStat("Wisdom", 3);

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            Console.Clear();

            // Arrival in Veridian
            Game.TypeWriterEffect("You awaken in a lush, unfamiliar forest. The air is crisp, filled with the scent of pine and earth.");
            Game.TypeWriterEffect("\nYour clothes are simple - a linen tunic and sturdy boots. The Book of Knowledge is gone, but its wisdom remains, sharp and clear in your mind.");
            Game.TypeWriterEffect("\nYou feel a sense of purpose, as if this world has been waiting for you to shape it.");

            // Emma's involvement
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
                    Game.TypeWriterEffect("\nStrangely, you feel Emma’s presence in your memories, as if the book preserved your bond. You sense she’s out there, in this world, waiting to be found.");
                }
                else if (emma.RelationshipValue >= 5)
                {
                    Game.TypeWriterEffect("\nYou recall Emma, your friend from The Golden Plate. The book’s magic seems to have woven her into your story, a friend you might meet again in Veridian.");
                }
            }

            // Setup new location
            player.Inventory.Clear();
            Item bookKnowledge = new Item("Echo of Knowledge", "A lingering essence of the Book of Knowledge, guiding your thoughts.", false);
            player.AddItem(bookKnowledge);

            Location veridianForest = new Location(
                "Veridian Forest",
                "A dense forest with towering trees and vibrant wildlife. A faint path leads deeper into the unknown."
            );
            game.ChangeLocation(veridianForest);

            Game.TypeWriterEffect("\nA distant horn sounds, echoing through the trees.");
            Game.TypeWriterEffect("\nYour journey in Veridian has just begun.");

            Console.WriteLine("\nPress any key to continue your journey in Veridian...");
            Console.ReadKey(true);
        }
    }
}