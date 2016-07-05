using UnityEngine;

namespace KiritanAction.ConcleteAction {
    /// <summary>
    /// 直線砲発射
    /// Blust off bullet to linear motion. 
    /// </summary>
    [CreateAssetMenu(fileName="LinearCannon", menuName="ScriptableObject/Action/LinearCannon")]
    public class LinearCannon : Action {

        //  消費エネルギー
        //  energy consumption
        public float Energy;

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

        //  パーティクルの発生元
        //  transform of particle emitter
        private Transform particleEmitter { get; set; }

        //  砲撃時のきりたん砲アニメーションコントローラ
        //  animator that has cannon motion
        private Animator cannonAnimator { get; set; }

        public override void Init(Kiritan kiritan, FixedInputController inputController) {
            base.Init(kiritan, inputController);

            emitterTransform = kiritan.transform.FindChild("CannonsPresenter").FindChild("MainCannon").FindChild("BulletEmitter");
            particleEmitter = kiritan.transform.FindChild("CannonsPresenter").FindChild("MainCannon").FindChild("ParticleEmitter");
            cannonAnimator = kiritan.transform.FindChild("CannonsPresenter").FindChild("MainCannon").GetComponent<Animator>();
        }

        public override bool Trigger() {
            if (input.InputButtonTable["Fire"].ReleasedFrame != 1) return false;
            if (kiritan.Energy.Current < float.Epsilon) return false;
            return true;
        }

        public override void OnTrigger() {
            float angle = - kiritan.CannonState.CannonModeAngle;
            //  射出溜め率を取得
            //  get charge ratio
            float ratio = kiritan.CannonState.CannonModeCharge / (float)MaxChargeFrame;
            if (ratio > 1f) ratio = 1f;

            //  反動を適用
            //  apply recoil
            float recoil = -((MaxRecoilPower - MinRecoilPower) * ratio + MinRecoilPower) * Mathf.Sin(angle * Mathf.Deg2Rad) * kiritan.Direction.ToFloat();
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
            bullet.transform.SetParent(GameObject.FindGameObjectWithTag("InstantObjectContainer").transform);
            Vector2 velocity = new Vector2(
                x: ((MaxPower - MinPower) * ratio + MinPower) * Mathf.Sin(angle * Mathf.Deg2Rad) * kiritan.Direction.ToFloat(),
                y: ((MaxPower - MinPower) * ratio + MinPower) * Mathf.Cos(angle * Mathf.Deg2Rad));
            bullet.GetComponent<LinearBullet>().Init(emitterTransform.position, velocity);

            //  パーティクルの発生
            //  emit particle
            ParticleSystem cannonParticle = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particles/CannonParticle1")).GetComponent<ParticleSystem>();
            cannonParticle.transform.SetParent(GameObject.FindGameObjectWithTag("InstantObjectContainer").transform);
            Presentation.CannonParticle1 cannonParticle1 = cannonParticle.transform.GetComponent<Presentation.CannonParticle1>();
            cannonParticle1.SetPosition(particleEmitter.position);
            cannonParticle1.SetAngleZ(- angle * kiritan.Direction.ToFloat());
            cannonParticle.Play();

            //  効果音
            //  play sound
            kiritan.PlaySE("Cannon1");

            //  アニメーション再生
            //  play cannon motion
            cannonAnimator.SetTrigger("Fire");

        }
    }
}
