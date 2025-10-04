// Core game orchestrator:
// - Holds player, locations, current location, game state
// - Displays intro
// - Manages main loop and player commands
// - Provides helpers for locations, flags, and a typewriter effect

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
            // Modern world locations
            Location restaurant = new Location(
                "The Golden Plate - Main Dining Area",
                "An elegant restaurant bustling with weekend patrons. Crystal light, hushed conversations."
            );

            Location kitchen = new Location(
                "The Golden Plate - Kitchen",
                "Heat, shouted orders, and plates that must be perfect."
            );

            Location victorsOffice = new Location(
                "The Golden Plate - Victor's Office",
                "Awards on walls and decisions made too fast."
            );

            Location lockerRoom = new Location(
                "The Golden Plate - Employee Locker Room",
                "Metal lockers, fluorescent lights, endings turned into beginnings."
            );

            Location cityLibrary = new Location(
                "Aurora City Library",
                "Stacks of books, sunlit tables, and quiet intellect."
            );

            Location rooftopAviary = new Location(
                "Aurora Rooftop Aviary",
                "Wind-swept roof, a trained hawk circles, the skyline feels close enough to touch."
            );

            Location saffronVeil = new Location(
                "The Saffron Veil - Bistro",
                "Warm light, fair wages, cardamom in the air."
            );

            // Veridian world locations
            Location veridianForest = new Location(
                "Veridian Forest",
                "Towering trees, lucid air, and paths that remember your footprints."
            );

            Location lyrianCitadel = new Location(
                "Lyrian Citadel",
                "Stone walls, watchful banners, strategy breathing in the halls."
            );

            Location harborTeaRoom = new Location(
                "Lyrian Harbor Tea Room",
                "A quiet nook by the sea. Soups for storms, stories for the willing."
            );

            Location veridianVillage = new Location(
                "Verdant Hollow Village",
                "Cobblestones, herb gardens, hope that grows in window boxes."
            );

            Location marketBazaar = new Location(
                "Lyrian Market Bazaar",
                "Lanterns, bargaining, and the heartbeat of a city."
            );

            // NPCs
            NPC emma = new NPC("Emma", "A new server—sharp, resilient, kinder than most.");
            NPC magenta = new NPC("Magenta", "A well-dressed gent with impeccable manners and one true sentence when you need it.");
            NPC asterHawk = new NPC("Aster", "A trained falcon—keen eyes, steady wings.");

            // Place NPCs
            kitchen.AddNPC(emma);
            cityLibrary.AddNPC(magenta);
            rooftopAviary.AddNPC(asterHawk);

            // Register locations
            locations.AddRange(new[]
            {
                restaurant, kitchen, victorsOffice, lockerRoom,
                cityLibrary, rooftopAviary, saffronVeil,
                veridianForest, lyrianCitadel, harborTeaRoom, veridianVillage, marketBazaar
            });

            // Start in restaurant
            currentLocation = restaurant;
        }

        public void Start()
        {
            DisplayIntro();
            Scene openingScene = new OpeningScene(this, player, currentLocation);
            openingScene.Play();

            // Main loop (free-roam and post-arc interactions are light; story is scene-driven)
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
            TypeWriterEffect("A journey through two worlds—and the promises you keep in both.", 50);
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
            // List NPCs in current location
            if (currentLocation.NPCs.Count == 0)
            {
                Console.WriteLine("There's no one here to talk to.");
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

            var picked = currentLocation.NPCs[choice - 1];
            picked.Interact(player);

            // Flavor: Magenta or Aster small stat bumps
            if (picked.Name == "Magenta")
            {
                Console.WriteLine("\nMagenta adjusts his cufflinks: \"Build something people want to belong to.\"");
                player.ModifyStat("Charisma", 1);
            }
            if (picked.Name == "Aster")
            {
                Console.WriteLine("\nAster tilts his head at your whistle, hops closer, and watches over you.");
                player.ModifyStat("Wisdom", 1);
            }
        }

        private void UseItem()
        {
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

        public List<Location> GetLocations()
        {
            return locations;
        }

        public void SetFlag(string key, bool value)
        {
            gameState.SetFlag(key, value);
        }

        public bool GetFlag(string key)
        {
            return gameState.GetFlag(key);
        }

        // Explore limit tracking
        private int exploreCount = 0; // track how many times explore has been used

        public bool CanExplore()
        {
            return exploreCount < 2;
        }

        public void IncrementExplore()
        {
            exploreCount++;
        }
    }
}
