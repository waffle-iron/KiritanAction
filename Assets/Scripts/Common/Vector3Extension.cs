using UnityEngine;

namespace KiritanAction.Common {
    /// <summary>
    /// extensions of Vector3
    /// </summary>
    public static class Vector3Extension {

        /// <summary>
        /// create Vector2 using x and y
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Vector3 self) {
            return new Vector2(self.x, self.y);
        }
    }
}
