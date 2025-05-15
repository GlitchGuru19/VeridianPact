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
            // Initialize game components
            player = new Player();
            gameState = new GameState();
            locations = new List<Location>();
            InitializeWorld();
            isRunning = true;
        }

        private void InitializeWorld()
        {
            // Create locations
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

            // Add Emma as NPC in kitchen
            NPC emma = new NPC("Emma", "A new server, nervous but eager to prove herself.");
            kitchen.AddNPC(emma);

            locations.AddRange(new[] { restaurant, kitchen, victorsOffice, lockerRoom });
            currentLocation = restaurant;
        }

        public void Start()
        {
            // Start game with intro
            DisplayIntro();
            Scene openingScene = new OpeningScene(this, player, currentLocation);
            openingScene.Play();

            // Main game loop
            while (isRunning)
            {
                HandlePlayerInput();
            }
        }

        private void DisplayIntro()
        {
            // Show title screen
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
            // Update current location
            currentLocation = newLocation;
            Console.Clear();
            DisplayLocationInfo();
        }

        public void DisplayLocationInfo()
        {
            // Show location details and NPCs
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Location: {(currentLocation != null ? currentLocation.Name : "Unknown Location")}");
            Console.ResetColor();
            Console.WriteLine(currentLocation != null ? currentLocation.Description : "No description available.");
            if (currentLocation.NPCs.Count > 0)
            {
                Console.WriteLine("\nPeople here:");
                foreach (var npc in currentLocation.NPCs)
                {
                    Console.WriteLine($"- {npc.Name}: {npc.Description}");
                }
            }
            if (currentLocation.Items.Count > 0)
            {
                Console.WriteLine("\nItems here:");
                foreach (var item in currentLocation.Items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
            Console.WriteLine();
        }

        private void HandlePlayerInput()
        {
            // Process player commands
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
                case "talk":
                case "t":
                    InteractWithNPC();
                    break;
                case "use":
                case "u":
                    UseItem();
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

        private void InteractWithNPC()
        {
            // Handle NPC interaction
            if (currentLocation.NPCs.Count == 0)
            {
                Console.WriteLine("There’s no one here to talk to.");
                return;
            }

            Console.WriteLine("\nWho would you like to talk to?");
            for (int i = 0; i < currentLocation.NPCs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {currentLocation.NPCs[i].Name}");
            }
            Console.Write($"Enter your choice (1-{currentLocation.NPCs.Count}): ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > currentLocation.NPCs.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice.");
                Console.ResetColor();
                return;
            }

            currentLocation.NPCs[choice - 1].Interact(player);
        }

        private void UseItem()
        {
            // Handle item usage
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                return;
            }

            Console.WriteLine("\nWhich item would you like to use?");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Inventory[i].Name}");
            }
            Console.Write($"Enter your choice (1-{player.Inventory.Count}): ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > player.Inventory.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice.");
                Console.ResetColor();
                return;
            }

            Item item = player.Inventory[choice - 1];
            if (item.IsUsable)
            {
                item.Use(player, gameState);
            }
            else
            {
                Console.WriteLine($"The {item.Name} cannot be used.");
            }
        }

        private void DisplayHelp()
        {
            // Show available commands
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("- inventory (i): Check your items");
            Console.WriteLine("- stats (s): View your character stats");
            Console.WriteLine("- look (l): Examine your surroundings again");
            Console.WriteLine("- talk (t): Interact with people in the location");
            Console.WriteLine("- use (u): Use an item from your inventory");
            Console.WriteLine("- help (h): Display this information");
            Console.WriteLine("- quit (q): Exit the game");
            Console.ResetColor();
        }

        public static void TypeWriterEffect(string text, int delay = 30)
        {
            // Display text with typewriter effect
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public Location GetLocation(string name)
        {
            // Find location by name
            return locations.Find(loc => loc.Name == name) ?? currentLocation;
        }

        public List<Location> GetLocations()
        {
            // Return all locations
            return locations;
        }

        public void SetFlag(string key, bool value)
        {
            // Set game flag
            gameState.SetFlag(key, value);
        }

        public bool GetFlag(string key)
        {
            // Get game flag
            return gameState.GetFlag(key);
        }
    }
}