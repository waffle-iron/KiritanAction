using UnityEngine;

namespace KiritanAction.AttackReceiveable {
    /// <summary>
    /// うに
    /// Sea urchin
    /// </summary>
    [RequireComponent(typeof(AttackReceiver))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Life))]
    [RequireComponent(typeof(AudioSource))]
    public class Uni : MonoBehaviour {

        /// <summary>
        /// 破壊したときに発生させるパーティクルのPrefab
        /// Prefab of the particles that target is destroyed.
        /// </summary>
        public GameObject breakParticlePrefab;

        private SpriteRenderer spriteRenderer { get; set; }
        private AudioSource audioSource { get; set; }
        private Life life { get; set; }

        private bool readyDestroy { get; set; }

        protected void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();

            life = GetComponent<Life>();

            readyDestroy = false;
        }

        protected void Update() {
            if (readyDestroy && !audioSource.isPlaying) {
                GameObject.Destroy(gameObject);
            }
        }

        /// <summary>
        /// 弾などが命中したときの処理
        /// 破壊する
        /// Destroy this.
        /// </summary>
        /// <param name="atk">
        /// 攻撃判定オブジェクト
        /// attack collider object
        /// </param>
        public void OnAttackReceived(Attack atk) {
            life.Damage(atk.Damage);
        }

        /// <summary>
        /// dead
        /// </summary>
        public void Dead() {
            GetComponent<Collider2D>().enabled = false;
            spriteRenderer.enabled = false;

            audioSource.Play();

            ParticleSystem particle = GameObject.Instantiate<GameObject>(breakParticlePrefab).GetComponent<ParticleSystem>();
            particle.transform.SetParent(GameObject.FindGameObjectWithTag("InstantObjectContainer").transform);
            particle.transform.position = transform.position;
            particle.Play();

            readyDestroy = true;
        }
    }
}
