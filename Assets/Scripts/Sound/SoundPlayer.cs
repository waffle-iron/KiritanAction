using UnityEngine;

namespace KiritanAction.Sound {
    //  sound player for clip do not allow overwrap (typically voice)
    public class SoundPlayer : MonoBehaviour{

        /// <summary>
        /// 重複再生を許すか
        /// falseなら再生時に現在再生中のサウンドを停止させる
        /// 
        /// allow overwrap?
        /// if false, stop sound when play new sound
        /// </summary>
        public bool AllowOverwrap;

        /// <summary>
        /// 再生を開始します
        /// start sound
        /// </summary>
        /// <param name="objectName">
        /// SoundSourceのオブジェクト名
        /// child object attached SoundSource component
        /// </param>
        /// <param name="volumeScale">
        /// 再生時のボリューム
        /// volume
        /// </param>
        public void Play(string objectName, float volumeScale = 1.0f) {
            if (!AllowOverwrap) StopAll();
            AudioSource source = transform.FindChild(objectName).GetComponent<AudioSource>();
            source.Play();
        }

        /// <summary>
        /// 全ての子要素のサウンドを停止する
        /// stop all sound
        /// </summary>
        public void StopAll() {
            var sources = GetComponentsInChildren<AudioSource>();
            foreach(var s in sources) {
                s.Stop();
            }
        }
    }
}
