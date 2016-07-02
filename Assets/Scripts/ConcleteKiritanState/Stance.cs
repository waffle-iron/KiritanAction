using KiritanAction.Sound;
using UnityEngine;

namespace KiritanAction.ConcleteKiritanState {
    /// <summary>
    /// 立ち構え状態
    /// stance mode
    /// </summary>
    [CreateAssetMenu(fileName="Stance", menuName="ScriptableObject/KiritanState/Stance")]
    public class Stance : KiritanState{

        /// <summary>
        /// 砲撃アクションへのアセット参照
        /// Asset reference for blust off action
        /// </summary>
        public ConcleteAction.Grenade Grenade;

        //  Powerゲージオブジェクト
        //  Power gauge
        private GameObject stancePowerGaugeObj { get; set; }

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

            stancePowerGaugeObj = GameObject.FindGameObjectWithTag("StancePowerGauge");
            stancePowerGaugeObj.SetActive(false);

            cannonChargeParticle = kiritan.MainCannonParticleEmitter.FindChild("CannonCharge").GetComponent<ParticleSystem>();

            audioSource = kiritan.transform.FindChild("AudioSources").FindChild("Charge").GetComponent<AudioSource>();
            audioPitchMax = 2f;
            audioPitchMin = 1f;
        }

        public override void OnStateEnter() {
            base.OnStateEnter();

            stancePowerGaugeObj.SetActive(true);
        }

        public override void OnStateExit() {
            base.OnStateExit();

            stancePowerGaugeObj.SetActive(false);

            cannonChargeParticle.Stop();
            cannonChargeParticle.Clear();
            audioSource.Stop();
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            //  チャージ
            //  charge
            if (inputController.InputButtonTable["Fire"].PressedFrame > 0) {
                ++ kiritan.CannonState.StanceCharge;
                float ratio = kiritan.CannonState.StanceCharge / (float)Grenade.MaxChargeFrame;
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
                kiritan.CannonState.StanceCharge = 0;
                if (cannonChargeParticle.isPlaying) {
                    cannonChargeParticle.Stop();
                    cannonChargeParticle.Clear();
                }
                if (audioSource.isPlaying) {
                    audioSource.Stop();
                }
            }

            //  空中ならばNormal状態へ移行する
            //  transition to normal state if Kiritan is not grounded 
            if (!kiritan.IsGround) {
                kiritan.TransitionState(KiritanStateEnum.Normal);
            }
        }
    }
}
