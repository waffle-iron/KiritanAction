using UnityEngine;

namespace KiritanAction.ConcleteItem {

    /// <summary>
    /// 浮遊するずんだもち
    /// Zundamochi
    /// </summary>
    public class Zundamochi : Item{

        private Stage currentStage { get; set; }

        private UI.ZundamochiContainer zundaCountainer { get; set; }

        protected override void Awake() {
            base.Awake();

            StageSet stageSet = Resources.Load<StageSet>("Data/StageSet");
            currentStage = stageSet.CurrentStage();

            zundaCountainer = GameObject.FindGameObjectWithTag("ZundamochiContainer").GetComponent<UI.ZundamochiContainer>();
        }

        protected override void Affect() {
            Kiritan.Voice("Zundamochi");

            ++ currentStage.ZundaCount;

            zundaCountainer.GetMochi();
        }
    }
}
