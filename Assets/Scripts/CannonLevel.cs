using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction {

    /// <summary>
    /// きりたん砲の状態
    /// level of cannon
    /// </summary>
    [CreateAssetMenu(fileName="NewCannonLevel", menuName="ScriptableObject/CannonLevel")]
    public class CannonLevel : ScriptableObject {

        /// <summary>
        /// UIに表示する名前
        /// name for display
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// UIに表示する名前の色
        /// text color for display
        /// </summary>
        public Color DisplayColor;

        public Kiritan Kiritan { get; set; }
    }
}
