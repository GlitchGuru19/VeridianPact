// Seven endings across two worlds.
// Knowledge path (#1-#4), Riches path (#5-#7).

using System;

namespace VeridianPact
{
    // 1) Scholar Sovereign (Knowledge path, wisdom+courage leadership)
    class EndingScholarSovereign : Scene
    {
        public EndingScholarSovereign(Game game, Player player, Location location) : base(game, player, location) { }
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ENDING: SCHOLAR SOVEREIGN");
            Console.ResetColor();

            Game.TypeWriterEffect("\nYou refuse crowns and accept burdens. Lyrian listens and grows curious instead of cruel.");
            Game.TypeWriterEffect("Power stays honest because you do.");

            Console.WriteLine("\nPress any key to end the game...");
            Console.ReadKey(true);
        }
    }

    // 2) Guardian of Veridian (Knowledge path, conscience+warden)
    class EndingGuardianOfVeridian : Scene
    {
        public EndingGuardianOfVeridian(Game game, Player player, Location location) : base(game, player, location) { }
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ENDING: GUARDIAN OF VERIDIAN");
            Console.ResetColor();

            Game.TypeWriterEffect("\nYou shelter the gentle, guide the lost, and stitch harm closed before it bleeds.");
            Game.TypeWriterEffect("Seasons change; the forest remains—because you did, too.");

            Console.WriteLine("\nPress any key to end the game...");
            Console.ReadKey(true);
        }
    }

    // 3) Hearts and Harbors (Knowledge path, love with Emma)
    class EndingHeartsAndHarbors : Scene
    {
        public EndingHeartsAndHarbors(Game game, Player player, Location location) : base(game, player, location) { }
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("ENDING: HEARTS AND HARBORS");
            Console.ResetColor();

            Game.TypeWriterEffect("\nAt Lyrian’s harbor, Emma laughs at a thieving gull. You open a tea-room together. You stay.");
            Game.TypeWriterEffect("Home matters more than magic ever could.");

            Console.WriteLine("\nPress any key to end the game...");
            Console.ReadKey(true);
        }
    }

    // 4) Wanderer of Paths (Knowledge path, navigator/default)
    class EndingWandererOfPaths : Scene
    {
        public EndingWandererOfPaths(Game game, Player player, Location location) : base(game, player, location) { }
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("ENDING: WANDERER OF PATHS");
            Console.ResetColor();

            Game.TypeWriterEffect("\nYou choose people over banners. Bridges over battlements. Quiet victories over loud applause.");
            Game.TypeWriterEffect("You leave Veridian kinder than you found it. Enough.");

            Console.WriteLine("\nPress any key to end the game...");
            Console.ReadKey(true);
        }
    }

    // 5) Tycoon With A Conscience (Riches path, ethical + high conscience)
    class EndingTycoonWithAConscience : Scene
    {
        public EndingTycoonWithAConscience(Game game, Player player, Location location) : base(game, player, location) { }
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ENDING: TYCOON WITH A CONSCIENCE");
            Console.ResetColor();

            Game.TypeWriterEffect("\nYou prove profit can pay for dignity. Case studies bear your name; paychecks carry your promise.");
            Game.TypeWriterEffect("Emma mentors staff at The Saffron Veil. Legacy matters more than headlines.");

            Console.WriteLine("\nPress any key to end the game...");
            Console.ReadKey(true);
        }
    }

    // 6) Empire Of Glass (Riches path, aggressive + low conscience)
    class EndingEmpireOfGlass : Scene
    {
        public EndingEmpireOfGlass(Game game, Player player, Location location) : base(game, player, location) { }
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("ENDING: EMPIRE OF GLASS");
            Console.ResetColor();

            Game.TypeWriterEffect("\nNumbers obey. Towers gleam. Silence grows.");
            Game.TypeWriterEffect("You learn how victory can look like a reflection—beautiful and empty.");

            Console.WriteLine("\nPress any key to end the game...");
            Console.ReadKey(true);
        }
    }

    // 7) Quiet Restorer (Riches path, local build with Emma)
    class EndingQuietRestorer : Scene
    {
        public EndingQuietRestorer(Game game, Player player, Location location) : base(game, player, location) { }
        public override void Play()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("ENDING: QUIET RESTORER");
            Console.ResetColor();

            Game.TypeWriterEffect("\nYou step off the fast road. You buy a stake in The Saffron Veil. You restore, not just renovate.");
            Game.TypeWriterEffect("Some fortunes are counted in names remembered. You finally count yours right.");

            Console.WriteLine("\nPress any key to end the game...");
            Console.ReadKey(true);
        }
    }
}
