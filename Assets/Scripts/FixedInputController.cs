using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction {

    /// <summary>
    /// InputからFixedUpdate用の入力情報を作成するクラス
    /// ExecutionOrderをデフォルトよりも小さくして扱う
    /// 
    /// input information for FixedUpdate
    /// set ExecutionOrder small
    /// </summary>
    public class FixedInputController : MonoBehaviour {

        /// <summary>
        /// 入力状態テーブルを取得します
        /// input button dictionary
        /// </summary>
        public Dictionary<string, InputButton> InputButtonTable { get; private set; }

        //  FixedUpdateが呼ばれるまでにUpdateを通ったか
        //  is through Update before FixedUpdate is called.
        private bool throughUpdate { get; set; }

        //  初期化
        //  initialize
        protected void Awake() {
            InputButtonTable = new Dictionary<string, InputButton>();
            InputButtonTable.Add("Left", new InputButton());
            InputButtonTable.Add("Right", new InputButton());
            InputButtonTable.Add("Up", new InputButton());
            InputButtonTable.Add("Down", new InputButton());
            InputButtonTable.Add("Jump", new InputButton());
            InputButtonTable.Add("Fire", new InputButton());
            InputButtonTable.Add("SelfKilling", new InputButton());
            InputButtonTable.Add("Pause", new InputButton());

            throughUpdate = false;
        }

        //  FixedUpdateとUpdateの周期が違うため入力はUpdateで拾う
        //  pick input infomation at Update
        protected void Update() {
            throughUpdate = true;
            if (Input.GetAxis("Horizontal") < -0.5f) {
                InputButtonTable["Left"]._tmpPressed = true;
            }
            else if (Input.GetAxis("Horizontal") > 0.5f) {
                InputButtonTable["Right"]._tmpPressed = true;
            }

            if (Input.GetAxis("Vertical") < -0.5f) {
                InputButtonTable["Down"]._tmpPressed = true;
            }
            else if (Input.GetAxis("Vertical") > 0.5f) {
                InputButtonTable["Up"]._tmpPressed = true;
            }

            if (Input.GetButton("Jump")) {
                InputButtonTable["Jump"]._tmpPressed = true;
            }

            if (Input.GetButton("Fire")) {
                InputButtonTable["Fire"]._tmpPressed = true;
            }

            if (Input.GetButtonDown("SelfKilling")) {
                InputButtonTable["SelfKilling"]._tmpPressed = true;
            }

            if (Input.GetButtonDown("Pause")) {
                InputButtonTable["Pause"]._tmpPressed = true;
            }
        }

        //  FixedUpdate用の入力情報に変換する
        //  translate input information for FixedUpdate from for Update
        protected void FixedUpdate() {
            foreach (var key in new List<string>(InputButtonTable.Keys)) {
                InputButtonTable[key]._OnFixedUpdate(throughUpdate);
            }
            throughUpdate = false;
        }

        /// <summary>
        /// 1種類の入力ボタンの押下フレーム数と開放フレーム数を取得するためのクラス
        /// button class
        /// </summary>
        public class InputButton {

            public bool _tmpPressed { get; set; }

            /// <summary>
            /// 押下フレーム数を取得します
            /// get pressed time (frame)
            /// </summary>
            public int PressedFrame { get; private set; }

            /// <summary>
            /// 開放フレーム数を取得します
            /// get released time (frame)
            /// </summary>
            public int ReleasedFrame { get; private set; }


            public InputButton() {
                ReleasedFrame = int.MaxValue;
                PressedFrame = 0;
            }

            //  現在のフレームの入力状態を設定する
            //  called at FixedUpdate
            //  update button state
            public void _OnFixedUpdate(bool throughUpdate) {
                if (throughUpdate) {    //  Updateを通っている場合、Updateの結果を反映する
                    if (_tmpPressed) {
                        if (PressedFrame < int.MaxValue) ++PressedFrame;
                        ReleasedFrame = 0;
                    }
                    else {
                        if (ReleasedFrame < int.MaxValue) ++ReleasedFrame;
                        PressedFrame = 0;
                    }
                    _tmpPressed = false;
                }
                else {  //  Updateを通っていない場合、前回の結果を反映する
                    if (PressedFrame > 0 && PressedFrame < int.MaxValue) ++PressedFrame;
                    if (ReleasedFrame > 0 && ReleasedFrame < int.MaxValue) ++ReleasedFrame;
                }
            }
        }
    }
}
