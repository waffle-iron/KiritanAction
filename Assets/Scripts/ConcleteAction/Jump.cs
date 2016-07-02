using UnityEngine;

namespace KiritanAction.ConcleteAction {

    /// <summary>
    /// ジャンプ
    /// Jump
    /// </summary>
    [CreateAssetMenu(fileName="Jump", menuName="ScriptableObject/Action/Jump")]
    public class Jump : Action{

        /// <summary>
        /// y軸に加える速さ
        /// verticl speed
        /// </summary>
        public float speed;

        /// <summary>
        /// 接地状態
        /// Jumpが入力されているとき
        /// 
        /// is grounded
        /// jump button pressed
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (kiritan.OnGroundFrame <= 1) return false;
            if (input.InputButtonTable["Jump"].PressedFrame == 1) return true;
            return false;

            //  TODO:   砲撃モードの頭上キャスト
        }

        /// <summary>
        /// y軸にspeedを加える
        /// add speed
        /// </summary>
        public override void OnTrigger() {
            kiritan.PlaySE("Jump");

            kiritan.RigidbodyCache.velocity = kiritan.RigidbodyCache.velocity + Vector2.up * speed;
        }
    }
}
