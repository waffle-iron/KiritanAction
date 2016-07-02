using KiritanAction.Sound;
using UnityEngine;

namespace KiritanAction.ConcleteAction {

    /// <summary>
    /// 放物投射弾発射
    /// blast off bullet under the influence of gravity
    /// </summary>
    [CreateAssetMenu(fileName = "Grenade", menuName = "ScriptableObject/Action/Grenade")]
    public class Grenade : Action {

        //  消費エネルギー
        //  energy consumption
        public float Energy;

        //  発射角度
        //  initial velocity angle
        public float Angle;

        //  最大タメまでのフレーム数
        //  frame count when max charged
        public int MaxChargeFrame;

        //  最小射出パワー
        //  magnitude of bullet velocity when minimum charged
        public float MinPower;

        //  最大タメ時の射出パワー
        //  magnitude of bullet velocity when max charged
        public float MaxPower;

        //  反動パワー
        //  magnitude of recoil velocity when minimum charged
        public float MinRecoilPower;

        //  最大溜め時の反動パワー
        //  magnitude of recoil velocity when max charged
        public float MaxRecoilPower;

        public GameObject BulletPrefab;

        //  発射元のオブジェクトのTransform
        //  transform of bullet emitter
        private Transform emitterTransform { get; set; }

        public override void Init(Kiritan kiritan, FixedInputController inputController) {
            base.Init(kiritan, inputController);

            emitterTransform = kiritan.transform.FindChild("BulletEmitter").FindChild("Stance");
        }

        public override bool Trigger() {
            if (input.InputButtonTable["Fire"].ReleasedFrame != 1) return false;
            if (kiritan.Energy.Current < float.Epsilon) return false;
            return true;
        }

        public override void OnTrigger() {
            //  射出溜め率を取得
            //  get charge ratio
            float ratio = kiritan.CannonState.StanceCharge / (float)MaxChargeFrame;
            if (ratio > 1f) ratio = 1f;

            //  反動を適用
            //  apply recoil
            float recoil = - ((MaxRecoilPower - MinRecoilPower) * ratio + MinRecoilPower) * Mathf.Sin(Angle * Mathf.Deg2Rad) * kiritan.Direction.ToFloat();
            kiritan.RigidbodyCache.velocity = new Vector2(kiritan.RigidbodyCache.velocity.x + recoil, kiritan.RigidbodyCache.velocity.y);

            //  ジャンプしたことにする(摩擦を無視するため)
            //  set grounded as aerial (for ignore friction)
            kiritan.OnGroundFrame = 1;

            //  エネルギーを減らす
            //  consume energy
            kiritan.Energy.Current -= Energy;

            //  弾を生成
            //  blust off
            GameObject bullet = GameObject.Instantiate<GameObject>(BulletPrefab);
            Vector2 velocity = new Vector2(
                x: ((MaxPower - MinPower) * ratio + MinPower) * Mathf.Sin(Angle * Mathf.Deg2Rad) * kiritan.Direction.ToFloat(),
                y: ((MaxPower - MinPower) * ratio + MinPower) * Mathf.Cos(Angle * Mathf.Deg2Rad));
            bullet.GetComponent<GrenadeBullet>().Init(emitterTransform.position, velocity);

            //  パーティクルの発生
            //  emit particle
            ParticleSystem cannonParticle = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particles/CannonParticle1")).GetComponent<ParticleSystem>();
            Presentation.CannonParticle1 cannonParticle1 = cannonParticle.transform.GetComponent<Presentation.CannonParticle1>();
            cannonParticle1.SetPosition(kiritan.MainCannonParticleEmitter.position);
            cannonParticle1.SetAngleZ(- 30f * kiritan.Direction.ToFloat());
            cannonParticle.Play();

            //  効果音
            //  play sound
            kiritan.PlaySE("Cannon1");

            //  アニメーション再生
            //  play cannon motion
            kiritan.MainCannonAnimator.SetTrigger("Fire");

        }
    }
}
