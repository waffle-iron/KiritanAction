using UnityEngine;

namespace KiritanAction.Presentation {

    /// <summary>
    /// きりたんのスピードに応じてオブジェクトを揺らす
    /// set object angle by Kiritan velocity
    /// </summary>
    public class ZRotationByVelocity : MonoBehaviour{

        public Kiritan Kiritan;

        public float DefaultRotation;

        public float RotationRangeMax;
        public float RotationRangeMin;

        public float XRatio;
        public float YRatio;

        protected void Update() {
            float direction = Kiritan.Direction.ToFloat();
            Vector2 velocity = Kiritan.RigidbodyCache.velocity;

            float r = DefaultRotation;

            //  y速度による回転
            r += velocity.y * YRatio;

            //  x速度による回転
            r += velocity.x * direction * XRatio;

            if (r > RotationRangeMax) r = RotationRangeMax;
            if (r < RotationRangeMin) r = RotationRangeMin;

            Quaternion q = new Quaternion();
            q.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(transform.localRotation.eulerAngles.z, r, 0.5f));
            transform.localRotation = q;
        }
    }
}
