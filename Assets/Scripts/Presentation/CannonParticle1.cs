using UnityEngine;

namespace KiritanAction.Presentation {
    /// <summary>
    /// きりたん砲発射時のパーティクル
    /// particle when cannon is fired
    /// </summary>
    public class CannonParticle1 : MonoBehaviour{

        private Quaternion ZeroRotation;

        protected void Awake() {
            ZeroRotation.eulerAngles = Vector3.zero;
        }

        /// <summary>
        /// 発生位置を設定します
        /// set position
        /// </summary>
        /// <param name="position">
        /// 発生位置(World)
        /// world position
        /// </param>
        public void SetPosition(Vector3 position) {
            transform.position = position;
        }

        /// <summary>
        /// 角度を設定します
        /// set particle angle
        /// </summary>
        /// <param name="angle">
        /// 角度
        /// angle
        /// </param>
        public void SetAngleZ(float angle) {
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0f, 0f, 90f + angle);
            Transform particles = transform.FindChild("Particles");
            particles.rotation = ZeroRotation;
            particles.Rotate(new Vector3(0f, 0f, 67.5f - 90f));
            Transform smallParticles = transform.FindChild("SmallParticles");
            smallParticles.rotation = ZeroRotation;
            smallParticles.Rotate(new Vector3(0f, 0f, 60f - 90f));
            transform.rotation = rotation;
        }
    }
}
