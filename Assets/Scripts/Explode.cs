using UnityEngine;
using KiritanAction.Common;

namespace KiritanAction {
    /// <summary>
    /// 爆発オブジェクト
    /// attack collider object created from bullet
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Attack))]
    public class Explode : MonoBehaviour{

        private AudioSource audioSource { get; set; }

        private float frames { get; set; }

        protected void Awake() {
            audioSource = GetComponent<AudioSource>();
            frames = 0;
        }

        protected void FixedUpdate() {
            if (frames > 0 && !audioSource.isPlaying) {
                GameObject.Destroy(gameObject);
            }

            frames++;
        }

        /// <summary>
        /// 爆発を発生させます
        /// explode
        /// </summary>
        /// <param name="position">
        /// 発生位置
        /// position emitted
        /// </param>
        public void Emit(Vector3 position) {
            transform.position = position;
        }
    }
}
