using System.Collections.Generic;

namespace VeridianPact
{
    class IncidentScene : Scene
    {
        public IncidentScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            // Display location
            Console.Clear();
            game.DisplayLocationInfo();

            // Setup incident with Victor and Emma
            Game.TypeWriterEffect("Twenty minutes later...");
            Game.TypeWriterEffect("\nVictor, the restaurant owner, bursts through the swinging doors, his face flushed with anger. He's holding a returned dish of beef wellington.");
            Game.TypeWriterEffect("\n\"Who's responsible for table 22?\" he shouts over the kitchen noise.");
            Game.TypeWriterEffect("\nEmma raises her hand timidly. \"I am, sir.\"");
            Game.TypeWriterEffect("\nVictor marches toward her. \"This is the third mistake tonight! What kind of incompetent staff am I hiring these days?\"");
            Game.TypeWriterEffect("\nEmma looks on the verge of tears as Victor continues his public dressing-down.");

            // Player choices
            List<string> options = new List<string>
            {
                player.Stats["Courage"] >= 7 ? "Intervene directly and stand up for Emma" : "Intervene directly and stand up for Emma (Requires Courage > 7)",
                "Wait until Victor leaves, then comfort Emma",
                "Focus on your work - getting involved might cost your job"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);

            Console.Clear();

            NPC emma = location.NPCs.Find(n => n.Name == "Emma");

            switch (choice)
            {
                case 1:
                    if (player.Stats["Courage"] < 7)
                    {
                        // Insufficient courage
                        Game.TypeWriterEffect("You want to step in, but your courage falters. You stay silent, hoping someone else will intervene.");
                        player.ModifyStat("Conscience", -1);
                        if (emma != null) emma.ModifyRelationship(-1);
                        Console.WriteLine("\nPress any key to continue your shift...");
                        Console.ReadKey(true);
                        return;
                    }
                    // Intervene for Emma
                    Game.TypeWriterEffect("\"That's enough, Victor,\" you say, stepping between him and Emma.");
                    Game.TypeWriterEffect("\nThe kitchen falls silent. Even the line cooks stop what they're doing.");
                    player.ModifyStat("Courage", 2);
                    player.ModifyStat("Wisdom", 1);
                    if (emma != null) emma.ModifyRelationship(2);
                    game.SetFlag("IntervenedForEmma", true);
                    Scene confrontationScene = new ConfrontationScene(game, player, location);
                    confrontationScene.Play();
                    break;
                case 2:
                    // Comfort Emma, leads to ally scene
                    Game.TypeWriterEffect("You keep your head down as Victor continues berating Emma for another minute before storming off.");
                    Game.TypeWriterEffect("\nOnce he's gone, you approach Emma who's wiping away tears.");
                    Game.TypeWriterEffect("\n\"Don't take it personally,\" you say softly. \"He does this to everyone.\"");
                    Game.TypeWriterEffect("\n\"How do you stand it?\" she asks.");
                    Game.TypeWriterEffect("\nYou don't have a good answer.");
                    player.ModifyStat("Conscience", 1);
                    if (emma != null) emma.ModifyRelationship(1);
                    game.SetFlag("ComfortedEmma", true);
                    Scene emmaAllyScene = new EmmaAllyScene(game, player, location);
                    emmaAllyScene.Play();
                    break;
                case 3:
                    // Ignore Emma, leads to direct firing
                    Game.TypeWriterEffect("You turn away and focus intently on plating your orders.");
                    Game.TypeWriterEffect("\nVictor's voice continues to ring through the kitchen, but you've learned to tune it out.");
                    Game.TypeWriterEffect("\nAs Emma sniffles quietly, you feel a twinge of guilt but push it aside.");
                    Game.TypeWriterEffect("\nLater, Victor storms back in, pointing at you. \"Marcus, Table 9's order was wrong, and they're furious! You're done here!\"");
                    player.ModifyStat("Conscience", -2);
                    if (emma != null) emma.ModifyRelationship(-2);
                    game.SetFlag("IgnoredEmma", true);
                    Location victorsOffice = game.GetLocation("The Golden Plate - Victor's Office");
                    game.ChangeLocation(victorsOffice);
                    Scene firingScene = new FiringScene(game, player, victorsOffice);
                    firingScene.Play();
                    break;
            }
        }
    }
}