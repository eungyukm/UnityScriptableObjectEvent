using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Comman.SO;
using UnityEngine.UI;

public class SliderSetter : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] public FloatVariable varialbe;

    private void Update()
    {
        if(slider != null && varialbe != null)
        {
            slider.value = varialbe.value;
        }
    }
}
