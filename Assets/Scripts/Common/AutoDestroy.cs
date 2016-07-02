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

        protected void Awake() {
            GameObject.Destroy(gameObject, Duration);
        }
    }
}
