using UnityEngine;

namespace KiritanAction.ConcleteKiritanState {
    [CreateAssetMenu(fileName="Damage", menuName="ScriptableObject/KiritanState/Damage")]
    public class Damage : KiritanState{

        /// <summary>
        /// max frame count when player will able to operation
        /// </summary>
        public int maxFrameCount;

        //  frame count from transition
        private int frameCount { get; set; }

        public override void OnStateEnter() {
            base.OnStateEnter();

            frameCount = 0;
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            frameCount++;

            if (frameCount >= maxFrameCount || (frameCount > 2 && kiritan.IsGround)) {
                kiritan.TransitionState(KiritanStateEnum.Normal);
                return;
            }
        }
    }
}
