using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comman.ScriptableObject.Variables
{
    public class VariableAudioTrigger : MonoBehaviour
    {
        public AudioSource audioSource;
        public FloatVariable variable;
        public FloatReference lowThreshold;

        private void Update()
        {
            if (variable.value < lowThreshold)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }
}

