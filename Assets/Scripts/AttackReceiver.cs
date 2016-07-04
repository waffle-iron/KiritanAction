using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

namespace KiritanAction {

    [System.Serializable]
    public class AttackEvent : UnityEvent<Attack> {
    }

    /// <summary>
    /// 攻撃を受けるオブジェクト
    /// receive attack
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class AttackReceiver : MonoBehaviour{

        public List<CircleCollider2D> CircleColliders;
        public List<PolygonCollider2D> PolygonColliders;
        public List<BoxCollider2D> BoxColliders;

        public List<Collider2D> colliders { get; private set; }

        protected void Awake() {
            colliders = CircleColliders
                .Cast<Collider2D>()
                .Concat(PolygonColliders.Cast<Collider2D>())
                .Concat(BoxColliders.Cast<Collider2D>())
                .ToList();
        }

        /// <summary>
        /// methods invoke when damaged
        /// </summary>
        public AttackEvent OnReceivedMethod;

        /// <summary>
        /// 攻撃を受けたときに呼び出されます
        /// on collided attack collider object
        /// </summary>
        /// <param name="attack">
        /// 攻撃判定オブジェクト
        /// attack collider object
        /// </param>
        public void OnAttackReceived(Attack attack) {
            OnReceivedMethod.Invoke(attack);
        }
    }
}
