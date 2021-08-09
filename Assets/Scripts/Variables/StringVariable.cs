using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comman.SO
{
    [CreateAssetMenu(menuName ="SO/StringVariable")]
    public class StringVariable : ScriptableObject
    {
        [SerializeField]
        private string value = "";

        public string Value
        {
            get 
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}
