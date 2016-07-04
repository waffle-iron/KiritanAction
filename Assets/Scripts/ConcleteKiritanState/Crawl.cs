using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction.ConcleteKiritanState {
    /// <summary>
    /// 四つんばい状態
    /// crawl state
    /// </summary>
    [CreateAssetMenu(fileName="Crawl", menuName="ScriptableObject/KiritanState/Crawl")]
    public class Crawl : KiritanState {

        /// <summary>
        /// 砲撃アクションへのアセット参照
        /// Asset reference for blust off action
        /// </summary>
        public ConcleteAction.LinearCannon LinearCannon;

        //  Powerゲージオブジェクト
        //  Power gauge
        private GameObject crawlPowerGaugeObj { get; set; }

        //  通常状態のColliderパラメータ
        //  collider parameters for normal state
        private Vector2 normalBoxColliderOffset { get; set; }
        private Vector2 normalBoxColliderSize { get; set; }
        private Vector2 normalTopCircleColliderOffset { get; set; }
        private float normalTopCircleColliderRadius { get; set; }
        private Vector2 normalBottomCircleColliderOffset { get; set; }
        private float normalBottomCircleColliderRadius { get; set; }

        private Vector2 normalDamageColliderOffset { get; set; }
        private Vector2 normalDamageColliderSize { get; set; }

        //  Crawl状態のColliderパラメータ
        //  collider parameters for crawl state
        private static readonly Vector2 crawlBoxColliderOffset = new Vector2(0f, 0.45f);
        private static readonly Vector2 crawlBoxColliderSize = new Vector2(0.72f, 0.9f);
        private static readonly Vector2 crawlTopCircleColliderOffset = new Vector2(0f, 0.54f);
        private static readonly float crawlTopCircleColliderRadius = 0.38f;
        private static readonly Vector2 crawlBottomCircleColliderOffset = new Vector2(0f, 0.36f);
        private static readonly float crawlBottomCircleColliderRadius = 0.38f;

        private static readonly Vector2 crawlDamageColliderOffset = new Vector2(0f, 0.45f);
        private static readonly Vector2 crawlDamageColliderSize = new Vector2(0.6f, 0.9f);

        //  きりたんのColliderへの参照キャッシュ
        //  cache of collider component
        private BoxCollider2D boxCollider { get; set; }
        private CircleCollider2D topCircleCollider { get; set; }
        private CircleCollider2D bottomCircleCollider { get; set; }

        private BoxCollider2D damageCollider { get; set; }

        //  メインきりたん砲(アニメーションが適用されない複製)への参照
        //  cannon presenter object depend on Kiritan animation
        private GameObject mainCannon { get; set; }

        //  メインきりたん砲(アニメーションが適用される本物)への参照
        //  cannon presenter object connected with Kiritan animation
        private GameObject animationCannon { get; set; }

        //  チャージ中に発生させるパーティクル
        //  particle for charging
        private ParticleSystem cannonChargeParticle { get; set; }

        //  チャージ音
        //  sound for charging
        private AudioSource audioSource { get; set; }
        private float audioPitchMax { get; set; }
        private float audioPitchMin { get; set; }

        public override void Init(Kiritan kiritan, FixedInputController inputController) {
            base.Init(kiritan, inputController);

            //  ゲージオブジェクトへの参照を取得
            //  get reference of gauge
            crawlPowerGaugeObj = GameObject.FindGameObjectWithTag("CrawlPowerGauge");
            crawlPowerGaugeObj.SetActive(false);

            //  通常状態の当たり判定のパラメータを記録
            //  cache collider parameters for normal state
            var boxColliders = kiritan.transform.GetComponents<BoxCollider2D>();
            boxCollider = boxColliders[0];
            normalBoxColliderOffset = boxCollider.offset;
            normalBoxColliderSize = boxCollider.size;
            CircleCollider2D[] circleColliders = kiritan.transform.GetComponents<CircleCollider2D>();
            topCircleCollider = circleColliders[0];
            normalTopCircleColliderOffset = circleColliders[0].offset;
            normalTopCircleColliderRadius = circleColliders[0].radius;
            bottomCircleCollider = circleColliders[1];
            normalBottomCircleColliderOffset = circleColliders[1].offset;
            normalBottomCircleColliderRadius = circleColliders[1].radius;
            damageCollider = boxColliders[1];
            normalDamageColliderOffset = damageCollider.offset;
            normalDamageColliderSize = damageCollider.size;

            //  メインきりたん砲への参照を取得
            //  get reference of cannon presenter
            animationCannon = kiritan.transform.FindChild("Body").FindChild("Cannons").gameObject;
            mainCannon = kiritan.transform.FindChild("CannonsPresenter").gameObject;
            cannonChargeParticle = mainCannon.transform.FindChild("MainCannon").FindChild("ParticleEmitter").FindChild("CannonCharge").GetComponent<ParticleSystem>();
            mainCannon.SetActive(false);    //  非表示に

            audioSource = kiritan.transform.FindChild("AudioSources").FindChild("Charge").GetComponent<AudioSource>();
            audioPitchMax = 2f;
            audioPitchMin = 1f;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();

            //  ゲージを見えるようにする
            //  set gauge as visible
            crawlPowerGaugeObj.SetActive(true);

            //  当たり判定をcrawl用のものに差し替える
            //  switch collider parameter for crawl mode
            boxCollider.offset = crawlBoxColliderOffset;
            boxCollider.size = crawlBoxColliderSize;
            topCircleCollider.offset = crawlTopCircleColliderOffset;
            topCircleCollider.radius = crawlTopCircleColliderRadius;
            bottomCircleCollider.offset = crawlBottomCircleColliderOffset;
            bottomCircleCollider.radius = crawlBottomCircleColliderRadius;
            damageCollider.offset = crawlDamageColliderOffset;
            damageCollider.size = crawlDamageColliderSize;

            //  きりたん砲の表示をアニメーションしないように切り替える
            //  switch cannon presenter
            animationCannon.SetActive(false);
            mainCannon.SetActive(true);

            //  きりたん砲の角度を90度に設定する
            //  set cannon angle as front
            kiritan.CannonState.CannonModeAngle = -90f;
        }

        public override void OnStateExit() {
            base.OnStateExit();

            //  ゲージを見えないようにする
            //  set gauge as invisible
            crawlPowerGaugeObj.SetActive(false);

            //  当たり判定を通常用に差し替える
            //  switch collider parameter for normal mode
            boxCollider.offset = normalBoxColliderOffset;
            boxCollider.size = normalBoxColliderSize;
            topCircleCollider.offset = normalTopCircleColliderOffset;
            topCircleCollider.radius = normalTopCircleColliderRadius;
            bottomCircleCollider.offset = normalBottomCircleColliderOffset;
            bottomCircleCollider.radius = normalBottomCircleColliderRadius;
            damageCollider.offset = normalDamageColliderOffset;
            damageCollider.size = normalDamageColliderSize;

            cannonChargeParticle.Stop();
            cannonChargeParticle.Clear();
            audioSource.Stop();

            //  きりたん砲の表示を通常に切り替える
            //  switch cannon presenter
            animationCannon.SetActive(true);
            mainCannon.SetActive(false);
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            //  チャージ
            //  charge
            if (inputController.InputButtonTable["Fire"].PressedFrame > 0) {
                ++kiritan.CannonState.CannonModeCharge;
                float ratio = kiritan.CannonState.CannonModeCharge / (float)LinearCannon.MaxChargeFrame;
                if (ratio > 1f) ratio = 1f;
                audioSource.pitch = audioPitchMin + (audioPitchMax - audioPitchMin) * ratio;
                if (!cannonChargeParticle.isPlaying) {
                    cannonChargeParticle.Play();
                }
                if (!audioSource.isPlaying) {
                    audioSource.Play();
                }
            }
            else {
                kiritan.CannonState.CannonModeCharge = 0;
                if (cannonChargeParticle.isPlaying) {
                    cannonChargeParticle.Stop();
                    cannonChargeParticle.Clear();
                }
                if (audioSource.isPlaying) {
                    audioSource.Stop();
                }
            }

            //  きりたん砲角度の適用
            //  apply cannon presenter angle
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0f, 0f, kiritan.CannonState.CannonModeAngle);
            mainCannon.transform.localRotation = rotation;

            //  空中ならばNormal状態へ移行する
            //  transition to normal state if Kiritan is not grounded 
            if (!kiritan.IsGround) {
                kiritan.TransitionState(KiritanStateEnum.Normal);
            }
        }
    }
}
