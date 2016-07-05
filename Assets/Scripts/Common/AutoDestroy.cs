using UnityEngine;

namespace KiritanAction.Common {
    /// <summary>
    /// 指定時間後に自動的にGameObjectを破棄
    /// Destroy gameobject automatically
    /// </summary>
    public class AutoDestroy : MonoBehaviour{

        /// <summary>
        /// Destroyまでの時間(s)
        /// time to destroy (s)
        /// </summary>
        public float Duration;

        private float time { get; set; }

        protected void Awake() {
            time = 0f;
        }

        protected void FixedUpdate() {
            time += Time.deltaTime;
            if (time >= Duration) {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
