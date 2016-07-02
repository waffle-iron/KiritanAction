using UnityEngine;

namespace KiritanAction {
    /// <summary>
    /// 地形の当たり判定オブジェクトにつけるコンポーネントクラス
    /// Collider for Landshape
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class LandshapeCollider : MonoBehaviour{

        public float Friction = 1f;

        public void AffectHorizontalFriction(Rigidbody2D target) {
            if(target.velocity.x > 0f) {
                target.velocity = new Vector2(target.velocity.x - Friction, target.velocity.y);
                if(target.velocity.x < 0f) target.velocity = new Vector2(0f, target.velocity.y);
            }
            else if(target.velocity.x < 0f) {
                target.velocity = new Vector2(target.velocity.x + Friction, target.velocity.y);
                if (target.velocity.x > 0f) target.velocity = new Vector2(0f, target.velocity.y);
            }
        }
    }
}
