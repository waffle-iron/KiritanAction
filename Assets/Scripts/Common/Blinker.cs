using UnityEngine;

namespace KiritanAction.Common {
    /// <summary>
    /// blink renderer
    /// </summary>
    public class Blinker : MonoBehaviour{

        private bool isBlinking { get; set; }

        private Renderer[] renderers { get; set; }

        public void StartBlink() {
            isBlinking = true;
        }

        public void StopBlink() {
            isBlinking = false;
            foreach (var renderer in renderers) {
                renderer.enabled = true;
            }
        }

        protected void Awake() {
            isBlinking = false;
            renderers = GetComponentsInChildren<Renderer>();
        }

        protected void Update() {
            if (!isBlinking) return;
            foreach (var renderer in renderers) {
                renderer.enabled = !renderer.enabled;
            }
        }
    }
}
