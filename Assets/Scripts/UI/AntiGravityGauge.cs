using UnityEngine;
using UnityEngine.UI;

namespace KiritanAction.UI {
    /// <summary>
    /// 反重力ジャンプ用ゲージ
    /// gauge for jump with cannon
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AntiGravityGauge : MonoBehaviour{

        //  反重力ジャンプアクションへのアセット参照
        //  Asset reference for jump with cannon
        public ConcleteAction.AntiGravityJump AntiGravityJump;

        /// <summary>
        /// きりたんへの参照を取得または設定します
        /// set and get Kiritan
        /// </summary>
        public Kiritan Kiritan { get; set; }

        //  表示バーへの参照
        //  bar image
        private Image bar { get; set; }

        //  Ready表示
        //  "Ready" text object
        private GameObject ready { get; set; }

        //  チャージ完了時の効果音
        //  sound for full charge
        private AudioSource chargeOk { get; set; }

        protected void Awake() {
            bar = transform.FindChild("Gauge").FindChild("Bar").GetComponent<Image>();
            ready = transform.FindChild("Gauge").FindChild("Ready").gameObject;
            chargeOk = GetComponent<AudioSource>();
        }

        protected void Update() {
            float amount = Kiritan.CannonState.AntiGravityChargeFrame / (float)AntiGravityJump.ChargeFrame;
            if (amount >= 1f) {
                ready.SetActive(true);
                amount = 1f;
            }
            else {
                ready.SetActive(false);
            }
            bar.fillAmount = amount;
        }

        protected void FixedUpdate() {
            if (Kiritan.CannonState.AntiGravityChargeFrame == AntiGravityJump.ChargeFrame) {
                chargeOk.Play();
            }
        }
    }
}
