using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Comman.SO;


public class UnitHealth : MonoBehaviour
{
    public FloatVariable hp;
    public bool resetHP;
    public FloatReference startingHP;

    private void Start()
    {
        if(resetHP)
        {
            hp.SetValue(startingHP);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ãæµ¹!");
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
        if(damage != null)
        {
            hp.ApplyChange(-damage.damageAmount);
        }
    }
}
