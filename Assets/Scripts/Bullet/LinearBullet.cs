using UnityEngine;

namespace KiritanAction {
    /// <summary>
    /// 直線投射弾
    /// Bullet to linear motion. 
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LinearBullet : MonoBehaviour{

        /// <summary>
        /// 命中後にオブジェクトを消滅させるまでの時間
        /// Time to destroy the object after hit.
        /// </summary>
        public float DelayAfterHit;

        /// <summary>
        /// オブジェクトが自動消滅するまでの時間
        /// Time to explode automatically after awake.
        /// </summary>
        public float Duration;

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

        private bool isExploded { get; set; }

        private Rigidbody2D rigidbodyCache { get; set; }

        private float time { get; set; }

        protected void Awake() {
            rigidbodyCache = GetComponent<Rigidbody2D>();
            time = 0f;
            isExploded = false;
        }

        /// <summary>
        /// 弾を初期化します
        /// initialize bullet
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

        protected void FixedUpdate() {
            time += Time.deltaTime;
            if (time >= Duration) {
                Explode();
            }
        }

        //  on collided
        protected void OnTriggerEnter2D(Collider2D other) {
            if (((1 << other.gameObject.layer) & TargetFilter) == 0) return;
            Explode();
        }

        //  爆発
        private void Explode() {
            if (isExploded) return;

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

            isExploded = true;
        }
    }
}
