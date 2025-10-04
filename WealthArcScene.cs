// Modern path decision arc:
// - Aggressive takeover
// - Ethical enterprises
// - Local venture with Emma
// Routes into WealthEndingRouterScene.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class WealthArcScene : Scene
    {
        public WealthArcScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Game.TypeWriterEffect("\nYour inbox overflows: VC invites, takeover whispers, and a letter from The Saffron Veil’s manager—Emma’s lead ripened.");

            List<string> options = new List<string>
            {
                "Pursue an aggressive corporate takeover to maximize short-term gains",
                "Fund ethical enterprises, prioritize fair wages and community impact",
                "Step back from high finance and build a local venture with Emma"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);
            Console.Clear();

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("You sharpen your strategy into a blade. Hostile bids. Perfect timing.");
                    player.ModifyStat("Resourcefulness", 2);
                    player.ModifyStat("Conscience", -2);
                    game.SetFlag("WealthAggressive", true);
                    break;

                case 2:
                    Game.TypeWriterEffect("You choose profit with purpose. You raise wages and mentor managers.");
                    player.ModifyStat("Conscience", 2);
                    player.ModifyStat("Wisdom", 1);
                    game.SetFlag("WealthEthical", true);
                    break;

                case 3:
                    Game.TypeWriterEffect("You call Emma. You buy a stake in The Saffron Veil. You fix what hurts and grow what heals.");
                    player.ModifyStat("Charisma", 1);
                    game.SetFlag("WealthLocal", true);
                    game.SetFlag("HasJobLead", true);
                    break;
            }

            // Optional flavor: consult Magenta and Aster if bonded
            Game.TypeWriterEffect("\nMagenta at the Aurora City Library: \"Build something people want to belong to.\"");
            if (game.GetFlag("AsterBonded"))
            {
                Game.TypeWriterEffect("On the rooftop, Aster watches the city with you. The wind feels honest.");
                player.ModifyStat("Wisdom", 1);
            }

            new WealthEndingRouterScene(game, player, location).Play();
        }
    }

    class WealthEndingRouterScene : Scene
    {
        public WealthEndingRouterScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            bool aggressive = game.GetFlag("WealthAggressive");
            bool ethical = game.GetFlag("WealthEthical");
            bool local = game.GetFlag("WealthLocal");
            int conscience = player.Stats["Conscience"];

            if (ethical && conscience >= 8)
            {
                new EndingTycoonWithAConscience(game, player, location).Play(); // Ending #5
            }
            else if (aggressive && conscience <= 5)
            {
                new EndingEmpireOfGlass(game, player, location).Play(); // Ending #6
            }
            else if (local)
            {
                new EndingQuietRestorer(game, player, location).Play(); // Ending #7
            }
            else
            {
                new EndingQuietRestorer(game, player, location).Play();
            }
        }
    }
}
