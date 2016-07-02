using UnityEngine;

namespace KiritanAction.ConcleteAction {
    /// <summary>
    /// 四つんばいモードへの移行
    /// transition Kiritan state to crawl mode
    /// </summary>
    [CreateAssetMenu(fileName="Crawl", menuName="ScriptableObject/Action/Crawl")]
    public class Crawl : Action{

        /// <summary>
        /// 接地状態
        /// ↓が入力されたとき
        /// 
        /// is grounded
        /// input down
        /// </summary>
        /// <returns></returns>
        public override bool Trigger() {
            if (!kiritan.IsGround) return false;
            if (input.InputButtonTable["Down"].PressedFrame == 1) return true;
            return false;
        }

        /// <summary>
        /// Crawlモードに移行する
        /// transition to crawl mode
        /// </summary>
        public override void OnTrigger() {
            kiritan.PlaySE("Stance");

            kiritan.TransitionState(KiritanStateEnum.Crawl);
        }
    }
}
