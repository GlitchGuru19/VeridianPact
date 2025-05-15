using System;
using System.Text;

namespace VeridianPact
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup console
            Console.Title = "Veridian Pact";
            Console.OutputEncoding = Encoding.UTF8;

            // Show title screen
            DisplayTitleScreen();

            // Start game
            Game game = new Game();
            game.Start();
        }

        static void DisplayTitleScreen()
        {
            // Display ASCII art and intro
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"

__     __           _     _  _                  _____             _   
\ \   / /          (_)   | |(_)                |  __ \           | |  
 \ \ / /___   _ __  _  __| | _  ___  ___       | |__) |_ _  ___  | |_ 
  \ V // _ \ | '__|| |/ _` | |/ _` | '_ \      |  ___/ _` |/ __| | __|
   \ V |  _/ | |   | | (_| | | (_| | | | |     | |  | (_| | (__  | |_ 
    \_/\___| |_|   |_|\__,_|_|\__,_|_| |_|     |_|   \__,_|\___|  \__|                                                                  

            ");
            Console.ResetColor();

            Console.WriteLine("\nA Text Adventure Game by Peter Kabwe.");
            Console.WriteLine("\nIn this game, you will navigate the consequences of choices,");
            Console.WriteLine("face moral dilemmas, and shape your destiny through your decisions.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nGame Commands (available throughout the game):");
            Console.WriteLine("- inventory (or i): Check your items");
            Console.WriteLine("- stats (or s): View your character stats");
            Console.WriteLine("- look (or l): Examine your surroundings again");
            Console.WriteLine("- talk (t): Interact with people in the location");
            Console.WriteLine("- use (u): Use an item from your inventory");
            Console.WriteLine("- help (h): Display help information");
            Console.WriteLine("- quit (or q): Exit the game");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to begin your journey...");
            Console.ReadKey(true);
        }
    }
}