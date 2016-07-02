using UnityEngine;

namespace KiritanAction {

    /// <summary>
    /// ステージクラス
    /// Stage
    /// </summary>
    [CreateAssetMenu(fileName="NewStage", menuName="ScriptableObject/Stage")]
    public class Stage : ScriptableObject{

        //  ステージ名
        //  stage name
        public string StageName;

        //  ステージのPrefab
        //  Prefab of stage
        public GameObject StagePrefab;

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

        //  初期化
        //  initalize
        protected void OnEnable() {
            ZundaCount = 0;
            Time = 0;
        }
    }
}
