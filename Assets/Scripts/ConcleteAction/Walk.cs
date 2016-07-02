using UnityEngine;

namespace KiritanAction.ConcleteAction {

    /// <summary>
    /// 歩行
    /// walk
    /// </summary>
    [CreateAssetMenu(fileName="Walk", menuName="ScriptableObject/Action/Walk")]
    public class Walk : Action{

        //  入力し続けた場合の速度
        //  walking speed
        public float Speed;

        //  加速度
        //  accelerometer
        public float Accel;

        /// <summary>
        /// 接地
        /// 左右入力かつジャンプ入力が無い
        /// 
        /// is grounded
        /// jump button is not pressing and horizontal button is pressing
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (!kiritan.IsGround) return false;
            if (input.InputButtonTable["Jump"].PressedFrame > 0) return false;
            if (input.InputButtonTable["Left"].PressedFrame > 0) return true;
            if (input.InputButtonTable["Right"].PressedFrame > 0) return true;
            return false;
        }

        /// <summary>
        /// 歩行アニメーションを再生
        /// 入力方向に一定の力を加える
        /// 但し歩行速度よりも横速度が速い場合は何もしない
        /// 
        /// play animation
        /// add force if horizontal velocity < walking speed
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