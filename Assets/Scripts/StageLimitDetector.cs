using UnityEngine;
using UnityEngine.Events;

namespace KiritanAction {

    /// <summary>
    /// detect stage limit and launch event
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class StageLimitDetector : MonoBehaviour{

        /// <summary>
        /// ステージの外に移動すると登録したメソッドを走らせる
        /// Method invoker launch when object was out of stage limit.
        /// </summary>
        public UnityEvent OnDetect;

        private LayerMask stageLimitLayer { get; set; }

        protected void Awake() {
            stageLimitLayer = LayerMask.GetMask(new string[] { "StageLimit" });
        }

        protected void OnTriggerExit2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & stageLimitLayer) == 0) return;
            OnDetect.Invoke();
        }
    }
}
