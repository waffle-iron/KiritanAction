using UnityEngine;

namespace KiritanAction.ConcleteItem {

    /// <summary>
    /// 大豆
    /// エネルギーゲージを回復する
    /// 
    /// Soybean
    /// repair energy
    /// </summary>
    public class Daizu : Item{

        //  大豆のエネルギー回復量
        //  repair amount
        private const float chargeValue = 5f;

        protected override void Affect() {
            Kiritan.Energy.Current += chargeValue;
        }
    }
}
