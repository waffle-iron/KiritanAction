using UnityEngine;
using UnityEngine.Events;

namespace KiritanAction {
    /// <summary>
    /// life point
    /// </summary>
    public class Life : MonoBehaviour{

        /// <summary>
        /// on dead actions
        /// </summary>
        public UnityEvent OnDead;

        /// <summary>
        /// max value of life
        /// </summary>
        public int Max;

        //  actual current value
        private int _current { get; set; }

        protected void Awake() {
            Current = Max;
        }

        /// <summary>
        /// get or set current value of life
        /// [0, Max]
        /// </summary>
        public int Current {
            get { return _current; }
            set {
                _current = value;
                if (_current > Max) _current = Max;
                if (_current < 0) _current = 0;

                if (Current == 0) {
                    OnDead.Invoke();
                }
            }
        }

        /// <summary>
        /// influence damage
        /// </summary>
        /// <param name="value">damage value</param>
        public void Damage(int value) {
            Current -= value;
        }
    }
}
