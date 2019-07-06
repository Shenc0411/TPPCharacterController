namespace Lake.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake;

    public class CharacterStatsPanel : MonoBehaviour
    {
        [SerializeField]
        private Character targetCharacter = default;
        private CharacterStats targetCharacterStats = default;
        private Dictionary<CharacterStatsEntry, CharacterStatsBar> characterStatsBarMap = new Dictionary<CharacterStatsEntry, CharacterStatsBar>();

        public Character TargetCharacter
        {
            get => this.targetCharacter;
            set
            {
                if (this.targetCharacter != value)
                {
                    SetUpTargetCharacter(value);
                }
            }
        }

        private void Start()
        {
            this.SetUpTargetCharacter(this.targetCharacter);
        }

        private void Update()
        {
            this.UpdateBars();
        }

        private void UpdateBars()
        {
            foreach (KeyValuePair<CharacterStatsEntry, CharacterStatsBar> pair in this.characterStatsBarMap)
            {
                CharacterStatsBar bar = pair.Value;
                CharacterStatsEntry entry = pair.Key;

                bar.Slider.value = entry.CurrentValue / entry.MaxValue;
            }
        }

        private void SetUpTargetCharacter(Character target)
        {
            foreach (KeyValuePair<CharacterStatsEntry, CharacterStatsBar> pair in this.characterStatsBarMap)
            {
                Destroy(pair.Value.gameObject);
            }
            this.characterStatsBarMap.Clear();

            this.targetCharacter = target;

            if (this.targetCharacter == null)
            {
                return;
            }

            this.targetCharacterStats = this.targetCharacter.CharacterStats;

            foreach (KeyValuePair<StatsType, CharacterStatsEntry> pair in this.targetCharacterStats.StatsEntries)
            {
                CharacterStatsBar bar = Instantiate(UIPrefabReferenceManager.Instance.CharacterStatsBarPrefab, this.transform).GetComponent<CharacterStatsBar>();
                CharacterStatsEntry entry = pair.Value;
                bar.BackgroundImage.color = entry.StatsBarUIBackgroundColor;
                bar.FillImage.color = entry.StatsBarUIFillColor;
                bar.Slider.value = entry.CurrentValue / entry.MaxValue;
                this.characterStatsBarMap.Add(entry, bar);
            }
        }
    }
}