using UnityEngine;
using KiritanAction.Sound;

namespace KiritanAction {
    /// <summary>
    /// きりたんのアクションクラス(抽象)
    /// action(abstract)
    /// </summary>
    public abstract class Action : ScriptableObject{

        //  きりたんへの参照
        //  Kiritan
        protected Kiritan kiritan { get; private set; }

        //  入力コントローラへの参照
        //  Input controller
        protected FixedInputController input { get; private set; }

        /// <summary>
        /// 手動初期化
        /// きりたんのオブジェクトがAwakeするタイミングで設定する
        /// 
        /// manual initialize when Kiritan object has created
        /// </summary>
        /// <param name="kiritan">
        /// きりたんオブジェクト
        /// Kiritan
        /// </param>
        /// <param name="inputController">
        /// 入力コントローラ
        /// input controller
        /// </param>
        public virtual void Init(Kiritan kiritan, FixedInputController inputController) {
            this.kiritan = kiritan;
            input = inputController;
        }

        /// <summary>
        /// アクションを実行するときtrueを返す抽象メソッド
        /// 例えば歩行なら[左右入力かつ地上かつきりたんが通常状態のときtrue]など
        /// 
        /// if this method was return true, call OnTrigger in this frame
        /// </summary>
        /// <returns>
        /// true:   そのアクションを実行する
        /// false:  そのアクションは実行しない
        /// 
        /// true:   call OnTrigger in this frame
        /// false:  do not call OnTrigger in this frame
        /// </returns>
        public abstract bool Trigger();

        /// <summary>
        /// 実行する場合の挙動
        /// Action
        /// </summary>
        public abstract void OnTrigger();
    }
}
