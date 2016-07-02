using UnityEngine;
using UnityEngine.UI;

namespace KiritanAction.UI {
    /// <summary>
    /// 直線砲撃用ゲージ
    /// gauge for crawl mode
    /// </summary>
    public class CrawlPowerGauge : MonoBehaviour{

        //  発射アクションへのアセット参照
        //  Asset reference for blust off
        public ConcleteAction.LinearCannon LinearCannon;

        /// <summary>
        /// きりたんへの参照を取得または設定します
        /// get and set Kiritan
        /// </summary>
        public Kiritan Kiritan { get; set; }

        //  表示バーへの参照
        //  bar image
        private Image bar { get; set; }

        protected void Awake() {
            bar = transform.FindChild("Gauge").FindChild("Bar").GetComponent<Image>();
        }

        protected void Update() {
            float amount = Kiritan.CannonState.CannonModeCharge / (float)LinearCannon.MaxChargeFrame;
            if (amount >= 1f) {
                amount = 1f;
            }
            bar.fillAmount = amount;
        }


    }
}
