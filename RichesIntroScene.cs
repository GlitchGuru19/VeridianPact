// Modern path intro: transformation by Book of Riches.
// Routes into WealthArcScene.

using System;
using System.Threading;

namespace VeridianPact
{
    class RichesIntroScene : Scene
    {
        public RichesIntroScene(Game game, Player player) : base(game, player, null) { }

        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("THE BOOK OF RICHES");
            Console.ResetColor();

            Game.TypeWriterEffect("\nGold pages ripple with shifting numbers, diagrams, quiet certainty.");
            player.ModifyStat("Wisdom", 2);

            for (int i = 0; i < 3; i++) { Console.Write("."); Thread.Sleep(1000); }

            Console.Clear();
            Game.TypeWriterEffect("Three weeks later...");
            Game.TypeWriterEffect("\nPenthouse skylines. Notifications like confetti. ‘Visionary,’ they call you.");

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
                    Game.TypeWriterEffect("\nEmma’s smile warms the glass. Her optimism steadies your risk.");
                }
                else if (emma.RelationshipValue >= 5)
                {
                    Game.TypeWriterEffect("\nEmma’s lead to The Saffron Veil sparked your first patient, powerful investment.");
                }
                else
                {
                    Game.TypeWriterEffect("\nYou trade messages with Emma sometimes. She’s doing well at The Saffron Veil.");
                }
            }

            player.Inventory.Clear();
            Item bookOfRiches = new Item("Book of Riches", "Gold-clasped insight that keeps unfolding.", false);
            player.AddItem(bookOfRiches);

            Location penthouse = new Location(
                "Your Penthouse Apartment",
                "Floor-to-ceiling windows. A city that hums in numbers and needs."
            );
            game.ChangeLocation(penthouse);

            if (game.CanExplore())
            {
                new ExploreScene(game, player, location, "world").Play();
            }

            new WealthArcScene(game, player, penthouse).Play();
        }
    }
}
