using UnityEngine;

namespace KiritanAction.ConcleteAction {
    /// <summary>
    /// 反重力ジャンプ
    /// Jump with cannon
    /// </summary>
    [CreateAssetMenu(fileName="AntiGravityJump", menuName="ScriptableObject/Action/AntiGravityJump")]
    public class AntiGravityJump : Action{

        //  反重力ジャンプに必要な入力フレーム数
        //  frame count required
        public int ChargeFrame;

        //  反重力ジャンプ角度
        //  jump angle
        public float Angle;

        //  ジャンプ力
        //  magnitude of initial velocity
        public float Power;

        //  消費エネルギー
        //  amount of energy consumption
        public float Energy;

        //  攻撃判定Prefab
        //  Prefab of attack collider object
        public GameObject AttackPrefab;

        //  攻撃判定発生源
        //  Emitter for attack collider object
        private Transform attackEmitter { get; set; }

        public override void Init(Kiritan kiritan, FixedInputController inputController) {
            base.Init(kiritan, inputController);

            attackEmitter = kiritan.transform.FindChild("BulletEmitter").FindChild("AntiGravity");
        }

        /// <summary>
        /// 地上
        /// Jump入力が放されたとき
        /// Energyが少しでもある
        /// AntiGravityゲージが十分に溜まっている
        /// 
        /// is grounded
        /// Jump input is released
        /// energy > 0
        /// Anti-Gravity Gauge is filled
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (kiritan.OnGroundFrame <= 1) return false;
            if (input.InputButtonTable["Jump"].ReleasedFrame != 1) return false;
            if (kiritan.Energy.Current < float.Epsilon) return false;
            if (kiritan.CannonState.AntiGravityChargeFrame >= ChargeFrame) return true;
            return false;
        }

        /// <summary>
        /// 反重力ジャンプ
        /// ジャンプ速度を現在の速度に加算する
        /// Add jump velocity
        /// </summary>
        public override void OnTrigger() {
            //  速度変更
            //  change velocity
            float angleRad = Angle * Mathf.Deg2Rad;
            Vector2 velocity = new Vector2(Mathf.Sin(angleRad) * kiritan.Direction.ToFloat(), Mathf.Cos(angleRad));
            velocity *= Power;
            kiritan.RigidbodyCache.velocity += velocity;

            //  エネルギー消費
            //  energy consumption
            kiritan.Energy.Current -= Energy;

            //  反重力チャージを0に設定
            //  set charge amount as 0
            kiritan.CannonState.AntiGravityChargeFrame = 0;

            //  攻撃判定の発生
            //  emit attack collider object
            GameObject attack = GameObject.Instantiate<GameObject>(AttackPrefab);
            attack.transform.position = attackEmitter.position;

            //  プレゼンターの再生
            //  play cannon motion
            kiritan.MainCannonAnimator.SetTrigger("Fire");

            //  パーティクルを発生させる
            //  emit particles
            ParticleSystem cannonParticle = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particles/CannonParticle1")).GetComponent<ParticleSystem>();
            Presentation.CannonParticle1 cannonParticle1 = cannonParticle.transform.GetComponent<Presentation.CannonParticle1>();
            cannonParticle1.SetPosition(kiritan.MainCannonParticleEmitter.position);
            cannonParticle1.SetAngleZ(180f - 30f * kiritan.Direction.ToFloat());
            cannonParticle.Play();

            kiritan.PlaySE("Cannon1");

            //  アニメーションフラグ
            //  send flag to animator
            kiritan.AntiGravityJumpFlag = true;
        }
    }
}
