using System.Collections.Generic;

namespace VeridianPact
{
    class GameState
    {
        private Dictionary<string, bool> flags;
        private Dictionary<string, int> counters;

        public GameState()
        {
            flags = new Dictionary<string, bool>();
            counters = new Dictionary<string, int>();
        }

        public void SetFlag(string flag, bool value) => flags[flag] = value;

        public bool GetFlag(string flag) => flags.ContainsKey(flag) && flags[flag];

        public void SetCounter(string counter, int value) => counters[counter] = value;

        public int GetCounter(string counter) => counters.ContainsKey(counter) ? counters[counter] : 0;

        public void IncrementCounter(string counter)
        {
            counters[counter] = GetCounter(counter) + 1;
        }
    }
}