using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillSetter : MonoBehaviour
{
    public FloatReference variable;
    public FloatReference min;
    public FloatReference max;

    public Image image;

    private void Update()
    {
        image.fillAmount = Mathf.Clamp01(
            Mathf.InverseLerp(min, max, variable));
    }
}
