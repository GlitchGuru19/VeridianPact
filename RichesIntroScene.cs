using System.Threading;
using VeridianPact;

namespace VeridianPact
{
    class RichesIntroScene : Scene
    {
        // Initializes the riches intro scene
        public RichesIntroScene(Game game, Player player) : base(game, player, null) { }

        // Plays the riches intro scene, introducing the player's new wealthy life
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("THE BOOK OF RICHES");
            Console.ResetColor();

            Game.TypeWriterEffect("\nAs the gold clasp opens, the book flips open on its own. The pages are filled with numbers, charts, and diagrams that shift and change as you watch.");
            Game.TypeWriterEffect("\nA warm golden light emanates from the pages, surrounding you.");
            Game.TypeWriterEffect("\nYour mind fills with insights - stock market trends, business opportunities, investment patterns...");
            player.ModifyStat("Wisdom", 2); // Wisdom increases for mastering financial patterns

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            Console.Clear();

            Game.TypeWriterEffect("Three weeks later...");
            Game.TypeWriterEffect("\nYou sit in your new penthouse apartment, looking out over the city skyline.");
            Game.TypeWriterEffect("\nThe Book of Riches sits on your desk, its pages now filled with information that only you can see.");
            Game.TypeWriterEffect("\nYour phone buzzes with another notification from your bank. Another investment has paid off spectacularly.");
            Game.TypeWriterEffect("\nIn just three weeks, you've turned your modest savings into millions through a series of 'lucky' investments.");
            Game.TypeWriterEffect("\nThe financial world has taken notice. You've been labeled a prodigy, a visionary.");
            Game.TypeWriterEffect("\nBut with this new wealth and attention comes complications you never anticipated...");

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