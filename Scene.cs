using System;
using System.Collections.Generic;
using VeridianPact;

namespace VeridiansPact
{
    abstract class Scene
    {
        protected Game game;
        protected Player player;
        protected Location location;

        // Initializes a scene with references to the game, player, and location
        public Scene(Game game, Player player, Location location)
        {
            this.game = game;
            this.player = player;
            this.location = location;
        }

        // Defines the logic for playing the scene
        public abstract void Play();

        // Displays a list of options for the player to choose from
        protected void DisplayOptions(List<string> options)
        {
            Console.WriteLine("\nWhat will you do?");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            Console.Write($"\nEnter your choice (1-{options.Count}): ");
        }

        // Retrieves and validates the player's choice
        protected int GetPlayerChoice(int maxChoice)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > maxChoice)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Please enter a valid choice (1-{maxChoice}): ");
                Console.ResetColor();
            }
            return choice;
        }
    }
}