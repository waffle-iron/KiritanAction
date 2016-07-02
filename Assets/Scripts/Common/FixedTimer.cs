using UnityEngine;

namespace KiritanAction.Common {

    /// <summary>
    /// FixedUpdateで更新されるタイマークラス
    /// Timer update at FixedUpdate
    /// </summary>
    public class FixedTimer : MonoBehaviour{

        /// <summary>
        /// trueならばAwakeでタイマーをスタートします
        /// Start on awake if true.
        /// </summary>
        [Tooltip("trueならばAwakeでタイマーをスタートします")]
        public bool startOnAwake = false;

        /// <summary>
        /// タイマーをスタートしてからの時間(s)
        /// Time
        /// </summary>
        public float Time { get; private set; }

        //  計測中かどうか
        private bool isStarted { get; set; }

        protected void Awake() {
            Reset();
            isStarted = startOnAwake;
        }

        protected void FixedUpdate() {
            if (isStarted) {
                Time += UnityEngine.Time.deltaTime;     //  FixedUpdateのdeltaTimeはFixedDeltaTimeと同値
            }
        }

        /// <summary>
        /// Timeを0に設定します
        /// reset timer
        /// </summary>
        public void Reset() {
            Time = 0.0f;
        }

        /// <summary>
        /// 計測を開始します
        /// start timer
        /// </summary>
        public void Start() {
            isStarted = true;
        }

        /// <summary>
        /// 計測を一時停止します
        /// stop timer
        /// </summary>
        public void Stop() {
            isStarted = false;
        }
    }
}
