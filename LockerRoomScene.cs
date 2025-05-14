using System.Collections.Generic;
using VeridianPact;

namespace VeridianPact
{
    class LockerRoomScene : Scene
    {
        // Initializes the locker room scene
        public LockerRoomScene(Game game, Player player, Location location) : base(game, player, location) { }

        // Plays the locker room scene where the player gathers their belongings
        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("You open your locker and begin emptying its contents into a paper bag. Six years of your life reduced to a few personal items.");
            Game.TypeWriterEffect("\nEmma slips into the room. \"I heard what happened. I'm so sorry.\"");
            Game.TypeWriterEffect("\n\"Don't be,\" you reply. \"It was a long time coming.\"");
            Game.TypeWriterEffect("\nShe places a folded napkin in your hand. \"My number. If you ever need anything...\" She leaves before you can respond.");

            Item emmasNumber = new Item("Emma's Phone Number", "A phone number written on a napkin.");
            player.AddItem(emmasNumber);

            Game.TypeWriterEffect("\nAs you close your locker for the final time, you notice you're not alone anymore.");
            Game.TypeWriterEffect("\nIn the corner, partially hidden by shadow, sits a figure you've never seen before.");

            List<string> options = new List<string>
            {
                "\"Who are you? Staff only back here.\"",
                "Ignore the figure and continue gathering your things",
                "Nod acknowledgment but say nothing"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();
            Scene librarianScene = new LibrarianScene(game, player, location);
            librarianScene.Play();
        }
    }
}