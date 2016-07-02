using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction {

    /// <summary>
    /// 全てのステージを格納するやつ
    /// stage set
    /// </summary>
    [CreateAssetMenu(fileName="StageSet", menuName="ScriptableObject/StageSet")]
    public class StageSet : ScriptableObject{

        //  現在のステージのインデックス
        //  current stage index
        private int currentIndex { get; set; }

        //  全てのステージ
        //  all stages
        public List<Stage> Stages;

        //  初期化
        //  initialize
        protected void OnEnable() {
            currentIndex = 0;
        }

        /// <summary>
        /// 現在のステージを取得します
        /// get current stage
        /// </summary>
        /// <returns>current stage</returns>
        public Stage CurrentStage() {
            return Stages[currentIndex];
        }
    }
}
