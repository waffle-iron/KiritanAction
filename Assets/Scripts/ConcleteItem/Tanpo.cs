using UnityEngine;

namespace KiritanAction.ConcleteItem {

    /// <summary>
    /// たんぽ
    /// Cannonレベルが上昇する
    /// 
    /// Kiritanpo
    /// raise cannon level
    /// </summary>
    public class Tanpo : Item{

        private CannonSet cannonSet { get; set; }

        protected override void Awake() {
            base.Awake();

            cannonSet = Resources.Load<CannonSet>("Data/CannonSet");
        }

        protected override void Affect() {
            Kiritan.Voice("Kiritanpo");

            Kiritan.CannonLevelUp();
        }
    }
}
