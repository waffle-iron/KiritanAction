using UnityEngine;

namespace KiritanAction.ConcleteAction {

    /// <summary>
    /// 地上構えへの移行
    /// transition Kiritan state to stance mode
    /// </summary>
    [CreateAssetMenu(fileName = "Stance", menuName = "ScriptableObject/Action/Stance")]
    public class Stance : Action{

        /// <summary>
        /// 接地状態
        /// Fireが入力されたとき
        /// 
        /// is grounded
        /// fire button pressed
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (!kiritan.IsGround) return false;
            if (input.InputButtonTable["Fire"].PressedFrame == 1) return true;
            return false;
        }

        /// <summary>
        /// Stanceモードに移行する
        /// transition to stance mode
        /// </summary>
        public override void OnTrigger() {
            kiritan.PlaySE("Stance");

            kiritan.TransitionState(KiritanStateEnum.Stance);
        }
    }
}
