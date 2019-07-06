namespace Lake.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.Utilities;
    using Lake.Character;

    public class UIPrefabReferenceManager : SingletonBehaviour<UIPrefabReferenceManager>
    {
        [SerializeField]
        private GameObject characterStatsPanelPrefab = default;

        [SerializeField]
        private GameObject characterStatsBarPrefab = default;

        [SerializeField]
        private List<NumericalStatusColorConfiguration> numericalStatusColorConfigurations = new List<NumericalStatusColorConfiguration>();
        private Dictionary<NumericalStatus.StatsType, Color> numericalStatusColorMap = new Dictionary<NumericalStatus.StatsType, Color>();
        [SerializeField]
        private Color defaultNumericalStatusBackgroundColor = Color.white;
        [SerializeField]
        private Color defaultNumericalStatusFillColor = Color.red;

        public GameObject CharacterStatsPanelPrefab { get => this.characterStatsPanelPrefab; }

        public GameObject CharacterStatsBarPrefab { get => this.characterStatsBarPrefab; }

        public Dictionary<NumericalStatus.StatsType, Color> NumericalStatusColorMap { get => this.numericalStatusColorMap; }

        public Color DefaultNumericalStatusBackgroundColor { get => this.defaultNumericalStatusBackgroundColor; }

        public Color DefaultNumericalStatusFillColor { get => this.defaultNumericalStatusFillColor; }

        protected override void Awake()
        {
            base.Awake();
            foreach(NumericalStatusColorConfiguration config in this.numericalStatusColorConfigurations)
            {
                this.numericalStatusColorMap.Add(config.key, config.value);
            }
        }

    }

    [System.Serializable]
    public struct NumericalStatusColorConfiguration
    {
        public NumericalStatus.StatsType key;
        public Color value;
    }

}