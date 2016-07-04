using UnityEngine;
using UnityEngine.UI;

namespace KiritanAction.UI {
    /// <summary>
    /// ライフゲージ(motivation)
    /// life gauge
    /// </summary>
    public class LifeGauge : MonoBehaviour{

        /// <summary>
        /// get and set related life component
        /// </summary>
        public Life Life { get; set; }

        //  表示バーへの参照
        //  bar image
        private Image bar { get; set; }

        protected void Awake() {
            bar = transform.FindChild("Gauge").FindChild("Bar").GetComponent<Image>();
        }

        protected void Update() {
            bar.fillAmount = Life.Current / (float)Life.Max;
        }
    }
}
