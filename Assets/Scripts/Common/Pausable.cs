using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace KiritanAction.Common {

    /// <summary>
    /// pause all children
    /// </summary>
    public class Pausable : MonoBehaviour {

        #region Inner struct RigidBody2DVelocity
        private struct RigidBody2DVelocity {
            private Vector2 velocity;
            private float angularVelocity;

            public RigidBody2DVelocity(Rigidbody2D rigidbody2D) {
                velocity = rigidbody2D.velocity;
                angularVelocity = rigidbody2D.angularVelocity;
            }

            public void Restore(ref Rigidbody2D rigidbody2D) {
                rigidbody2D.velocity = velocity;
                rigidbody2D.angularVelocity = angularVelocity;
            }
        }
        #endregion

        #region Inner struct RigidBodyVelocity
        private struct RigidBodyVelocity {
            private Vector3 velocity;
            private Vector3 angularVelocity;

            public RigidBodyVelocity(Rigidbody rigidbody) {
                velocity = rigidbody.velocity;
                angularVelocity = rigidbody.angularVelocity;
            }

            public void Restore(ref Rigidbody rigidbody) {
                rigidbody.velocity = velocity;
                rigidbody.angularVelocity = angularVelocity;
            }
        }
        #endregion

        public bool IsPausing { get; private set; }

        private RigidBody2DVelocity[] velocities2D { get; set; }
        private Rigidbody2D[] rigidbodies2D { get; set; }
        private RigidBodyVelocity[] velocities { get; set; }
        private Rigidbody[] rigidbodies { get; set; }
        private MonoBehaviour[] components { get; set; }
        private ParticleSystem[] particles { get; set; }
        private Animator[] animators { get; set; }
        private AudioSource[] audioSources { get; set; }

        protected void Awake(){
            IsPausing = false;
        }

        public void Pause() {
            //  extract all rigidbody2D that is not sleeping
            rigidbodies2D = GetComponentsInChildren<Rigidbody2D>()
                .Where(x => !x.IsSleeping())
                .ToArray();

            //  pause rigidbody2D
            if (rigidbodies2D.Length > 0) {
                velocities2D = new RigidBody2DVelocity[rigidbodies2D.Length];
                for (int i = 0; i < rigidbodies2D.Length; ++i) {
                    velocities2D[i] = new RigidBody2DVelocity(rigidbodies2D[i]);
                    rigidbodies2D[i].Sleep();
                }
            }

            //  extract all rigidbody that is not sleeping
            rigidbodies = GetComponentsInChildren<Rigidbody>()
                .Where(x => !x.IsSleeping())
                .ToArray();

            //  pause rigidbody
            if (rigidbodies.Length > 0) {
                velocities = new RigidBodyVelocity[rigidbodies.Length];
                for (int i = 0; i < rigidbodies.Length; ++i) {
                    velocities[i] = new RigidBodyVelocity(rigidbodies[i]);
                    rigidbodies[i].Sleep();
                }
            }

            //  extract enabled animator
            animators = GetComponentsInChildren<Animator>()
                .Where(x => x.isActiveAndEnabled)
                .ToArray();
            //  pause animation
            if (animators.Length > 0) {
                for (int i = 0; i < animators.Length; ++i) {
                    animators[i].Stop();
                }
            }

            //  extract playing audio sources
            audioSources = GetComponentsInChildren<AudioSource>()
                .Where(x => x.isPlaying)
                .ToArray();
            //  pause audio
            foreach (var aud in audioSources) {
                aud.Pause();
            }


            //  extract playing particle system
            particles = GetComponentsInChildren<ParticleSystem>()
                .Where(x => x.isPlaying)
                .ToArray();
            //  pause particle
            foreach (var par in particles) {
                par.Pause(false);
            }


            //  extract all behavior without renderer
            components = GetComponentsInChildren<MonoBehaviour>()
                .Where(x => x != this)
                .Where(x => !(x is Graphic || x is BaseMeshEffect || x is CanvasScaler))
                .Where(x => x.enabled)
                .ToArray();
            //  pause components
            foreach (var com in components) {
                com.enabled = false;
            }

            IsPausing = true;
        }

        public void Resume() {
            //  resume components
            foreach (var com in components) {
                if (com == null) continue;
                com.enabled = true;
            }

            //  resume particle
            foreach (var par in particles) {
                if (par == null) continue;
                par.Play(false);
            }

            //  resume audio
            foreach (var aud in audioSources) {
                if (aud == null) continue;
                aud.Play();
            }

            //  resume animation
            if (animators.Length > 0) {
                for (int i = 0; i < animators.Length; ++i) {
                    if (animators[i] == null) continue;
                    animators[i].enabled = false;
                    animators[i].enabled = true;
                }
            }

            //  resume rigidbody2D
            if (rigidbodies2D.Length > 0) {
                for (int i = 0; i < rigidbodies2D.Length; ++i) {
                    if (rigidbodies2D[i] == null) continue;
                    velocities2D[i].Restore(ref rigidbodies2D[i]);
                    rigidbodies2D[i].WakeUp();
                }
            }

            //  resume rigidbody
            if (rigidbodies.Length > 0) {
                for (int i = 0; i < rigidbodies.Length; ++i) {
                    if (rigidbodies[i] == null) continue;
                    velocities[i].Restore(ref rigidbodies[i]);
                    rigidbodies[i].WakeUp();
                }
            }

            IsPausing = false;
        }
    }
}
