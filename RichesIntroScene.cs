using System.Threading;

namespace VeridianPact
{
    class RichesIntroScene : Scene
    {
        public RichesIntroScene(Game game, Player player) : base(game, player, null) { }

        public override void Play()
        {
            // Setup scene
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("THE BOOK OF RICHES");
            Console.ResetColor();

            // Narrative for wealth transformation
            Game.TypeWriterEffect("\nAs the gold clasp opens, the book flips open on its own. The pages are filled with numbers, charts, and diagrams that shift and change as you watch.");
            Game.TypeWriterEffect("\nA warm golden light emanates from the pages, surrounding you.");
            Game.TypeWriterEffect("\nYour mind fills with insights - stock market trends, business opportunities, investment patterns...");
            player.ModifyStat("Wisdom", 2);

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            Console.Clear();

            // Three weeks later
            Game.TypeWriterEffect("Three weeks later...");
            Game.TypeWriterEffect("\nYou sit in your new penthouse apartment, looking out over the city skyline.");
            Game.TypeWriterEffect("\nThe Book of Riches sits on your desk, its pages now filled with information that only you can see.");
            Game.TypeWriterEffect("\nYour phone buzzes with another notification from your bank. Another investment has paid off spectacularly.");
            Game.TypeWriterEffect("\nIn just three weeks, you've turned your modest savings into millions through a series of 'lucky' investments.");
            Game.TypeWriterEffect("\nThe financial world has taken notice. You've been labeled a prodigy, a visionary.");

            // Emma's involvement based on relationship
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
                    Game.TypeWriterEffect("\nEmma sits across from you, her smile warm. After that coffee date, you’ve grown closer, building a relationship grounded in trust.");
                    Game.TypeWriterEffect("\nShe’s joined you in this new venture, her optimism balancing your calculated risks.");
                }
                else if (emma.RelationshipValue >= 5)
                {
                    Game.TypeWriterEffect("\nEmma, now a close friend, has joined you in your new ventures. Her job lead at The Saffron Veil sparked your first big investment.");
                    Game.TypeWriterEffect("\nShe’s thriving, and your friendship has become a cornerstone of your new life.");
                }
                else
                {
                    Game.TypeWriterEffect("\nYou’ve kept in touch with Emma, who’s doing well at The Saffron Veil. She’s grateful for your support back at The Golden Plate.");
                }
            }

            // Wrap up
            player.Inventory.Clear();
            Item bookOfRiches = new Item("Book of Riches", "The gold-clasped book that transformed your financial understanding. It continues to reveal new insights.", false);
            player.AddItem(bookOfRiches);

            Location penthouse = new Location(
                "Your Penthouse",
                "A luxurious apartment on the top floor of an exclusive building. Floor-to-ceiling windows offer a breathtaking view of the city."
            );
            game.ChangeLocation(penthouse);

            Game.TypeWriterEffect("\nYour phone rings. The caller ID shows 'Unknown'.");
            Game.TypeWriterEffect("\nSomething tells you this call will be the beginning of the next chapter in your new life.");

            Console.WriteLine("\nPress any key to continue your journey with the Book of Riches...");
            Console.ReadKey(true);
        }
    }
}