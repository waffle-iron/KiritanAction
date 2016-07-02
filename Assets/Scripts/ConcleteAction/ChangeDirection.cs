using UnityEngine;

namespace KiritanAction.ConcleteAction {

    /// <summary>
    /// 方向転換
    /// change Kiritan direction
    /// </summary>
    [CreateAssetMenu(fileName="ChangeDirection", menuName="ScriptableObject/Action/ChangeDirection")]
    public class ChangeDirection : Action{

        /// <summary>
        /// 左右入力
        /// input horizontal
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (input.InputButtonTable["Left"].PressedFrame > 0) return true;
            if (input.InputButtonTable["Right"].PressedFrame > 0) return true;
            return false;
        }

        /// <summary>
        /// きりたんの向きを変更する
        /// change Kiritan direction
        /// </summary>
        public override void OnTrigger() {
            if (input.InputButtonTable["Left"].PressedFrame > 0) {
                kiritan.SetDirection(ActorDirection.Left);
            }
            else {
                kiritan.SetDirection(ActorDirection.Right);
            }
        }
    }
}
