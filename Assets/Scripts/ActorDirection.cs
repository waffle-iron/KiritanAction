using System;

namespace KiritanAction {

    /// <summary>
    /// キャラクタの向きの列挙
    /// direction of actor
    /// </summary>
    public enum ActorDirection {
        Right,
        Left,
    }

    /// <summary>
    /// ActorDirectonの拡張メソッド
    /// extensions of ActorDirection
    /// </summary>
    public static class ActorDirectionExt {

        /// <summary>
        /// 向きをfloat型で取得します
        /// 右向きなら1
        /// 左向きなら-1
        /// 
        /// get float value
        /// return 1 if direction is right
        /// return -1 if direction is left
        /// </summary>
        /// <param name="dir">
        /// 向き
        /// self direction
        /// </param>
        /// <returns>1 or -1</returns>
        public static float ToFloat(this ActorDirection dir) {
            switch (dir) {
                case ActorDirection.Left:
                    return -1.0f;
                case ActorDirection.Right:
                    return 1.0f;
                default:
                    throw new NotImplementedException("unreachable code");
            }
        }
    }
}
