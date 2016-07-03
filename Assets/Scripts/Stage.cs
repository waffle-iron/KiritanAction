using UnityEngine;
using KiritanAction.Common;

namespace KiritanAction {

    /// <summary>
    /// ステージクラス
    /// Stage
    /// </summary>
    public class Stage : MonoBehaviour{

        //  ステージ名
        //  stage name
        public string StageName;

        //  ずんだもちの数
        //  zundamochi count obtained
        public int ZundaCount { get; set; }

        //  かかった時間
        //  time after stage started
        public float Time { get; set; }

        //  最速記録
        //  record
        public float Record { get; set; }

        //  最速記録(ずんだもち全回収)
        //  record (obtained all zundamochies)
        public float ZundaRecord { get; set; }

        protected void Awake() {
            ZundaCount = 0;
            Time = 0;
        }

        //  manual initialization
        public void Init(Kiritan kiritan) {
            kiritan.ResponePosition = transform.FindChild("ResponePoint").transform.position.ToVector2();

            var items = transform.FindChild("Items").GetComponentsInChildren<Item>();
            foreach(var i in items) {
                i.Kiritan = kiritan;
            }
        }
    }
}
