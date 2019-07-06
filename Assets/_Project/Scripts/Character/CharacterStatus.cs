namespace Lake.Character
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.UI;

    public class CharacterStatus
    {
        private Dictionary<NumericalStatus.StatsType, NumericalStatus> numericalStatusMap = new Dictionary<NumericalStatus.StatsType, NumericalStatus>();

        public CharacterStatus()
        {
            this.AddDefaultEntries();
        }

        public Dictionary<NumericalStatus.StatsType, NumericalStatus> NumericalStatusMap { get => this.numericalStatusMap; }

        public void Update(float deltaTime)
        {
            foreach(KeyValuePair<NumericalStatus.StatsType, NumericalStatus> pair in this.numericalStatusMap)
            {
                pair.Value.CurrentValue += pair.Value.RecoverRate * deltaTime;
            }
        }

        private void AddDefaultEntries()
        {
            NumericalStatus healthStatus = new NumericalStatus(NumericalStatus.StatsType.Health, 100.0f, 100.0f, 0.0f, 1.0f);
            NumericalStatus staminaStatus = new NumericalStatus(NumericalStatus.StatsType.Stamina, 100.0f, 100.0f, 0.0f, 10.0f);
            NumericalStatus hungerStatus = new NumericalStatus(NumericalStatus.StatsType.Hunger, 100.0f, 100.0f, 0.0f, -5.0f);
            this.numericalStatusMap.Add(healthStatus.Type, healthStatus);
            this.numericalStatusMap.Add(staminaStatus.Type, staminaStatus);
            this.numericalStatusMap.Add(hungerStatus.Type, hungerStatus);
        }
    } 
}

