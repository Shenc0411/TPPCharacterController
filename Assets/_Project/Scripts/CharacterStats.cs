namespace Lake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.UI;

    public class CharacterStats
    {
        private Dictionary<StatsType, CharacterStatsEntry> statsEntries = new Dictionary<StatsType, CharacterStatsEntry>();

        public CharacterStats()
        {
            this.AddDefaultEntries();
        }

        public Dictionary<StatsType, CharacterStatsEntry> StatsEntries { get => this.statsEntries; }

        public void Update(float deltaTime)
        {
            foreach(KeyValuePair<StatsType, CharacterStatsEntry> pair in this.statsEntries)
            {
                pair.Value.CurrentValue += pair.Value.RecoverRate * deltaTime;
            }
        }

        private void AddDefaultEntries()
        {
            CharacterStatsEntry healthEntry = new CharacterHealthStatsEntry(100.0f, 100.0f, 0.0f, 5.0f);
            CharacterStatsEntry staminaEntry = new CharacterStaminaStatsEntry(100.0f, 100.0f, 0.0f, 5.0f);
            CharacterStatsEntry hungerEntry = new CharacterHungerStatsEntry(100.0f, 100.0f, 0.0f, -5.0f);
            this.statsEntries.Add(healthEntry.Type, healthEntry);
            this.statsEntries.Add(staminaEntry.Type, staminaEntry);
            this.statsEntries.Add(hungerEntry.Type, hungerEntry);
        }
    } 
}

