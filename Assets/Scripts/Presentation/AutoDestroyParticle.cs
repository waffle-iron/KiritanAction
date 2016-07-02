using UnityEngine;

namespace KiritanAction.Presentation {
    /// <summary>
    /// Durationで自殺するパーティクルシステム
    /// destroy automatically using duration of paticle system
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class AutoDestroyParticle : MonoBehaviour{

        private ParticleSystem particle { get; set; }

        protected void Awake() {
            particle = GetComponent<ParticleSystem>();
        }

        public void Start() {
            GameObject.Destroy(gameObject, particle.duration);
        }
    }
}
