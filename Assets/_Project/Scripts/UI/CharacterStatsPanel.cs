namespace Lake.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.Character;

    public class CharacterStatsPanel : MonoBehaviour
    {
        [SerializeField]
        private Character targetCharacter = default;
        private CharacterStatus targetCharacterStats = default;
        private Dictionary<NumericalStatus, CharacterStatsBar> characterStatsBarMap = new Dictionary<NumericalStatus, CharacterStatsBar>();

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
            foreach (KeyValuePair<NumericalStatus, CharacterStatsBar> pair in this.characterStatsBarMap)
            {
                CharacterStatsBar bar = pair.Value;
                NumericalStatus entry = pair.Key;

                bar.Slider.value = entry.CurrentValue / entry.MaxValue;
            }
        }

        private void SetUpTargetCharacter(Character target)
        {
            foreach (KeyValuePair<NumericalStatus, CharacterStatsBar> pair in this.characterStatsBarMap)
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

            foreach (KeyValuePair<NumericalStatus.StatsType, NumericalStatus> pair in this.targetCharacterStats.NumericalStatusMap)
            {
                CharacterStatsBar bar = Instantiate(UIPrefabReferenceManager.Instance.CharacterStatsBarPrefab, this.transform).GetComponent<CharacterStatsBar>();
                NumericalStatus numericalStatus = pair.Value;
                bar.BackgroundImage.color = UIPrefabReferenceManager.Instance.DefaultNumericalStatusBackgroundColor;

                if (UIPrefabReferenceManager.Instance.NumericalStatusColorMap.ContainsKey(numericalStatus.Type))
                {
                    bar.FillImage.color = UIPrefabReferenceManager.Instance.NumericalStatusColorMap[numericalStatus.Type];
                }
                else
                {
                    bar.FillImage.color = UIPrefabReferenceManager.Instance.DefaultNumericalStatusFillColor;
                }
                
                bar.Slider.value = numericalStatus.CurrentValue / numericalStatus.MaxValue;
                this.characterStatsBarMap.Add(numericalStatus, bar);
            }
        }
    }
}