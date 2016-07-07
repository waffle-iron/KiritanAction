using UnityEngine;
using KiritanAction.Common;

namespace KiritanAction {
    public class PauseController : MonoBehaviour{

        public FixedInputController InputController;

        public Pausable Pausable;

        public GameObject PausingCanvasObject;

        protected void Awake() {
            PausingCanvasObject.SetActive(false);
        }

        protected void FixedUpdate() {
            if (InputController.InputButtonTable["Pause"].PressedFrame == 1) {
                if (Pausable.IsPausing) {
                    Pausable.Resume();
                    PausingCanvasObject.SetActive(false);
                }
                else {
                    Pausable.Pause();
                    PausingCanvasObject.SetActive(true);
                }
            }
        }
    }
}
