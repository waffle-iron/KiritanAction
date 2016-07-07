using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace KiritanAction.Debugger{

    public class KiritanDebugger : EditorWindow{

        [MenuItem("KiritanAction/Open KiritanDebugger")]
        public static void OpenWindow() {
            GetWindow<KiritanDebugger>("Kiritan");
        }

        void OnGUI() {
            if (!Application.isPlaying) {
                EditorGUILayout.HelpBox("Game is not playing", MessageType.Info);
                return;
            }
            if (SceneManager.GetActiveScene().name != "Stage") {
                EditorGUILayout.HelpBox("Scene is not in Stage.", MessageType.Info);
                return;
            }

            Life kiritanLife = GameObject.FindGameObjectWithTag("Kiritan").GetComponent<Life>();
            Kiritan kiritan = GameObject.FindGameObjectWithTag("Kiritan").GetComponent<Kiritan>();
            Energy miso = kiritan.Energy;

            kiritanLife.Current = EditorGUILayout.IntSlider("Motivation", kiritanLife.Current, 0, kiritanLife.Max);
            miso.Current = EditorGUILayout.Slider("Miso", miso.Current, 0f, miso.Max);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Send Damage");
            EditorGUILayout.BeginHorizontal();

            int damageValue = EditorGUILayout.IntField("Value", 0);
            if (GUILayout.Button("Send")) {
                GameObject attacker = new GameObject();
                attacker.AddComponent<CircleCollider2D>();
                Attack atk = attacker.AddComponent<Attack>();
                atk.Damage = damageValue;
                kiritan.GetComponent<AttackReceiver>().OnReceivedMethod.Invoke(atk);
                GameObject.Destroy(attacker);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
