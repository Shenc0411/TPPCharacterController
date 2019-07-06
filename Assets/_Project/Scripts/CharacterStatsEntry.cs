namespace Lake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.UI;

    public enum StatsType
    {
        Health, Stamina, Hunger
    }

    [Serializable]
    public abstract class CharacterStatsEntry
    {
        private StatsType type = StatsType.Health;
        private float currentValue = 100.0f;
        private float maxValue = 100.0f;
        private float minValue = 0.0f;
        private float recoverRate = 5.0f;
        private Action onFullyRecovered = default;
        private Action onFullyDrained = default;
        private Action onDrain = default;
        private Action onRecover = default;
        private CharacterStatsBar statsBarUI = default;
        private Color statsBarUIBackgroundColor = Color.white;
        private Color statsBarUIFillColor = Color.red;

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

        public CharacterStatsBar StatsBarUI { get => this.statsBarUI; set => this.statsBarUI = value; }

        public Color StatsBarUIBackgroundColor { get => this.statsBarUIBackgroundColor; set => this.statsBarUIBackgroundColor = value; }

        public Color StatsBarUIFillColor { get => this.statsBarUIFillColor; set => this.statsBarUIFillColor = value; }

        public StatsType Type { get => type; set => type = value; }

        public CharacterStatsEntry(StatsType type, float currentValue, float maxValue, float minValue, float recoverRate)
        {
            this.type = type;
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.recoverRate = recoverRate;
        }
    }

    [Serializable]
    public class CharacterHealthStatsEntry : CharacterStatsEntry
    {
        public CharacterHealthStatsEntry(float currentValue, float maxValue, float minValue, float recoverRate)
            : base(StatsType.Health, currentValue, maxValue, minValue, recoverRate)
        {
            Color.RGBToHSV(Color.red, out float h, out float s, out float v);
            s = 0.5f;
            this.StatsBarUIFillColor = Color.HSVToRGB(h, s, v);
        }
    }

    [Serializable]
    public class CharacterStaminaStatsEntry : CharacterStatsEntry
    {
        public CharacterStaminaStatsEntry(float currentValue, float maxValue, float minValue, float recoverRate)
            : base(StatsType.Stamina, currentValue, maxValue, minValue, recoverRate)
        {
            Color.RGBToHSV(Color.green, out float h, out float s, out float v);
            s = 0.5f;
            this.StatsBarUIFillColor = Color.HSVToRGB(h, s, v);
        }
    }

    [Serializable]
    public class CharacterHungerStatsEntry : CharacterStatsEntry
    {
        public CharacterHungerStatsEntry(float currentValue, float maxValue, float minValue, float recoverRate)
            : base(StatsType.Hunger, currentValue, maxValue, minValue, recoverRate)
        {
            Color.RGBToHSV(Color.yellow, out float h, out float s, out float v);
            s = 0.5f;
            this.StatsBarUIFillColor = Color.HSVToRGB(h, s, v);
        }
    }
}
