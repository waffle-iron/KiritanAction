using UnityEngine;

namespace KiritanAction.ConcleteAction
{

    /// <summary>
    /// 自滅
    /// Self Killing
    /// </summary>
    [CreateAssetMenu(fileName = "SelfKilling", menuName = "ScriptableObject/Action/SelfKilling")]
    public class SelfKilling : Action
    {
        /// <summary>
        /// 接地状態
        /// self-killingが入力されているとき
        /// 
        /// is grounded
        /// self-killing button pressed
        /// </summary>
        /// <returns></returns>
        public override bool Trigger()
        {
            return (0 < kiritan.OnGroundFrame && input.InputButtonTable["SelfKilling"].PressedFrame == 1);
        }

        public override void OnTrigger()
        {
            kiritan.OnDead();
        }
    }
}
