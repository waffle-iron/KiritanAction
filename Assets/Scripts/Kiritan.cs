using UnityEngine;
using UnityEngine.UI;
using KiritanAction.Sound;
using System.Collections.Generic;
using System.Linq;
using KiritanAction.Common;

namespace KiritanAction {

    /// <summary>
    /// きりたんクラス
    /// 生成のタイミングは Stage -> Kiritan
    /// 
    /// Kiritan
    /// Create order is Stage -> Kiritan
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(JudgeGround))]
    [RequireComponent(typeof(Life))]
    public class Kiritan : MonoBehaviour {

        //  きりたん砲レベル定義インスタンスへの参照
        //  cannon set asset
        public CannonSet CannonSet;

        //  きりたんの状態定義インスタンスへの参照
        //  state set asset
        public KiritanStateSet StateSet;

        //  着地から何フレーム間摩擦を無視するか
        //  Kiritan ignore friction if grounded frame count < IgnoreFrictionFrame
        public int IgnoreFrictionFrame;

        //  Particle emitter when the level is raised
        public GameObject LevelUpParticlePrefab;

        /// <summary>
        /// knockback direction angle when damaged (degree)
        /// </summary>
        public float KnockbackAngle;

        /// <summary>
        /// knockback velocity magnitude when damaged
        /// </summary>
        public float KnockbackPower;

        //  入力情報への参照
        //  input controller
        private FixedInputController inputController { get; set; }

        //  BoxColliderへのキャッシュ
        //  cache of collider
        private BoxCollider2D boxColliderCache { get; set; }

        //  接地判定コンポーネント
        //  judge ground component
        private JudgeGround judgeGround { get; set; }

        //  アニメーションコントローラ
        //  animation controller
        private Animator animator { get; set; }

        /// <summary>
        /// rigidbodyのキャッシュ
        /// cache of rigidbody
        /// </summary>
        public Rigidbody2D RigidbodyCache { get; private set; }

        /// <summary>
        /// きりたんの向き
        /// direction
        /// </summary>
        public ActorDirection Direction { get; protected set; }

        /// <summary>
        /// エネルギー(Miso)を取得または設定します
        /// set and get Energy
        /// </summary>
        public Energy Energy { get; set; }

        /// <summary>
        /// きりたん砲のレベルを取得します
        /// 0 ～ 4 特殊ケースで5を使用するかも
        /// level of cannon
        /// </summary>
        public int CannonLevel { get; set; }

        /// <summary>
        /// UI Text "Kiritan Cannon Level :"
        /// </summary>
        public Text CannonLevelText { get; set; }

        /// <summary>
        /// きりたんの状態を取得または設定します
        /// State of Kiritan
        /// </summary>
        public KiritanState State { get; private set; }

        /// <summary>
        /// きりたんが接地してからのフレーム数を取得または設定します
        /// frame count after Kiritan is grounded
        /// </summary>
        public int OnGroundFrame { get; set; }

        /// <summary>
        /// きりたんが壁に接触して壁キック可能であるかどうかを取得します
        /// 
        /// Is Kiritan touching wall?
        /// if touching, Kiritan can wall jump (cannon level >= 1)
        /// </summary>
        public bool IsTouchingWall { get; private set; }

        /// <summary>
        /// きりたんが壁に接触しているとき接触している壁
        /// direction of wall touching
        /// </summary>
        public ActorDirection TouchingDirection { get; private set; }

        /// <summary>
        /// きりたん砲の状態を取得します
        /// get cannon state
        /// </summary>
        public CannonState CannonState { get; private set; }

        /// <summary>
        /// メインきりたん砲のパーティクル発生ポジションを取得します
        /// get transform of cannon particle emitter
        /// </summary>
        public Transform MainCannonParticleEmitter { get; private set; }

        /// <summary>
        /// メインきりたん砲のアニメーションコントローラを取得します
        /// animation controller for cannon motion
        /// </summary>
        public Animator MainCannonAnimator { get; private set; }

        //  gameobject for present jet nozzle(Lv.2)
        private GameObject JetNozzleObject { get; set; }

        //  gameobject for present sub cannon(Lv.3)
        private GameObject SubCannonObject { get; set; }

        //  gameobject for present jet nozzle2(Lv.4)
        private GameObject JetNozzle2Object { get; set; }

        //  gameobject for present option unit(Lv.5)
        private GameObject OptionUnitObject { get; set; }

        //  life
        private Life life { get; set; }

        /// <summary>
        /// position to respone
        /// </summary>
        public Vector2 ResponePosition { private get; set; }

        /// <summary>
        /// 反重力ジャンプをしたかどうかのフラグ(Animatorへの通知に利用する)
        /// is jumped with cannon? (using animation control)
        /// </summary>
        public bool AntiGravityJumpFlag { get; set; }

        //  ボイスプレイヤー
        //  voice sound player
        private SoundPlayer voicePlayer { get; set; }

        //  サウンドエフェクト
        //  sound effect dictionary
        private Dictionary<string, AudioSource> soundEffects { get; set; }


        protected void Awake(){

            boxColliderCache = GetComponent<BoxCollider2D>();
            RigidbodyCache = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            judgeGround = GetComponent<JudgeGround>();

            //  エネルギーのインスタンス化
            //  instantiate energy
            Energy = ScriptableObject.CreateInstance<Energy>();

            //  get life component
            life = GetComponent<Life>();

            Direction = ActorDirection.Right;

            //  UIからきりたんへの参照を有効にする
            //  set Kiritan reference as this
            GameObject.FindGameObjectWithTag("EnergyGaugeContainer").GetComponent<UI.EnergyGauge>().Kiritan = this;
            GameObject.FindGameObjectWithTag("AntiGravityGauge").GetComponent<UI.AntiGravityGauge>().Kiritan = this;
            GameObject.FindGameObjectWithTag("StancePowerGauge").GetComponent<UI.StancePowerGauge>().Kiritan = this;
            GameObject.FindGameObjectWithTag("CrawlPowerGauge").GetComponent<UI.CrawlPowerGauge>().Kiritan = this;
            GameObject.FindGameObjectWithTag("MotivationGauge").GetComponent<UI.LifeGauge>().Life = life;

            //  resolve cannon level UI
            CannonLevel = 0;
            CannonLevelText = GameObject.FindGameObjectWithTag("LevelText").GetComponent<Text>();
            CannonLevelText.text = CannonSet.CurrentCannonLevel(this).DisplayName;
            CannonLevelText.color = CannonSet.CurrentCannonLevel(this).DisplayColor;

            //  サウンドエフェクト辞書の作成
            //  create sound effect dictionary
            soundEffects = transform.FindChild("AudioSources").GetComponentsInChildren<AudioSource>().ToDictionary(x => x.gameObject.name);

            //  きりたん砲のプレゼンターへの参照を解決する
            //  resolve cannon presenter
            MainCannonParticleEmitter = transform.FindChild("Body").FindChild("Cannons").FindChild("MainCannon").FindChild("ParticleEmitter").transform;
            MainCannonAnimator = transform.FindChild("Body").FindChild("Cannons").FindChild("MainCannon").GetComponent<Animator>();
            JetNozzleObject = transform.FindChild("Body").FindChild("Cannons").FindChild("JetNozzle").gameObject;
            JetNozzleObject.SetActive(false);
            SubCannonObject = transform.FindChild("Body").FindChild("Cannons").FindChild("SubCannon").gameObject;
            SubCannonObject.SetActive(false);
            JetNozzle2Object = transform.FindChild("Body").FindChild("Cannons").FindChild("JetNozzle2").gameObject;
            JetNozzle2Object.SetActive(false);
            OptionUnitObject = transform.FindChild("Body").FindChild("Cannons").FindChild("Option").gameObject;
            OptionUnitObject.SetActive(false);

            //  Stateを初期化する
            //  initialize states
            inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<FixedInputController>();
            voicePlayer = transform.FindChild("VoiceController").GetComponent<SoundPlayer>();
            foreach (KiritanState state in StateSet.States) {
                state.Init(this, inputController);
            }

            //  メインカメラがきりたんを追跡するように設定する
            //  camera start trace Kiritan
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PositionSync2D>().Target = transform;

            //  きりたんの状態を通常状態にする
            //  set Kiritan state as normal
            TransitionState(KiritanStateEnum.Normal);
            CannonState = new CannonState(this, inputController);

            //  ステージにきりたんの情報を伝える
            //  send kititan info to stage
            GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>().Init(this);
        }


        protected void FixedUpdate() {

            //  接地フレーム数計算
            //  calc OnGroundFrame
            if (IsGround) {
                ++OnGroundFrame;
                LandshapeCollider groundedCollider = judgeGround.RaycastHit.transform.GetComponent<LandshapeCollider>();
                //  摩擦計算
                //  Friction
                if (OnGroundFrame > IgnoreFrictionFrame) groundedCollider.AffectHorizontalFriction(RigidbodyCache);
            }
            else {
                OnGroundFrame = 0;
            }

            //  TODO:   接壁計算


            //  きりたん砲状態の更新
            //  update cannon state
            CannonState.OnFixedUpdate();

            //  きりたん状態に応じた処理
            //  process
            State.OnFixedUpdate();

            //  Animatorへの状態通知
            //  set animator parameters
            animator.SetBool("OnGround", IsGround);
            animator.SetInteger("OnGroundFrame", OnGroundFrame);
            animator.SetFloat("VelocityY", RigidbodyCache.velocity.y);
            animator.SetFloat("HorizontalInput", Mathf.Abs(Input.GetAxis("Horizontal")));
            animator.SetInteger("FireInputFrame", inputController.InputButtonTable["Fire"].PressedFrame);
            animator.SetInteger("JumpInputFrame", inputController.InputButtonTable["Jump"].PressedFrame);
            animator.SetInteger("JumpReleasedFrame", inputController.InputButtonTable["Jump"].ReleasedFrame);
            animator.SetInteger("KiritanState", (int)State);
            animator.SetBool("AntiGravityJump", AntiGravityJumpFlag);
            if (AntiGravityJumpFlag) {
                AntiGravityJumpFlag = false;
            }
        }

        /// <summary>
        /// ボイスを発声します
        /// play voice
        /// </summary>
        /// <param name="voiceName">
        /// ボイスオブジェクトの名前
        /// name of voice object
        /// </param>
        public void Voice(string voiceName) {
            voicePlayer.Play(voiceName);
        }

        /// <summary>
        /// 効果音を発声します
        /// play sound effect
        /// </summary>
        /// <param name="seName">
        /// 効果音オブジェクトの名前
        /// name of sound effect object
        /// </param>
        public void PlaySE(string seName) {
            soundEffects[seName].Play();
        }

        /// <summary>
        /// 向きを設定します
        /// set direction
        /// </summary>
        /// <param name="dir">
        /// 設定する向き
        /// direction
        /// </param>
        public void SetDirection(ActorDirection dir) {
            Direction = dir;
            transform.localScale = new Vector3(dir.ToFloat(), transform.localScale.y, transform.localScale.z);
        }

        /// <summary>
        /// きりたん砲レベルを1上昇させます
        /// raise cannon level
        /// </summary>
        public void CannonLevelUp() {
            CannonLevel++;

            //  switch Kirian graphics
            switch (CannonLevel) {
                case 1: //  Lv.1 -> Lv.2
                    JetNozzleObject.SetActive(true);
                    break;
                case 2: //  Lv.2 -> Lv.3
                    SubCannonObject.SetActive(true);
                    break;
                case 3: //  Lv.3 -> Lv.4
                    JetNozzle2Object.SetActive(true);
                    break;
                case 4: //  Lv.4 -> Lv.5
                    OptionUnitObject.SetActive(true);
                    break;
            }

            //  emit particle
            ParticleSystem particle = GameObject.Instantiate<GameObject>(LevelUpParticlePrefab).GetComponent<ParticleSystem>();
            particle.transform.position = transform.position;
            particle.Play();

            //  update cannon level UI Text
            CannonLevel level = CannonSet.CurrentCannonLevel(this);
            CannonLevelText.text = level.DisplayName;
            CannonLevelText.color = level.DisplayColor;
        }

        /// <summary>
        /// きりたんの状態を遷移させます。
        /// transition Kiritan state as next state
        /// </summary>
        /// <param name="nextState">
        /// next state
        /// </param>
        public void TransitionState(KiritanStateEnum nextStateName) {
            if (State != null) State.OnStateExit();
            State = StateSet.GetInstance(nextStateName);
            State.OnStateEnter();
        }

        /// <summary>
        /// きりたんが接地状態かどうかを取得します
        /// is Kiritan grounded?
        /// </summary>
        public bool IsGround {
            get {
                return judgeGround.IsGround;
            }
        }

        /// <summary>
        /// きりたんの位置をリスポーンポイントに戻す
        /// </summary>
        public void SetPositionAsResponePosition() {
            RigidbodyCache.MovePosition(ResponePosition);
            RigidbodyCache.velocity = Vector2.zero;
        }

        /// <summary>
        /// receive damage
        /// </summary>
        /// <param name="atk">attack collider object</param>
        public void Damage(Attack atk) {
            TransitionState(KiritanStateEnum.Damage);
            life.Damage(atk.Damage);
            PlaySE("Damage");
            Voice("Ah");

            //  calc knockback velocity
            var knockbackVelocity = new Vector2(
                x: Mathf.Sin(KnockbackAngle * Mathf.Deg2Rad) * KnockbackPower * (-Direction.ToFloat()),
                y: Mathf.Cos(KnockbackPower * Mathf.Deg2Rad) * KnockbackPower
                );
            RigidbodyCache.velocity = knockbackVelocity;
        }

        /// <summary>
        /// 死亡時の処理
        /// </summary>
        public void OnDead() {
            TransitionState(KiritanStateEnum.Dead);
            if (0 < life.Current) {
                life.Current = 0;
            }
        }
    }
}
