using UnityEngine;
using KiritanAction.Sound;

namespace KiritanAction {
    
    /// <summary>
    /// ステージ中に存在するアイテム
    /// 触れると消滅する
    /// 
    /// floating goods in stage
    /// destroy at collide to Kiritan
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class Item : MonoBehaviour{

        /// <summary>
        /// 入手したときの効果
        /// effect at get goods
        /// </summary>
        protected abstract void Affect();

        //  きりたんへの参照
        //  Kiritan
        public Kiritan Kiritan { protected get; set; }

        //  取得時のSE
        //  Sound at get goods
        private AudioSource soundEffect { get; set; }

        //  当たり判定
        //  collider
        private CircleCollider2D circleCollider { get; set; }

        //  パーティクルのGameObject
        //  Prefab of loop particle
        private GameObject particleObj { get; set; }

        //  スプライト
        //  sprite
        private SpriteRenderer spriteRenderer { get; set; }

        protected virtual void Awake() {
            soundEffect = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            circleCollider = GetComponent<CircleCollider2D>();
            particleObj = transform.FindChild("Particle").gameObject;
        }

        //  接触時
        //  on collided
        protected void OnTriggerEnter2D(Collider2D other) {
            Affect();
            soundEffect.Play();
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            particleObj.SetActive(false);
            GameObject.Destroy(gameObject, soundEffect.clip != null ? soundEffect.clip.length : 0f);
        }
    }
}
