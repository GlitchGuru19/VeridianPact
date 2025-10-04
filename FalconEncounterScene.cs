// Pre-Librarian rooftop encounter: meet Aster the trained falcon.
// Choice: bond as a pet (companion) or leave wild.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class FalconEncounterScene : Scene
    {
        public FalconEncounterScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Console.Clear();
            game.ChangeLocation(game.GetLocation("Aurora Rooftop Aviary"));
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("A hawk circles, then lands—Aster. He watches you like you’re a puzzle worth solving.");

            List<string> options = new List<string>
            {
                "Offer food and a gentle whistle to bond with Aster",
                "Admire Aster from a distance and leave him wild",
                "Ask Magenta (at the city library) about falcons before deciding"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("Aster steps closer. You offer food, whistle softly. Bond accepted.");
                    player.ModifyStat("Wisdom", 1);
                    player.ModifyStat("Courage", 1);
                    game.SetFlag("AsterBonded", true);
                    break;

                case 2:
                    Game.TypeWriterEffect("You respect the sky. Aster nods—wild remains wild.");
                    player.ModifyStat("Wisdom", 1);
                    game.SetFlag("AsterBonded", false);
                    break;

                case 3:
                    Game.TypeWriterEffect("You detour to the Aurora City Library. Magenta meets you with a crisp nod.");
                    game.ChangeLocation(game.GetLocation("Aurora City Library"));
                    Game.TypeWriterEffect("\nMagenta: \"Bond if you must, but let him still be the sky.\"");
                    player.ModifyStat("Charisma", 1);

                    List<string> second = new List<string>
                    {
                        "Bond with Aster after Magenta's advice",
                        "Leave Aster wild after Magenta's advice"
                    };
                    DisplayOptions(second);
                    int secondChoice = GetPlayerChoice(second.Count);
                    Console.Clear();

                    if (secondChoice == 1)
                    {
                        Game.TypeWriterEffect("You return and bond gently. Aster accepts.");
                        player.ModifyStat("Wisdom", 1);
                        player.ModifyStat("Courage", 1);
                        game.SetFlag("AsterBonded", true);
                    }
                    else
                    {
                        Game.TypeWriterEffect("You return and bow slightly. The sky remains his.");
                        player.ModifyStat("Wisdom", 1);
                        game.SetFlag("AsterBonded", false);
                    }
                    break;
            }

            // After rooftop/library moment, proceed toward Librarian in locker room
            Location lockerRoom = game.GetLocation("The Golden Plate - Employee Locker Room");
            game.ChangeLocation(lockerRoom);
            new LockerRoomScene(game, player, lockerRoom).Play();
        }
    }
}
