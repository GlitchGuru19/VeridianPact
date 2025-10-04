// Fantasy path decision arc:
// - Council of Insight
// - Wardens of Veridian
// - Free Navigators
// - Seek Emma across worlds
// Crisis at Lyrian (defend, evacuate, avoid, follow rumor)
// Routes into VeridianEndingRouterScene.

using System;
using System.Collections.Generic;

namespace VeridianPact
{
    class VeridianArcScene : Scene
    {
        public VeridianArcScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            Game.TypeWriterEffect("\nThree banners sway in a clearing:");
            Game.TypeWriterEffect("\nSilver quill (Council of Insight), green stag (Wardens), blue compass (Free Navigators).");

            List<string> options = new List<string>
            {
                "Join the Council of Insight to advise leaders with wisdom",
                "Join the Wardens of Veridian to protect forest and folk",
                "Join the Free Navigators to roam, learn, and heal where needed",
                "Search for Emma—if bonds cross worlds, maybe love does too"
            };

            DisplayOptions(options);
            int choice = GetPlayerChoice(options.Count);
            Console.Clear();

            switch (choice)
            {
                case 1:
                    Game.TypeWriterEffect("The Council tests your mind—cause, consequence, compassion.");
                    player.ModifyStat("Wisdom", 2);
                    game.SetFlag("CouncilPath", true);
                    break;

                case 2:
                    Game.TypeWriterEffect("The Wardens teach wind and root. You learn what harm looks like before it happens.");
                    player.ModifyStat("Conscience", 2);
                    player.ModifyStat("Courage", 1);
                    game.SetFlag("WardenPath", true);
                    break;

                case 3:
                    Game.TypeWriterEffect("You choose roads over titles. You mend bridges and leave before parades find you.");
                    player.ModifyStat("Wisdom", 1);
                    player.ModifyStat("Charisma", 1);
                    game.SetFlag("NavigatorPath", true);
                    break;

                case 4:
                    Game.TypeWriterEffect("You follow laughter to water. Rumors say the harbor remembers a name: Emma.");
                    game.SetFlag("SeekEmmaVeridian", true);
                    break;
            }

            Location lyrianCitadel = game.GetLocation("Lyrian Citadel");
            game.ChangeLocation(lyrianCitadel);
            Game.TypeWriterEffect("\nA horn blares—Lyrian under siege. Choices become ropes you hold onto.");

            List<string> crisis = new List<string>
            {
                "Lead a defense at Lyrian using strategy and empathy",
                "Protect fleeing villagers through the forest’s secret paths",
                "Avoid the siege; focus on long-term healing and learning",
                "Follow a rumor that someone named Emma reached Lyrian’s harbor"
            };

            DisplayOptions(crisis);
            int crisisChoice = GetPlayerChoice(crisis.Count);
            Console.Clear();

            switch (crisisChoice)
            {
                case 1:
                    player.ModifyStat("Courage", 2);
                    game.SetFlag("LyrianDefense", true);
                    break;
                case 2:
                    player.ModifyStat("Conscience", 2);
                    game.SetFlag("LyrianEvac", true);
                    break;
                case 3:
                    player.ModifyStat("Wisdom", 1);
                    game.SetFlag("LyrianAvoid", true);
                    break;
                case 4:
                    game.SetFlag("LyrianEmmaRumor", true);
                    break;
            }

            // Aster companion benefit if bonded
            if (game.GetFlag("AsterBonded"))
            {
                Game.TypeWriterEffect("\nAster scouts from above. You read the land faster, safer.");
                player.ModifyStat("Wisdom", 1);
            }

            // Flavor: market and village
            Location bazaar = game.GetLocation("Lyrian Market Bazaar");
            game.ChangeLocation(bazaar);
            Game.TypeWriterEffect("\nLanterns sway over barter-songs. Advice becomes the currency people trust.");
            Location village = game.GetLocation("Verdant Hollow Village");
            game.ChangeLocation(village);
            Game.TypeWriterEffect("\nHerb gardens. Children ask if you can make storms gentler. You try.");

            new VeridianEndingRouterScene(game, player, location).Play();
        }
    }

    class VeridianEndingRouterScene : Scene
    {
        public VeridianEndingRouterScene(Game game, Player player, Location location) : base(game, player, location) { }

        public override void Play()
        {
            int wisdom = player.Stats["Wisdom"];
            int courage = player.Stats["Courage"];
            int conscience = player.Stats["Conscience"];
            bool council = game.GetFlag("CouncilPath");
            bool warden = game.GetFlag("WardenPath");
            bool navigator = game.GetFlag("NavigatorPath");
            bool seekEmma = game.GetFlag("SeekEmmaVeridian") || game.GetFlag("LyrianEmmaRumor");
            bool defense = game.GetFlag("LyrianDefense");
            bool evac = game.GetFlag("LyrianEvac");

            if (seekEmma && conscience >= 8)
            {
                new EndingHeartsAndHarbors(game, player, location).Play(); // #3
            }
            else if (council && defense && wisdom >= 10 && courage >= 8)
            {
                new EndingScholarSovereign(game, player, location).Play(); // #1
            }
            else if (warden && evac && conscience >= 9)
            {
                new EndingGuardianOfVeridian(game, player, location).Play(); // #2
            }
            else
            {
                new EndingWandererOfPaths(game, player, location).Play(); // #4
            }
        }
    }
}
