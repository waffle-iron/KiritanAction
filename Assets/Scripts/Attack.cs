using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction {
    /// <summary>
    /// 攻撃判定
    /// attack collider object
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Attack : MonoBehaviour{

        /// <summary>
        /// 攻撃判定が作用する先
        /// layer mask that can receive attack
        /// </summary>
        public LayerMask TargetLayer;

        /// <summary>
        /// ダメージ量
        /// damage parameter
        /// </summary>
        public int Damage;

        /// <summary>
        /// 攻撃レベル
        /// attack level
        /// </summary>
        public int AttackLevel;

        /// <summary>
        /// 一度衝突した後に衝突しなくなるか(true のとき1度のみ)
        /// if true, collided object is never collide.
        /// </summary>
        public bool AttackOnce;

        //  一度衝突した対象の履歴
        //  List of collided object
        private List<AttackReceiver> collidedObjects { get; set; }

        protected void Awake() {
            collidedObjects = new List<AttackReceiver>();
        }

        //  接触時
        //  on collided
        protected void OnTriggerEnter2D(Collider2D other) {
            //  接触対象がレイヤーマスクに適合しない場合早期return
            //  exit if collided object do not have target layer 
            if (((1 << other.gameObject.layer) & TargetLayer.value) == 0) return;

            //  当たり判定レシーバがあればレシーバを起動する
            //  fire receiver event if collided object have AttackReceiver component
            AttackReceiver receiver = other.GetComponent<AttackReceiver>();
            if (receiver == null) return;
            if (!receiver.colliders.Contains(other)) return;
            if (!collidedObjects.Contains(receiver)) {  //  衝突履歴に含まれていない場合のみ処理する
                if(AttackOnce) collidedObjects.Add(receiver);
                receiver.OnAttackReceived(this);
            }
        }
    }
}
