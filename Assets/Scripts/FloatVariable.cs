using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FloatVariable")]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string developerDescription = "";
# endif
    public float value;

    public void SetValue(float param)
    {
        value = param;
    }

    public void SetValue(FloatVariable param)
    {
        value = param.value;
    }

    public void ApplyChange(float amount)
    {
        value += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        value += amount.value;
    }
}
