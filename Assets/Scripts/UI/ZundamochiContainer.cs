using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction.UI {
    /// <summary>
    /// UI上のずんだもちコンテナ
    /// Container for Zundamochi
    /// </summary>
    public class ZundamochiContainer : MonoBehaviour{

        public List<UIZundamochi> Mochis;

        private int current { get; set; }

        protected void Awake() {
            current = 0;
        }

        /// <summary>
        /// 餅を取得したときに呼び出します
        /// on get mochi
        /// </summary>
        public void GetMochi() {
            Mochis[current].GetMochi();
            ++current;
        }
    }
}
