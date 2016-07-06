using UnityEngine;
using UnityEngine.SceneManagement;

namespace KiritanAction.ConcleteKiritanState
{
    [CreateAssetMenu(fileName = "Dead", menuName = "ScriptableObject/KiritanState/Dead")]
    public class Dead : KiritanState
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            //TODO:waiting finish animation

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}