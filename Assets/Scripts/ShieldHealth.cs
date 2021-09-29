using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : Health , IDamageable
{
    private GameObject _boss;

    private void Awake()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");
        _currentHealth = _maxHealth;

    }


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
            Boss _newBoss = _boss.gameObject.GetComponent<Boss>();
            _newBoss._isShielded = false;

        }


    }


}
