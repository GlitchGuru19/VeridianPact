using System.Collections.Generic;
using VeridianPact;

namespace VeridianPact
{
    class GameState
    {
        private Dictionary<string, bool> flags;
        private Dictionary<string, int> counters;

        // Initializes the game state with empty flags and counters
        public GameState()
        {
            flags = new Dictionary<string, bool>();
            counters = new Dictionary<string, int>();
        }

        // Sets a flag to a specified boolean value
        public void SetFlag(string flag, bool value) => flags[flag] = value;

        // Retrieves the value of a flag, returning false if not set
        public bool GetFlag(string flag) => flags.ContainsKey(flag) && flags[flag];

        // Sets a counter to a specified value
        public void SetCounter(string counter, int value) => counters[counter] = value;

        // Retrieves the value of a counter, returning 0 if not set
        public int GetCounter(string counter) => counters.ContainsKey(counter) ? counters[counter] : 0;

        // Increments a counter by 1
        public void IncrementCounter(string counter)
        {
            counters[counter] = GetCounter(counter) + 1;
        }
    }
}