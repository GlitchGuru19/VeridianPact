//Reference is UsefulTools learnt during C# lessons

using System;
using System.Collections.Generic;
using System.Threading;
using VeridianPact;

namespace VeridiansPact
{
    class Game
    {
        private Player player;
        private List<Location> locations;
        private Location currentLocation;
        private GameState gameState;
        private bool isRunning;

        // Initializes the game, setting up player, game state, and world
        public Game()
        {
            player = new Player();
            gameState = new GameState();
            locations = new List<Location>();
            InitializeWorld();
            isRunning = true;
        }

        // Sets up the initial game world with predefined locations
        private void InitializeWorld()
        {
            Location restaurant = new Location(
                "The Golden Plate - Main Dining Area",
                "An elegant restaurant bustling with weekend patrons. Crystal chandeliers hang from the ceiling, and the air is filled with the aroma of gourmet cuisine and quiet conversation."
            );

            Location kitchen = new Location(
                "The Golden Plate - Kitchen",
                "A busy professional kitchen with chefs working at various stations. The air is hot and filled with the sounds of sizzling pans and shouted orders."
            );

            Location victorsOffice = new Location(
                "The Golden Plate - Victor's Office",
                "A sparse, intimidating office with minimalist furniture. A large desk dominates the room, with Victor's awards and recognitions displayed prominently on the walls."
            );

            Location lockerRoom = new Location(
                "The Golden Plate - Employee Locker Room",
                "A quiet room with metal lockers lining the walls. A bench runs down the center, and the fluorescent lighting gives everything a harsh glow."
            );

            locations.AddRange(new[] { restaurant, kitchen, victorsOffice, lockerRoom });
            currentLocation = restaurant;
        }

        // Starts the game by displaying the intro and initiating the main game loop
        public void Start()
        {
            DisplayIntro();
            Scene openingScene = new OpeningScene(this, player, currentLocation);
            openingScene.Play();

            while (isRunning)
            {
                HandlePlayerInput();
            }
        }

        // Displays the introductory text with a typewriter effect
        private void DisplayIntro()
        {
            Console.Clear();
            TypeWriterEffect("VERIDIAN'S PACT", 100);
            Console.WriteLine("\n");
            TypeWriterEffect("A journey of choices, consequences, and second chances...", 50);
            Console.WriteLine("\n\nPress any key to begin your journey...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // Changes the player's current location and displays its information
        public void ChangeLocation(Location newLocation)
        {
            currentLocation = newLocation;
            Console.Clear();
            DisplayLocationInfo();
        }

        // Displays the name and description of the current location
        public void DisplayLocationInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Location: {currentLocation.Name}");
            Console.ResetColor();
            Console.WriteLine(currentLocation.Description);
            Console.WriteLine();
        }

        // Processes player input commands
        private void HandlePlayerInput()
        {
            Console.Write("\nEnter a command (type 'help' for options): ");
            string input = Console.ReadLine()?.Trim().ToLower();

            switch (input)
            {
                case "inventory":
                case "i":
                    player.DisplayInventory();
                    break;
                case "stats":
                case "s":
                    player.DisplayStats();
                    break;
                case "look":
                case "l":
                    DisplayLocationInfo();
                    break;
                case "help":
                case "h":
                    DisplayHelp();
                    break;
                case "quit":
                case "q":
                    isRunning = false;
                    Console.Clear();
                    TypeWriterEffect("Thank you for playing Veridian's Pact. Goodbye.", 50);
                    Thread.Sleep(2000);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command. Type 'help' for options.");
                    Console.ResetColor();
                    break;
            }
        }

        // Displays the list of available commands
        private void DisplayHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("- inventory (i): Check your items");
            Console.WriteLine("- stats (s): View your character stats");
            Console.WriteLine("- look (l): Examine your surroundings again");
            Console.WriteLine("- help (h): Display this information");
            Console.WriteLine("- quit (q): Exit the game");
            Console.ResetColor();
        }

        // Prints text with a typewriter effect, character by character
        public static void TypeWriterEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        // Retrieves a location by its name, returning the current location if not found
        public Location GetLocation(string name)
        {
            return locations.Find(loc => loc.Name == name) ?? currentLocation;
        }
    }
}