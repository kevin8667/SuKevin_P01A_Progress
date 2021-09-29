using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyHealth : Health , IDamageable
{
    public static event Action<float> EnemyTakingDamage;

    public bool _isBossInvincible = false;

    public override void TakeDamage(int amount)
    {
        if (_isBossInvincible == false) 
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
            StartCoroutine(InvincibleTimer());
            StartCoroutine(InvincibleFlash());
        }
        
    }

    private IEnumerator InvincibleTimer()
    {
        _isBossInvincible = true;

        //wait for the required dduration
        yield return new WaitForSeconds(0.3f);

        _isBossInvincible = false;

    }
    private IEnumerator InvincibleFlash()
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.07f);

        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.07f);

        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.07f);
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        yield return new WaitForSeconds(0.07f);

    }
}
