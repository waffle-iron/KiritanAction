﻿using UnityEngine;

namespace KiritanAction.AttackReceiveable {
    /// <summary>
    /// 的
    /// Target
    /// </summary>
    [RequireComponent(typeof(AttackReceiver))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Target : MonoBehaviour {

        /// <summary>
        /// 破壊したときに発生させるパーティクルのPrefab
        /// Prefab of the particles that target is destroyed.
        /// </summary>
        public GameObject breakParticlePrefab;

        private AudioSource audioSource { get; set; }
        private SpriteRenderer spriteRenderer { get; set; }

        private bool readyDestroy { get; set; }

        protected void Awake() {
            audioSource = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
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
