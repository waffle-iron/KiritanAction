using UnityEngine;

namespace KiritanAction.ConcleteAction {
    /// <summary>
    /// きりたん砲の角度を変更する
    /// change cannon angle
    /// </summary>
    [CreateAssetMenu(fileName="ChangeCannonAngle", menuName="ScriptableObject/Action/ChangeCannonAngle")]
    public class ChangeCannonAngle : Action{

        /// <summary>
        /// 最大角度(Degree)
        /// max angle (degree)
        /// </summary>
        public float MaxAngle;

        /// <summary>
        /// 最小角度(Degree)
        /// min angle (degree)
        /// </summary>
        public float MinAngle;

        /// <summary>
        /// 回転速度(Degree/frame)
        /// angular velocity (degree/frame)
        /// </summary>
        public float AngularVelocity;

        /// <summary>
        /// 上下入力
        /// input vertical
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (input.InputButtonTable["Up"].PressedFrame > 0 || input.InputButtonTable["Down"].PressedFrame > 0) return true;
            return false;
        }

        public override void OnTrigger() {
            if (input.InputButtonTable["Down"].PressedFrame > 0) {
                kiritan.CannonState.CannonModeAngle -= AngularVelocity;
                if (kiritan.CannonState.CannonModeAngle < MinAngle) kiritan.CannonState.CannonModeAngle = MinAngle;
            }
            else {
                kiritan.CannonState.CannonModeAngle += AngularVelocity;
                if (kiritan.CannonState.CannonModeAngle > MaxAngle) kiritan.CannonState.CannonModeAngle = MaxAngle;
            }
        }
    }
}
