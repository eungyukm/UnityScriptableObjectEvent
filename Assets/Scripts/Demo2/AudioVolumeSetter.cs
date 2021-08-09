using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Comman.SO;
using UnityEngine.Audio;

public class AudioVolumeSetter : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName = "";
    public FloatVariable variable;

    // Update is called once per frame
    void Update()
    {
        float db = variable.value > 0.0f ? 20.0f * Mathf.Log10(variable.value) : -80.0f;

        mixer.SetFloat(parameterName, db);
    }
}
