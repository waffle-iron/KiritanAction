using UnityEngine;

namespace KiritanAction {
    /// <summary>
    /// 放物線投射弾
    /// Bullet under the influence of gravity. 
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class GrenadeBullet : MonoBehaviour{

        /// <summary>
        /// 命中後にオブジェクトを消滅させるまでの時間
        /// Time to destroy the object after hit.
        /// </summary>
        public float DelayAfterHit;

        /// <summary>
        /// 爆発オブジェクトPrefab
        /// Prefab of attack collider object.
        /// </summary>
        public GameObject ExplodePrefab;

        /// <summary>
        /// 衝突対象
        /// collider target layer mask
        /// </summary>
        public LayerMask TargetFilter;

        private Rigidbody2D rigidbodyCache { get; set; }

        protected void Awake() {
            rigidbodyCache = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// 弾を初期化します
        /// initialize bullet.
        /// </summary>
        /// <param name="position">
        /// 発射位置
        /// emitter position
        /// </param>
        /// <param name="velocity">
        /// 初速
        /// initial velocity
        /// </param>
        public void Init(Vector3 position, Vector2 velocity) {
            transform.position = position;
            rigidbodyCache.velocity = velocity;
            rigidbodyCache.isKinematic = false;
        }

        //  当たり判定があるものに接触時
        //  on collided
        protected void OnTriggerEnter2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & TargetFilter) == 0) return;

            rigidbodyCache.isKinematic = true;

            //  爆発を発生させる
            //  emit explosion
            GameObject.Instantiate<GameObject>(ExplodePrefab).GetComponent<Explode>().Emit(transform.position);

            //  パーティクルを発生させる
            //  emit explosion particles
            ParticleSystem particle = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Particles/Explode1")).GetComponent<ParticleSystem>();
            particle.transform.position = transform.position;
            particle.Play();

            //  当たり判定を消失させる
            //  remove my collider
            GetComponent<Collider2D>().enabled = false;

            GameObject.Destroy(gameObject, DelayAfterHit);
        }

        public void DestroyMe() {
            GameObject.Destroy(gameObject);
        }
    }
}
