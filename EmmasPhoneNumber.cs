namespace VeridianPact
{
    class EmmasPhoneNumber : Item
    {
        Game game;

        public EmmasPhoneNumber() : base("Emma's Phone Number", "A phone number written on a napkin.", true) { }

        public override void Use(Player player, GameState gameState)
        {
            // Use item to contact Emma
            Console.WriteLine("You call Emma using the number on the napkin.");
            if (gameState.GetFlag("HasJobLead"))
            {
                Console.WriteLine("Emma picks up. \"Hey, Marcus! Did you check out The Saffron Veil? I got hired! They’re still looking for someone with your experience.\"");
                gameState.SetFlag("ContactedEmma", true);
            }
            else
            {
                Console.WriteLine("Emma answers. \"Marcus, good to hear from you. I'm still figuring things out here. Let’s catch up soon.\"");
                gameState.SetFlag("ContactedEmma", true);
            }
            // Strengthen relationship
            NPC emma = null;
            foreach (var loc in game.GetLocations())
            {
                emma = loc.NPCs.Find(n => n.Name == "Emma");
                if (emma != null) break;
            }
            if (emma != null) emma.ModifyRelationship(1);
        }
    }
}