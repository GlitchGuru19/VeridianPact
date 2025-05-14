using System;
using System.Collections.Generic;
using System.Threading;

namespace VeridianPact
{
    class Game
    {
        private Player player;
        private List<Location> locations;
        private Location currentLocation;
        private GameState gameState;
        private bool isRunning;

        public Game()
        {
            player = new Player();
            gameState = new GameState();
            locations = new List<Location>();
            InitializeWorld();
            isRunning = true;
        }

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

        private void DisplayIntro()
        {
            Console.Clear();
            TypeWriterEffect("VERIDIAN PACT", 100);
            Console.WriteLine("\n");
            TypeWriterEffect("A journey of choices, consequences, and second chances...", 50);
            Console.WriteLine("\n\nPress any key to begin your journey...");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void ChangeLocation(Location newLocation)
        {
            currentLocation = newLocation;
            Console.Clear();
            DisplayLocationInfo();
        }

        public void DisplayLocationInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Location: {(currentLocation != null ? currentLocation.Name : "Unknown Location")}");
            Console.ResetColor();
            Console.WriteLine(currentLocation != null ? currentLocation.Description : "No description available.");
            Console.WriteLine();
        }

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
                    TypeWriterEffect("Thank you for playing Veridian Pact. Goodbye.", 50);
                    Thread.Sleep(2000);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command. Type 'help' for options.");
                    Console.ResetColor();
                    break;
            }
        }

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

        public static void TypeWriterEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public Location GetLocation(string name)
        {
            return locations.Find(loc => loc.Name == name) ?? currentLocation;
        }

        public void SetFlag(string key, bool value)
        {
            gameState.SetFlag(key, value);
        }

        public bool GetFlag(string key)
        {
            return gameState.GetFlag(key);
        }
    }
}