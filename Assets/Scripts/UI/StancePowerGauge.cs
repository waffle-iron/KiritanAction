using UnityEngine;
using UnityEngine.UI;

namespace KiritanAction.UI {
    /// <summary>
    /// グレネード用ゲージ
    /// gauge for stance mode
    /// </summary>
    public class StancePowerGauge : MonoBehaviour{

        //  発射アクションへのアセット参照
        //  Asset reference for blust off
        public ConcleteAction.Grenade Grenade;

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
            float amount = Kiritan.CannonState.StanceCharge / (float)Grenade.MaxChargeFrame;
            if (amount >= 1f) {
                amount = 1f;
            }
            bar.fillAmount = amount;
        }


    }
}
