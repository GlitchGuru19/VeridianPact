// LockerRoomScene
// Receive Emma’s number (fixed: EmmasPhoneNumber(Game)), then meet the Librarian.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class LockerRoomScene : Scene
    {
        public LockerRoomScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Console.Clear();
            game.DisplayLocationInfo();

            Game.TypeWriterEffect("You pack six years into a paper bag. It fits too easily.");

            NPC emma = game.GetLocation("The Golden Plate - Kitchen").NPCs.Find(n => n.Name == "Emma");
            if (emma != null && emma.RelationshipValue >= 5)
            {
                Game.TypeWriterEffect("\nEmma slips in. \"You didn’t deserve this—especially after helping me.\"");
                Game.TypeWriterEffect("\nA napkin. \"My number. Call me if you need a friend... or more.\"");
                emma.ModifyRelationship(1);
            }
            else if (emma != null && emma.RelationshipValue >= 2)
            {
                Game.TypeWriterEffect("\nEmma slips in. \"You didn’t deserve this.\" A napkin. \"Call me if you need anything.\"");
                emma.ModifyRelationship(1);
            }
            else
            {
                Game.TypeWriterEffect("\nEmma: \"I'm so sorry.\" A napkin. \"My number.\" Then she leaves.");
            }

            // Fixed: pass Game to EmmasPhoneNumber
            Item emmasNumber = new EmmasPhoneNumber(game);
            player.AddItem(emmasNumber);

            Game.TypeWriterEffect("\nAs you close your locker, a figure waits in shadow—immaculate suit, bright eyes.");

            List<string> options = new List<string>
            {
                "\"Who are you? Staff only back here.\"",
                "Ignore the figure and continue gathering your things",
                "Nod acknowledgment but say nothing"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();
            new LibrarianScene(game, player, location).Play();
        }
    }
}
