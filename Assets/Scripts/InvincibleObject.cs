using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using KiritanAction.Common;

namespace KiritanAction {
    /// <summary>
    /// attached object can set invincible
    /// </summary>
    public class InvincibleObject : MonoBehaviour{

        //  image for blink
        //  nullable
        public Blinker blinkerImage;

        //  frame count 
        private int invincibleFrame { get; set; }

        public List<Collider2D> colliders;

        /// <summary>
        /// get object is invincible or not
        /// </summary>
        public bool IsInvincible {
            get { return invincibleFrame > 0; }
        }

        /// <summary>
        /// set as invincible during frameCount frames
        /// </summary>
        /// <param name="frameCount">frame count of invincible period</param>
        public void SetInvincible(int frameCount) {
            invincibleFrame = frameCount;
            foreach (var collider in colliders) {
                collider.enabled = false;
            }
            if(blinkerImage != null) blinkerImage.StartBlink();
        }

        protected void Awake() {
            invincibleFrame = 0;
        }

        protected void FixedUpdate() {
            if (invincibleFrame > 0) {
                invincibleFrame--;
                if (invincibleFrame <= 0) {
                    foreach (var collider in colliders) {
                        collider.enabled = true;
                    }
                    if (blinkerImage != null) blinkerImage.StopBlink();
                }
            }
        }
    }
}
