using UnityEngine;
using UnityEngine.UI;

namespace KiritanAction.UI {
    /// <summary>
    /// エネルギーゲージ
    /// energy gauge
    /// </summary>
    public class EnergyGauge : MonoBehaviour{

        /// <summary>
        /// きりたんへの参照を取得または設定します
        /// set and get Kiritan
        /// </summary>
        public Kiritan Kiritan { get; set; }

        //  表示バーへの参照
        //  bar image
        private Image bar { get; set; }

        protected void Awake() {
            bar = transform.FindChild("Gauge").FindChild("Bar").GetComponent<Image>();
        }

        protected void Update() {
            bar.fillAmount = Kiritan.Energy.GetRatio();
        }
    }
}
