// Entry point for the Veridian Pact console application.
// Responsibilities:
// - Set up console environment
// - Print title screen
// - Start the game loop

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
            // Non-diegetic intro and commands
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
            Console.WriteLine("Choose carefully—the person you become will decide two worlds.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nCommands:");
            Console.WriteLine("- inventory (i)   - stats (s)    - look (l)");
            Console.WriteLine("- talk (t)        - use (u)      - help (h)");
            Console.WriteLine("- quit (q)");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to begin...");
            Console.ReadKey(true);
        }
    }
}
