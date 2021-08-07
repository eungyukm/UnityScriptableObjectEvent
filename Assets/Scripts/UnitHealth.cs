using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Comman.ScriptableObject.Variables;


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
        Debug.Log("충돌!");
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
        if(damage != null)
        {
            hp.ApplyChange(-damage.damageAmount);
        }
    }
}
