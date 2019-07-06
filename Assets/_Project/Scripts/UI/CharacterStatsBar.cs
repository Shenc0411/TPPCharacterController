namespace Lake.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CharacterStatsBar : MonoBehaviour
    {
        [SerializeField]
        private Slider slider = default;
        [SerializeField]
        private Image backgroundImage = default;
        [SerializeField]
        private Image fillImage = default;
        private CharacterStatsPanel owner = default;

        public Image BackgroundImage { get => this.backgroundImage; }

        public Image FillImage { get => this.fillImage; }

        public CharacterStatsPanel Owner { get => this.owner; }

        public Slider Slider { get => this.slider; }
    }
}