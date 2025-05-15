using System.Collections.Generic;

namespace VeridianPact
{
    class LockerRoomScene : Scene
    {
        public LockerRoomScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Display location
            Console.Clear();
            game.DisplayLocationInfo();

            // Setup scene
            Game.TypeWriterEffect("You open your locker and begin emptying its contents into a paper bag. Six years of your life reduced to a few personal items.");

            // Emma's dialogue based on relationship
            NPC emma = game.GetLocation("The Golden Plate - Kitchen").NPCs.Find(n => n.Name == "Emma");
            if (emma != null && emma.RelationshipValue >= 5)
            {
                Game.TypeWriterEffect("\nEmma slips into the room, her eyes red. \"I heard what happened. You didn’t deserve this, especially after helping me.\"");
                Game.TypeWriterEffect("\n\"It was bound to happen,\" you reply. \"Victor’s been looking for an excuse.\"");
                Game.TypeWriterEffect("\nShe presses a folded napkin into your hand. \"My number. Call me if you need a friend... or more.\"");
                emma.ModifyRelationship(1);
            }
            else if (emma != null && emma.RelationshipValue >= 2)
            {
                Game.TypeWriterEffect("\nEmma slips into the room, her eyes red. \"I heard what happened. You didn’t deserve this.\"");
                Game.TypeWriterEffect("\n\"It was bound to happen,\" you reply. \"Victor’s been looking for an excuse.\"");
                Game.TypeWriterEffect("\nShe places a folded napkin in your hand. \"My number. Call me if you need anything.\"");
                emma.ModifyRelationship(1);
            }
            else
            {
                Game.TypeWriterEffect("\nEmma slips into the room. \"I heard what happened. I’m so sorry.\"");
                Game.TypeWriterEffect("\n\"Don’t be,\" you reply. \"It was a long time coming.\"");
                Game.TypeWriterEffect("\nShe places a folded napkin in your hand. \"My number. If you ever need anything...\" She leaves before you can respond.");
            }

            // Add Emma's phone number to inventory
            Item emmasNumber = new EmmasPhoneNumber();
            player.AddItem(emmasNumber);

            // Introduce the Librarian
            Game.TypeWriterEffect("\nAs you close your locker for the final time, you notice you're not alone anymore.");
            Game.TypeWriterEffect("\nIn the corner, partially hidden by shadow, sits a figure you've never seen before.");

            // Player choices
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