using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Comman.SO;
using UnityEngine.UI;

public class TextReplacer : MonoBehaviour
{
    public Text text;
    public StringVariable stringVariable;
    public bool alwaysUpdate;

    private void OnEnable()
    {
        text.text = stringVariable.Value;
    }

    // Update is called once per frame
    void Update()
    {
        if(alwaysUpdate)
        {
            text.text = stringVariable.Value;
        }
    }
}
