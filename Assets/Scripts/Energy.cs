using UnityEngine;

namespace KiritanAction {

    /// <summary>
    /// きりたん砲を撃つためのエネルギー
    /// MISOとしてゲージを表示
    /// 
    /// energy for cannon
    /// display as MISO
    /// </summary>
    public class Energy : ScriptableObject{

        private float _current { get; set; }

        /// <summary>
        /// エネルギーの現在値を取得または設定します
        /// get and set current energy
        /// </summary>
        public float Current {
            get { return _current; }
            set {
                if (value > Max) value = Max;
                else if (value < 0f) value = 0f;
                _current = value;
            }
        }

        //  エネルギーの最大値
        //  max energy amount
        [Tooltip("エネルギーの最大値")]
        public float Max = 100f;

        //  初期化
        //  initialize
        protected void OnEnable() {
            Current = Max;
        }

        /// <summary>
        /// 現在値/最大値を取得します
        /// get current / max
        /// </summary>
        /// <returns>[0f, 1f]</returns>
        public float GetRatio() {
            return Current / Max;
        }
    }
}
