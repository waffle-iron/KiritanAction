using UnityEngine;
using KiritanAction.Sound;
using System.Collections.Generic;

namespace KiritanAction {
    /// <summary>
    /// きりたんの状態
    /// State of Kiritan
    /// </summary>
    [CreateAssetMenu(fileName="NewState", menuName="ScriptableObject/KiritanState/State")]
    public class KiritanState : ScriptableObject{

        /// <summary>
        /// 使用可能なアクション
        /// actions
        /// </summary>
        [Tooltip("使用可能なアクション")]
        public List<Action> Actions;

        /// <summary>
        /// ステート列挙上での定義
        /// State enum definition
        /// </summary>
        public KiritanStateEnum StateId;

        protected Kiritan kiritan { get; set; }

        protected FixedInputController inputController { get; set; }

        /// <summary>
        /// 状態を初期化します
        /// initialize state
        /// </summary>
        /// <param name="kiritan">
        /// きりたんへの参照
        /// Kiritan
        /// </param>
        /// <param name="inputController">
        /// 入力コントローラへの参照
        /// input controller
        /// </param>
        public virtual void Init(Kiritan kiritan, FixedInputController inputController) {
            this.kiritan = kiritan;
            this.inputController = inputController;
            foreach (Action a in Actions) {
                a.Init(kiritan, inputController);
            }
        }

        /// <summary>
        /// きりたんがこのステートに遷移したときに呼び出される
        /// call at state change as this
        /// </summary>
        public virtual void OnStateEnter() {
            //  nop
        }

        /// <summary>
        /// きりたんがこのステートから遷移するときに呼び出される
        /// call at state change as next
        /// </summary>
        public virtual void OnStateExit() {
            //  nop
        }

        public virtual void OnFixedUpdate() {
            HandleActions();
        }

        protected void HandleActions() {
            foreach (Action act in Actions) {
                if (act.Trigger()) act.OnTrigger();
            }
        }

        /// <summary>
        /// ステートからintへの明示的キャスト
        /// StateIdの値を返却します
        /// 
        /// state to int explicit cast
        /// use for animation controller
        /// </summary>
        /// <param name="state"></param>
        /// <returns>StateId</returns>
        public static explicit operator int(KiritanState state) {
            return (int)state.StateId;
        }

        /// <summary>
        /// ステートからenumへの暗黙的キャスト
        /// StateIdの値を返却します
        /// 
        /// state to KirianStateEnum implicit cast
        /// </summary>
        /// <param name="state"></param>
        /// <returns>StateId</returns>
        public static implicit operator KiritanStateEnum(KiritanState state) {
            return state.StateId;
        }
    }
}
