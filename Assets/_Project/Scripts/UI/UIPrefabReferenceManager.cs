namespace Lake.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.Utilities;

    public class UIPrefabReferenceManager : SingletonBehaviour<UIPrefabReferenceManager>
    {
        [SerializeField]
        private GameObject characterStatsPanelPrefab = default;

        [SerializeField]
        private GameObject characterStatsBarPrefab = default;

        public GameObject CharacterStatsPanelPrefab { get => this.characterStatsPanelPrefab; }

        public GameObject CharacterStatsBarPrefab { get => this.characterStatsBarPrefab; }
    }

}