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

        public List<Collider2D> colliders;

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
