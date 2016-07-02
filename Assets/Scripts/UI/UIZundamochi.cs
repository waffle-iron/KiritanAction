using UnityEngine;
using UnityEngine.UI;

namespace KiritanAction.UI {
    /// <summary>
    /// UI上のずんだもち
    /// Zundamochi on UI
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class UIZundamochi : MonoBehaviour{

        public ParticleSystem ParticleEmitter;

        private Image image { get; set; }

        protected void Awake() {
            image = GetComponent<Image>();
        }

        public void GetMochi() {
            image.color = Color.white;
            ParticleEmitter.Play();
        }
    }
}
