using UnityEngine;
using System.Collections.Generic;

namespace KiritanAction.Presentation {
    /// <summary>
    /// transformのRotationとParticleSystemのStartRotationを同期させる
    /// sync ParticleSystem.StartRotation to object rotation 
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class RotationSyncParticle : MonoBehaviour{

        private float defaultRotation { get; set; }

        private ParticleSystem particle { get; set; }

        protected void Awake(){
            particle = GetComponent<ParticleSystem>();
            defaultRotation = particle.startRotation;
        }

        protected void Update() {
            particle.startRotation = (defaultRotation - transform.rotation.eulerAngles.z) * Mathf.Deg2Rad;
        }
    }
}
