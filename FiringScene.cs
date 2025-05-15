using System.Collections.Generic;

namespace VeridianPact
{
    class FiringScene : Scene
    {
        public FiringScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Victor fires the player
            Game.TypeWriterEffect("Victor stands up, his chair scraping against the hardwood floor.");
            Game.TypeWriterEffect("\n\"You're fired. Clear out your locker and be gone in fifteen minutes, or I'll have security escort you out.\"");
            Game.TypeWriterEffect("\nThe words hang in the air between you. After six years, it ends like this.");

            // Player choices
            List<string> options = new List<string>
            {
                "Leave quietly with dignity",
                "Tell Victor exactly what you think of him now that you have nothing to lose",
                "Ask for a second chance"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    // Dignified exit
                    Game.TypeWriterEffect("You stand up slowly, maintaining eye contact with Victor.");
                    Game.TypeWriterEffect("\n\"Good luck finding someone who cares as much as I did,\" you say calmly.");
                    Game.TypeWriterEffect("\nYou turn and walk out, head held high.");
                    player.ModifyStat("Wisdom", 1);
                    break;
                case 2:
                    // Confront Victor
                    Game.TypeWriterEffect("Six years of frustration pour out of you.");
                    Game.TypeWriterEffect("\n\"You're a bully who builds himself up by tearing others down. Your restaurant succeeds despite you, not because of you.\"");
                    Game.TypeWriterEffect("\nVictor's face pales slightly as you tell him exactly what the staff says behind his back.");
                    Game.TypeWriterEffect("\n\"Get out!\" he finally shouts, standing up and pointing at the door.");
                    player.ModifyStat("Courage", 2);
                    break;
                case 3:
                    // Plead for job
                    Game.TypeWriterEffect("\"Victor, please. I need this job. Can we work something out?\"");
                    Game.TypeWriterEffect("\nVictor's expression is cold. \"You should have thought about that before questioning my authority.\"");
                    Game.TypeWriterEffect("\n\"The decision is final. Leave now.\"");
                    player.ModifyStat("Courage", -1);
                    break;
            }

            // Move to locker room
            Location lockerRoom = game.GetLocation("The Golden Plate - Employee Locker Room");
            game.ChangeLocation(lockerRoom);
            Scene lockerRoomScene = new LockerRoomScene(game, player, lockerRoom);
            lockerRoomScene.Play();
        }
    }
}