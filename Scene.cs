// Abstract base for scenes:
// - Provides option display and validated input helpers

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    abstract class Scene
    {
        protected Game game;
        protected Player player;
        protected Location location;

        public Scene(Game game, Player player, Location location)
        {
            this.game = game;
            this.player = player;
            this.location = location;
        }

        public abstract void Play();

        protected void DisplayOptions(List<string> options)
        {
            Console.WriteLine("\nWhat do you do?");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
        }

        protected int GetPlayerChoice(int maxOptions)
        {
            int choice;
            while (true)
            {
                Console.Write($"\nEnter your choice (1-{maxOptions}): ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= maxOptions)
                {
                    return choice;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ResetColor();
            }
        }
    }
}
