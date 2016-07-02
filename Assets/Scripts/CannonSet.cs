using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction {

    /// <summary>
    /// Cannonのインスタンスを保持するやつ
    /// collection of cannon level 
    /// </summary>
    [CreateAssetMenu(fileName="CannonSet", menuName="ScriptableObject/CannonSet")]
    public class CannonSet : ScriptableObject{

        //  CannonLevelのインスタンス
        //  cannon level objects
        public List<CannonLevel> Cannons;

        /// <summary>
        /// 現在装備中のCannonLevelを取得します
        /// get cannon level equiped
        /// </summary>
        public CannonLevel CurrentCannonLevel(Kiritan kiritan) {
            return Cannons[kiritan.CannonLevel];
        }
    }
}
