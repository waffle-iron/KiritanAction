using UnityEngine;

namespace KiritanAction.Common {

    /// <summary>
    /// 自身のPositionをTargetのx座標とy座標に設定する
    /// sync object position to target
    /// </summary>
    public class PositionSync2D : MonoBehaviour {

        /// <summary>
        /// 追跡対象
        /// target object transform
        /// </summary>
        [Tooltip("追跡対象")]
        public Transform Target;

        protected void Update() {
            if (Target == null) return;

            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
        }
    }
}
