using UnityEngine;

namespace KiritanAction.ConcleteAction {

    /// <summary>
    /// 空中制御
    /// move aerial
    /// </summary>
    [CreateAssetMenu(fileName="AirMove", menuName="ScriptableObject/Action/AirMove")]
    public class AirMove : Action {

        //  入力時の加速度
        //  accelometer at input
        public float Accel;

        //  入力し続けた場合の速度
        //  max speed of aerial move
        public float Speed;

        /// <summary>
        /// 接地していない
        /// 左右入力
        /// 
        /// is not grounded
        /// input horizontal
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (kiritan.IsGround) return false;
            if (input.InputButtonTable["Left"].PressedFrame > 0) return true;
            if (input.InputButtonTable["Right"].PressedFrame > 0) return true;
            return false;
        }

        /// <summary>
        /// 入力方向に一定の力を加える
        /// 但し歩行速度よりも横速度が速い場合は何もしない
        /// add force if horizontal velocity smaller than Speed.
        /// </summary>
        public override void OnTrigger() {

            Vector2 velocity = kiritan.RigidbodyCache.velocity;
            if (input.InputButtonTable["Left"].PressedFrame > 0) {
                if (velocity.x > -Speed) {
                    kiritan.RigidbodyCache.velocity = new Vector2(velocity.x - Accel, velocity.y);
                    if (kiritan.RigidbodyCache.velocity.x < -Speed) {
                        kiritan.RigidbodyCache.velocity = new Vector2(-Speed, velocity.y);
                    }
                }
            }
            else {
                if (velocity.x < Speed) {
                    kiritan.RigidbodyCache.velocity = new Vector2(velocity.x + Accel, velocity.y);
                    if (kiritan.RigidbodyCache.velocity.x > Speed) {
                        kiritan.RigidbodyCache.velocity = new Vector2(Speed, velocity.y);
                    }
                }
            }
        }
    }
}
