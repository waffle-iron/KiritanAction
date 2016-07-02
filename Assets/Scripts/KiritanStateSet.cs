using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KiritanAction {
    /// <summary>
    /// きりたんの状態インスタンスが複数生成されないように制御するファクトリクラス
    /// kiritan state definitions
    /// </summary>
    [CreateAssetMenu(fileName="StateSet", menuName="ScriptableObject/StateSet")]
    public class KiritanStateSet : ScriptableObject{

        /// <summary>
        /// 全ての状態を格納する
        /// states
        /// </summary>
        public List<KiritanState> States;

        /// <summary>
        /// 状態のプール
        /// hash enum -> KiritanState object
        /// </summary>
        private Dictionary<KiritanStateEnum, KiritanState> pool;

        /// <summary>
        /// 初期化
        /// initialize
        /// </summary>
        protected void OnEnable() {
            pool = States.ToDictionary(x => x.StateId);
        }

        /// <summary>
        /// ステートインスタンスを取得します
        /// get state instance
        /// </summary>
        /// <param name="state">state enumeration</param>
        /// <returns></returns>
        public KiritanState GetInstance(KiritanStateEnum state) {
            return pool[state];
        }
    }
}
