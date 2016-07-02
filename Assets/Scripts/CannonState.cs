

namespace KiritanAction {
    /// <summary>
    /// きりたん砲の状態クラス
    /// cannon state
    /// </summary>
    public class CannonState {

        private FixedInputController inputController { get; set; }

        private Kiritan kiritan { get; set; }

        //  反重力ジャンプ用チャージ状況(Frame)
        //  charge frame count for jump with cannon
        public int AntiGravityChargeFrame{ get; set; }

        //  立ち構え用Powerチャージ状況(Frame)
        //  charge frame count for stance mode
        public int StanceCharge{ get; set; }

        //  Crawl状態用きりたん砲角度(Degree)
        //  cannon angle for crawl mode (degree)
        public float CannonModeAngle { get; set; }

        //  Crawl状態用Powerチャージ状況(Frame)
        //  charge frame count for crawl mode
        public int CannonModeCharge { get; set; }

        /// <summary>
        /// 初期化
        /// initialize
        /// </summary>
        /// <param name="kiritan"></param>
        /// <param name="input"></param>
        public CannonState(Kiritan kiritan, FixedInputController input) {
            this.kiritan = kiritan;
            inputController = input;

            AntiGravityChargeFrame = 0;
        }

        public void OnFixedUpdate() {
            //  ノーマル状態かつJump入力がされているとき、チャージする
            //  charge if Kiritan state is normal and jump button is pressing
            if (kiritan.State == KiritanStateEnum.Normal && inputController.InputButtonTable["Jump"].PressedFrame > 0) {
                ++AntiGravityChargeFrame;
            }
            else if (inputController.InputButtonTable["Jump"].ReleasedFrame > 1) {
                //  ReleasedFrameが1のときは反重力ジャンプの判定にAntiGravityChargeFrameを使用するのでリセットしない
                AntiGravityChargeFrame = 0;
            }
        }
    }
}
