using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Comman.ScriptableObject.Variables;

public class UnitHealthEvent : MonoBehaviour
{
    public FloatVariable hp;
    public bool resetHP;
    public FloatReference startingHP;
    public UnityEvent damageEvent;
    public UnityEvent deathEvent;

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
            damageEvent.Invoke();
        }

        if(hp.value <= 0.0f)
        {
            deathEvent.Invoke();
        }
    }
}
