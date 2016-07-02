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

        protected void Awake() {
            audioSource = GetComponent<AudioSource>();
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

            GameObject.Destroy(gameObject, audioSource.clip != null ? audioSource.clip.length : 1f);
        }
    }
}
