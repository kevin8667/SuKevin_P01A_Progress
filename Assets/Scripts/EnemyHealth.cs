using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyHealth : Health , IDamageable
{
    public static event Action<float> EnemyTakingDamage;

    public override void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log(gameObject.name + ": " + _currentHealth);
        if (_damagedSound != null)
        {
            AudioHelper.PlayClip2D(_damagedSound, 1f);
        }
        if (_currentHealth <= 0)
        {
            Kill();
        }
        EnemyTakingDamage?.Invoke((float)amount / _maxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
