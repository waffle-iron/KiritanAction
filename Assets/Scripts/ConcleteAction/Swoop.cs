using UnityEngine;

namespace KiritanAction.ConcleteAction {
    /// <summary>
    /// 急降下
    /// Swoop with cannon
    /// </summary>
    [CreateAssetMenu(fileName= "Swoop", menuName= "ScriptableObject/Action/Swoop")]
    public class Swoop : Action{

        //  急降下速度
        //  swoop speed
        public float Speed;

        //  消費エネルギー
        //  energy consumption
        public float Energy;

        //  攻撃判定Prefab
        //  Prefab of attack collider object 
        public GameObject AttackPrefab;

        //  攻撃判定発生源
        //  Emitter for attack collider object
        private Transform attackEmitter { get; set; }

        public override void Init(Kiritan kiritan, FixedInputController inputController) {
            base.Init(kiritan, inputController);

            attackEmitter = kiritan.transform.FindChild("BulletEmitter").FindChild("Swoop");
        }

        /// <summary>
        /// 空中
        /// Jump入力が入力された時
        /// Down入力中
        /// Energyが少しでもある
        /// 
        /// is not grounded
        /// jump button pressed and doen button pressing
        /// energy > 0
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (kiritan.OnGroundFrame > 0) return false;
            if (input.InputButtonTable["Jump"].PressedFrame != 1) return false;
            if (input.InputButtonTable["Down"].PressedFrame == 0) return false;
            if (kiritan.Energy.Current < float.Epsilon) return false;
            return true;
        }

        /// <summary>
        /// 急降下
        /// y速度を固定値にする
        /// </summary>
        public override void OnTrigger() {
            //  速度変更
            kiritan.RigidbodyCache.velocity = new Vector2(kiritan.RigidbodyCache.velocity.x, - Speed);

            //  エネルギー消費
            kiritan.Energy.Current -= Energy;

            //  攻撃判定の発生
            GameObject attack = GameObject.Instantiate<GameObject>(AttackPrefab);
            attack.transform.position = attackEmitter.position;

            //  プレゼンターの再生
            kiritan.MainCannonAnimator.SetTrigger("Fire");

            //  パーティクルを発生させる
            ParticleSystem cannonParticle = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particles/CannonParticle1")).GetComponent<ParticleSystem>();
            Presentation.CannonParticle1 cannonParticle1 = cannonParticle.transform.GetComponent<Presentation.CannonParticle1>();
            cannonParticle1.SetPosition(kiritan.MainCannonParticleEmitter.position);
            cannonParticle1.SetAngleZ(0f);
            cannonParticle.Play();

            kiritan.PlaySE("Cannon1");
        }
    }
}
