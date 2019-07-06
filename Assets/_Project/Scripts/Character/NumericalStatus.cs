namespace Lake.Character
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.UI;

    [Serializable]
    public class NumericalStatus
    {
        public enum StatsType
        {
            Health, Stamina, Hunger
        }

        private StatsType type = StatsType.Health;
        private float currentValue = 100.0f;
        private float maxValue = 100.0f;
        private float minValue = 0.0f;
        private float recoverRate = 5.0f;
        private Action onFullyRecovered = default;
        private Action onFullyDrained = default;
        private Action onDrain = default;
        private Action onRecover = default;

        public float CurrentValue
        {
            get => this.currentValue;
            set
            {
                float newValue = Mathf.Clamp(value, this.minValue, this.maxValue);
                if(newValue != this.currentValue)
                {
                    if(newValue < this.currentValue)
                    {
                        this.onDrain?.Invoke();
                    }

                    if(newValue > this.currentValue)
                    {
                        this.onRecover?.Invoke();
                    }

                    if (newValue == maxValue)
                    {
                        this.onFullyRecovered?.Invoke();
                    }

                    if(newValue == minValue)
                    {
                        this.onFullyDrained?.Invoke();
                    }

                    this.currentValue = newValue;
                }
            } 
        }

        public float MaxValue { get => this.maxValue; set => this.maxValue = value; }

        public float MinValue { get => this.minValue; set => this.minValue = value; }

        public float RecoverRate { get => this.recoverRate; set => this.recoverRate = value; }

        public Action OnFullyRecovered { get => this.onFullyRecovered; set => this.onFullyRecovered = value; }

        public Action OnFullyDrained { get => this.onFullyDrained; set => this.onFullyDrained = value; }

        public Action OnDrain { get => this.onDrain; set => this.onDrain = value; }

        public Action OnRecover { get => this.onRecover; set => this.onRecover = value; }

        public StatsType Type { get => type; set => type = value; }

        public NumericalStatus(StatsType type, float currentValue, float maxValue, float minValue, float recoverRate)
        {
            this.type = type;
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.recoverRate = recoverRate;
        }
    }
}
